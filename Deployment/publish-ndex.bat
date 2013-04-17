nuget pack ../NDex/NDex.csproj -Prop Configuration=Release -Build
nuget push *.nupkg
del *.nupkg