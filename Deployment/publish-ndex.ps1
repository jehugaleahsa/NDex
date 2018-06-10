&dotnet pack "..\NDex\NDex.csproj" --configuration Release --output $PWD

.\NuGet.exe push NDex.*.nupkg -Source https://www.nuget.org/api/v2/package

Remove-Item NDex.*.nupkg