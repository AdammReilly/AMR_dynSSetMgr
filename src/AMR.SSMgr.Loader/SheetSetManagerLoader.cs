using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using Dynamo.Extensions;
using Dynamo.Library;
using Dynamo.Logging;

namespace AMR.SSMgr.Loader
{
    /// <summary>
    /// Dynamo extension that loads the AMR.SSMgr node library built for the running AutoCAD release.
    /// </summary>
    /// <remarks>
    /// The package ships one build per AutoCAD release in bin\&lt;year&gt;\AMR.SSMgr.dll, because the
    /// Sheet Set Manager COM interfaces change between releases. Dynamo only auto-loads DLLs at the
    /// top of bin\, so this extension picks the matching build and hands it to Dynamo.
    ///
    /// A build matches when the acdbmgd version it was compiled against has the same major.minor
    /// as the acdbmgd loaded in this AutoCAD session (25.0 = 2025, 25.1 = 2026, 26.0 = 2027).
    /// No year table is needed here: each build describes itself.
    /// </remarks>
    public class SheetSetManagerLoader : IExtension, ILogSource
    {
        private const string NodeLibraryFileName = "AMR.SSMgr.dll";
        private const string AutoCadDatabaseAssembly = "acdbmgd";

        public string UniqueId => "4f6b2c1e-8d3a-4b7e-9a51-2c0d6e8f1a73";

        public string Name => "AMR Sheet Set Manager Loader";

        public event Action<ILogMessage> MessageLogged;

        public void Startup(StartupParams sp)
        {
            try
            {
                LoadMatchingBuild(sp.LibraryLoader);
            }
            catch (Exception ex)
            {
                Log("AMR Sheet Set Manager: failed to load nodes. " + ex);
            }
        }

        public void Ready(ReadyParams sp) { }

        public void Shutdown() { }

        public void Dispose() { }

        private void LoadMatchingBuild(ILibraryLoader libraryLoader)
        {
            Version host = GetLoadedVersion(AutoCadDatabaseAssembly);

            if (host == null)
            {
                Log("AMR Sheet Set Manager: AutoCAD is not loaded in this process, so no nodes were loaded. Run Dynamo from inside Civil 3D or AutoCAD.");
                return;
            }

            string binDir = Path.GetDirectoryName(typeof(SheetSetManagerLoader).Assembly.Location);

            var builds = Directory.EnumerateDirectories(binDir)
                .Select(dir => Path.Combine(dir, NodeLibraryFileName))
                .Where(File.Exists)
                .Select(path => new { Path = path, Target = GetAutoCadTargetVersion(path) })
                .Where(b => b.Target != null && b.Target.Major == host.Major && b.Target.Minor <= host.Minor)
                .OrderByDescending(b => b.Target.Minor)
                .ToList();

            var match = builds.FirstOrDefault();
            if (match == null)
            {
                Log($"AMR Sheet Set Manager: no build in '{binDir}' supports this AutoCAD release (acdbmgd {host}). Check for a newer package version.");
                return;
            }

            if (match.Target.Minor != host.Minor)
            {
                Log($"AMR Sheet Set Manager: no exact build for acdbmgd {host}; using the one built for {match.Target}.");
            }

            // The node library's own dependencies (AMR.SSMgr.ComHelpers.dll) sit beside it in
            // bin\<year>\. Resolve them from there explicitly rather than relying on probing.
            string buildDir = Path.GetDirectoryName(match.Path);
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string candidate = Path.Combine(buildDir, new AssemblyName(args.Name).Name + ".dll");
                return File.Exists(candidate) ? Assembly.LoadFrom(candidate) : null;
            };

            Assembly library = Assembly.LoadFrom(match.Path);
            libraryLoader.LoadNodeLibrary(library);
            Log($"AMR Sheet Set Manager: loaded {match.Path} for acdbmgd {host}.");
        }

        private static Version GetLoadedVersion(string assemblyName)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Select(a => a.GetName())
                .FirstOrDefault(n => string.Equals(n.Name, assemblyName, StringComparison.OrdinalIgnoreCase))
                ?.Version;
        }

        /// <summary>
        /// Reads which AutoCAD release a build was compiled against, without loading it
        /// (a build for a newer .NET would fail to load). acdbmgd, accoremgd and acmgd share
        /// the release version, and a build only records the ones its code actually uses.
        /// </summary>
        private static Version GetAutoCadTargetVersion(string path)
        {
            string[] autoCadAssemblies = { AutoCadDatabaseAssembly, "accoremgd", "acmgd" };
            try
            {
                using var stream = File.OpenRead(path);
                using var pe = new PEReader(stream);
                MetadataReader reader = pe.GetMetadataReader();
                foreach (AssemblyReferenceHandle handle in reader.AssemblyReferences)
                {
                    AssemblyReference reference = reader.GetAssemblyReference(handle);
                    string name = reader.GetString(reference.Name);
                    if (autoCadAssemblies.Contains(name, StringComparer.OrdinalIgnoreCase))
                    {
                        return reference.Version;
                    }
                }
            }
            catch (Exception)
            {
                // Not a readable .NET assembly; skip it.
            }
            return null;
        }

        private void Log(string message)
        {
            MessageLogged?.Invoke(LogMessage.Info(message));
        }
    }
}
