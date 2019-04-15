Param(
 [Parameter(Mandatory=$true)]
 [string]$webAppName,
 [Parameter(Mandatory=$true)]
 [string]$runSettingsFilePath
)

$con = Get-Content $runSettingsFilePath

$con | % { $_.Replace("http://localhost:17469",$webAppName) } `
  | Set-Content $runSettingsFilePath