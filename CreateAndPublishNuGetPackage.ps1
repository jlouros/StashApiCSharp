param(
	[parameter(Mandatory=$true)]
	[string]$ApiKey
)

# removes existing '*.nupkg' pacakges
Get-ChildItem . | ? { $_.Name -imatch ".nupkg$" } | ForEach-Object { Remove-Item $_.FullName }

# creates nuget package
..\NuGet.exe pack .\Atlassian.Stash.Api\Atlassian.Stash.Api.csproj -Prop Configuration=Release

# publish package
$packageName = Get-ChildItem . | ? { $_.Name -imatch ".nupkg$" }

Write-Output "`r`nAbout to publish '$packageName'."
$userConfirm = Read-Host "Do you want to proceed? [Y]es/[N]o"

if($userConfirm -match "Y") 
{
	Write-Output "`r`nPublishing package..."	
	..\NuGet.exe push $packageName -ApiKey $ApiKey	
} 
else 
{
	Write-Output "`r`nPublish operation skipped by the user."	
}