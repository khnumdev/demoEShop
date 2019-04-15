Param(
[Parameter(Mandatory=$true)]
 [string]$resourceGroupName,
 [Parameter(Mandatory=$true)]
 [string]$hostserviceName,
 [Parameter(Mandatory=$false)]
 [string]$webappName
)

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$functionsDir = Join-Path -Path $scriptDir -ChildPath "\Functions"
$templatesDir = Join-Path -Path $scriptDir -ChildPath "..\Templates"

. "$functionsDir\DeployTemplate.ps1"

# Templates path
$webappTemplatePath = "$templatesDir\WebSite.json"

$templateParameters = @{}
$templateParameters.Add("hostingPlanName", $hostserviceName)
$templateParameters.Add("websiteName", $webappName)

$deploymentResult = DeployTemplate -templateFile $webappTemplatePath -templateParametersObject $templateParameters -resourceGroupName $resourceGroupName -ErrorAction Stop

$websiteUrl = $deploymentResult.outputs.websiteUrl.Value

# Provide webapp FTP settings to VSTS pipeline
Write-Output ("##vso[task.setvariable variable=websiteUrl;]$websiteUrl")