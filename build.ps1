<#
.SYNOPSIS
  Builds the single AMR Sheet Set Manager Dynamo package for every supported Civil 3D year.

.DESCRIPTION
  Supported years come from build\HostVersions.props. The result is one package:

    dist\AMR Sheet Set Manager\
      pkg.json
      bin\AMR.SSMgr.Loader.dll         picks the right build at Dynamo startup
      bin\<year>\AMR.SSMgr.dll         one build per Civil 3D year
      extra\AMR.SSMgr_ExtensionDefinition.xml, sample graphs

  With -Deploy, the package is copied into each supported year's Dynamo user packages
  folder (%AppData%\Autodesk\C3D <year>\Dynamo\<version>\packages), replacing any existing
  "AMR Sheet Set Manager" folder there. The newest Dynamo version folder is used, because
  Dynamo renames it between releases.

.EXAMPLE
  .\build.ps1              # build the package, Release
  .\build.ps1 -Deploy      # build, then install into every Civil 3D year found
#>
[CmdletBinding()]
param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Release',
    [switch]$Deploy
)

$ErrorActionPreference = 'Stop'
$repo = $PSScriptRoot
$nodes = Join-Path $repo 'src\AMR.SSMgr\AMR.SSMgr.csproj'
$loader = Join-Path $repo 'src\AMR.SSMgr.Loader\AMR.SSMgr.Loader.csproj'
$packageName = 'AMR Sheet Set Manager'
$packageDir = Join-Path $repo "dist\$packageName"

$years = ((& dotnet msbuild $nodes -nologo -getProperty:SupportedAcadYears).Trim() -split ';') | Where-Object { $_ }

# Start clean so a year removed from the table doesn't linger in the package.
if (Test-Path $packageDir) { Remove-Item $packageDir -Recurse -Force }

Write-Host "`n=== Loader ($Configuration) ===" -ForegroundColor Cyan
& dotnet build $loader -c $Configuration -nologo -v:minimal
if ($LASTEXITCODE -ne 0) { throw 'Loader build failed.' }

foreach ($year in $years) {
    Write-Host "`n=== Civil 3D $year nodes ($Configuration) ===" -ForegroundColor Cyan
    & dotnet build $nodes -c $Configuration "-p:AcadYear=$year" -nologo -v:minimal
    if ($LASTEXITCODE -ne 0) { throw "Build failed for Civil 3D $year." }
}

$version = (Get-Content (Join-Path $packageDir 'pkg.json') -Raw | ConvertFrom-Json).version
Write-Host "`nPackage $version for Civil 3D $($years -join ', ') -> $packageDir" -ForegroundColor Green

if ($Deploy) {
    foreach ($year in $years) {
        $dynamoRoot = Join-Path $env:APPDATA "Autodesk\C3D $year\Dynamo"
        $newest = if (Test-Path $dynamoRoot) {
            Get-ChildItem $dynamoRoot -Directory |
                Where-Object { $_.Name -match '^\d+(\.\d+)+$' } |
                Sort-Object { [version]$_.Name } -Descending |
                Select-Object -First 1
        }
        if (-not $newest) {
            Write-Warning "No Dynamo folder for Civil 3D $year under %AppData%; open Dynamo in Civil 3D $year once, then re-run."
            continue
        }
        $target = Join-Path $newest.FullName "packages\$packageName"
        # Replace the whole folder: an older AMR.SSMgr.<year>.dll left in bin\ would make
        # Dynamo load the same node types twice.
        if (Test-Path $target) { Remove-Item $target -Recurse -Force }
        New-Item -ItemType Directory -Force $target | Out-Null
        Copy-Item -Path (Join-Path $packageDir '*') -Destination $target -Recurse -Force
        Write-Host "Deployed to $target" -ForegroundColor Green
    }
}
