declare solutionPath=$1
declare projectKey=$2
declare sonarHostUrl=$3
declare sonarLoginToken=$4

echo $solutionPath
echo $projectKey
echo $sonarHostUrl
echo $sonarLoginToken

echo "> Install dotnet-sonarscanner"
dotnet tool install --global dotnet-sonarscanner --version 4.8.0

echo "> Begin .NET Core - Scanner for MSBuild"
dotnet sonarscanner begin /k:$projectKey /d:sonar.host.url=$sonarHostUrl /d:sonar.login=$sonarLoginToken
dotnet build $solutionPath
dotnet sonarscanner end /d:sonar.login=$sonarLoginToken
echo "> End .NET Core - Scanner for MSBuild"