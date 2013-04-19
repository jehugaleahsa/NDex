msbuild ../NDex.sln /p:Configuration=Release
nuget pack ../NDex/NDex.csproj -Prop Configuration=Release
nuget push *.nupkg
del *.nupkg