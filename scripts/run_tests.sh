#! /bin/bash


dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura 
#dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutputDirectory=./tmp/
#dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura --results-directory:"./tmp/"
reportgenerator -reports:./NKanren.Tests/coverage.cobertura.xml -targetdir:./tmp -reporttypes:Html
brave-browser ./tmp/index.html
