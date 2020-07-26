param(
    [string]$projectName,
    [string]$solution
)
$directory = "Tests"

echo "Create new classlib project"
dotnet new classlib -n $projectName -o "./$directory/$projectName"

echo "Install UnitTest dependencies"
dotnet add ./$directory/$projectName package Microsoft.NET.Test.Sdk -s https://api.nuget.org/v3/index.json
dotnet add ./$directory/$projectName package xunit -s https://api.nuget.org/v3/index.json
dotnet add ./$directory/$projectName package xunit.runner.visualstudio -s https://api.nuget.org/v3/index.json

echo "Add project to current solution"
dotnet sln $solution add ./$directory/$projectName