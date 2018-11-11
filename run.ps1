param(
  [string] $Action,
  [string] $PathToMsBuild = "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin",
  [string] $PathToSqlPackage = "C:\Program Files\Microsoft SQL Server\140\DAC\bin"
)
function Start-Environment {

  Write-Host "Starting docker containers"
  docker-compose --file .\docker\docker-compose.yml up --detach
  
  Write-Host "Configuring path"
  $Env:Path =  "$($Env:Path);$($PathToMsBuild);$($PathToSqlPackage)"

  Write-Host "Building DACPAC"
  msbuild.exe .\src\Sql\Sql.sqlproj /p:configuration="Release" /p:VisualStudioVersion="15.0"

  Write-Host "Publishing Database"
  sqlpackage.exe /Action:Publish /SourceFile:".\src\Sql\bin\Release\Sql.dacpac" /TargetConnectionString:"Server=localhost,1436;Database=sample;User Id=sa;Password=mys@passw0rd;" /p:BlockOnPossibleDataLoss=False
}

function Start-Build {
  Write-Host "Starting build"
  dotnet build .\src\Sample.sln
}

function Stop-Environment {
  Write-Host "Stopping and removing docker containers"
  docker-compose --file .\docker\docker-compose.yml down
}

function Get-MSBuildPath()
{
    $path = cmd.exe /c '"C:\Program Files (x86)\Microsoft Visual Studio\Installer\vswhere.exe" -latest -products * -requires Microsoft.Component.MSBuild -property installationPath 2>&1'
    if ($path) 
    {
      $path = Join-Path $path 'MSBuild\15.0\Bin'
      if (Test-Path $path) 
      {
        return $path
      }
    }
}
function Start-Integration-Tests {
  Write-Host "Starting integration tests"
  dotnet test --no-build  .\src\IntegrationTests\IntegrationTests.csproj
}

If (-not (Test-Path $PathToMsBuild)) {
  $PathToMsBuild = Get-MSBuildPath
} 

If (-not (Test-Path $PathToMsBuild)) {
  Write-Error "The path to MSBuild does not exist. Ensure that '$($PathToMsBuild)' exists. If MSBuild is found at a different path you can provide the -PathToMsBuild parameter when running this script."
  Exit
} 

If (-not (Test-Path $PathToSqlPackage)) {
  Write-Error "The path to SqlPackage.exe does not exist. Ensure that '$($PathToSqlPackage)' exists. You can install SqlPackage.exe from https://docs.microsoft.com/en-us/sql/tools/sqlpackage-download?view=sql-server-2017"
  Exit
}

switch ( $Action ) {
  start { Start-Environment }
  stop { Stop-Environment }
  build { Start-Build }
  integration-tests { Start-Integration-Tests }
  default { 
    if($Action -eq '') {
      Start-Environment
      Start-Build
      Start-Integration-Tests
    } else {
      Write-Error "'$($Action)' is an in valid action. Did you mean 'start', 'stop', 'seed', 'build', 'unit-tests', 'integration-tests' or 'performance-tests'?" }
    }
}