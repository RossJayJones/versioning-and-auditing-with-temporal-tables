param(
  [string] $Action
)

# Import utility functions to help find vswhere and sql package
. ".\SqlPackageOnTargetMachines.ps1"

function Get-MsBuildPath {
  Write-Host -NoNewline "Locating MSBuild..."
  $path = & (Find-VSWhere) -find MSBuild\**\Bin\MSBuild.exe | select-object -first 1

  If (-not (Test-Path $path)) {
    Write-Error "Unable not locate MSBuild. Is Visual Studio 2017 (15.2) or later installed?"
    Exit
  } 

  Write-Host " done."
  return $path;
}

function Get-SqlPackagePath {
  Write-Host -NoNewline "Locating SqlPackage..."
  $path = Get-SqlPackageOnTargetMachine

  If (-not (Test-Path $path)) {
    Write-Error "Unable not locate SqlPackage. You can acquire SqlPackage from https://docs.microsoft.com/en-us/sql/tools/sqlpackage-download"
    Exit
  }

  Write-Host " done."
  return $path;
}

function Start-Environment {

  Write-Host "Starting docker containers"
  docker-compose --file .\docker\docker-compose.yml up --detach
  
  Write-Host "Building DACPAC"
  & (Get-MsBuildPath) .\src\Sql\Sql.sqlproj /p:configuration="Release"

  Write-Host "Publishing Database"
  & (Get-SqlPackagePath) /Action:Publish /SourceFile:".\src\Sql\bin\Release\Sql.dacpac" /TargetConnectionString:"Server=localhost,1436;Database=sample;User Id=sa;Password=mys@passw0rd;" /p:BlockOnPossibleDataLoss=False
}

function Start-Build {
  Write-Host "Starting build"
  dotnet build .\src\Sample.sln
}

function Stop-Environment {
  Write-Host "Stopping and removing docker containers"
  docker-compose --file .\docker\docker-compose.yml down
}

function Start-Integration-Tests {
  Write-Host "Starting integration tests"
  dotnet test --no-build  .\src\IntegrationTests\IntegrationTests.csproj
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