function DeployTemplate
{
	Param(
	 [Parameter(Mandatory=$true)]
	 [string]$templateFile,
	 [Parameter(Mandatory=$true)]
	 [object]$templateParametersObject,
	 [Parameter(Mandatory=$true)]
	 [string]$resourceGroupName,
	 [Parameter(Mandatory=$false)]
	 [string]$suffix = ""
	)

	$deploymentName = $suffix + ((Get-ChildItem $templateFile).BaseName)
	Write-Host "Deploying ARM template: " $templateFile $deploymentName

	$deployments = Get-AzureRmResourceGroupDeployment -ResourceGroupName $resourceGroupName

	# Based on https://blog.maximerouiller.com/post/cleaning-up-azure-resource-manager-deployments-in-continuous-integration-scenario/
	$deploymentsToDelete = $deployments | where { $_.DeploymentName.StartsWith($deploymentName) } | select -Last 5

	foreach ($deployment in $deploymentsToDelete) {
		Write-Host "Preparing to remove deployment" $deployment.DeploymentName 
		Write-Host "Start to remove deployment" $deployment.DeploymentName
		Remove-AzureRmResourceGroupDeployment -ResourceGroupName $resourceGroupName -DeploymentName $deployment.DeploymentName
		Write-Host "Removed deployment" $deployment.DeploymentName
	}

	$result = New-AzureRmResourceGroupDeployment -Name $deploymentName `
										   -ResourceGroupName $resourceGroupName `
										   -TemplateFile $templateFile `
										   -TemplateParameterObject $templateParametersObject `
										   -Force -Verbose `
										   -ErrorVariable ErrorMessages `

	$ErrorMessages = $ErrorMessages | ForEach-Object { $_.Exception.Message.TrimEnd("`r`n") }

	if ($ErrorMessages)
	{
		"", ("{0} returned the following errors:" -f ("Template deployment", "Validation")[[bool]$ValidateOnly]), @($ErrorMessages) | ForEach-Object { Write-Output $_ }
	}

	return $result
}