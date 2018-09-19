param([string]$version="2.0.0.0")

$env:Path += ";C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE"
$projects = "ToastNotifications", "ToastNotifications.Messages"
$solution = "../Src/WpfNotifications.sln"

$versionRegexp = "(\d+\.\d+\.\d+\.\d+)"

 foreach ($project in $projects) {
   $assemblyInfoFile = "../Src/"+$project+"/Properties/AssemblyInfo.cs"
   (Get-Content $assemblyInfoFile) -replace $versionRegexp, $version | Set-Content $assemblyInfoFile
 }

devenv $solution /rebuild Release

 foreach ($project in $projects) {
   $csprojFile = "../Src/"+$project+"/"+$project+".csproj"
   ./nuget.exe pack $csprojFile -Prop Configuration=Release
 }

