#$oldFileVersion = "2.0.0-beta3-build04840"
#$newFileVersion = "2.0.0-beta3"

$newVersion = .\NuGet.exe list "Microsoft.ApplicationInsights" -Source https://www.myget.org/F/applicationinsights -Pre -NonInteractive | Select-String -Pattern "Microsoft.ApplicationInsights " | %{$_.Line.Split(" ")} | Select -skip 1

Write-Host $newVersion

$oldVersion = cat .\Global.props | Select-String -Pattern "CoreSdkVersion" | %{$_.Line.Split("<>")} | Select -skip 2 | Select -First 1

Write-Host $oldVersion

(Get-Content Global.props) | 
Foreach-Object {$_ -replace $oldVersion, $newVersion} | 
Set-Content Global.props 


Get-ChildItem -Filter packages.config -Recurse | 
foreach-object {
  (Get-Content $_.FullName) | 
  Foreach-Object {$_ -replace $oldVersion, $newVersion} | 
  Set-Content $_.FullName
}


Get-ChildItem -Filter *proj -Recurse | 
foreach-object {
  (Get-Content $_.FullName) | 
  Foreach-Object {$_ -replace $oldVersion, $newVersion} | 
  Set-Content $_.FullName


  (Get-Content $_.FullName) | 
  Foreach-Object {$_ -replace $oldFileVersion, $newFileVersion} | 
  Set-Content $_.FullName
}