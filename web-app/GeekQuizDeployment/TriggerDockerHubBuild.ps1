#
# TriggerDockerHubBuild.ps1
#

param (
    [string][Parameter(Mandatory=$true)]$triggerToken
)

$json = '{"build":true}'
$uri = "https://registry.hub.docker.com/u/sbozovic/docker-geekquiz/trigger/"+$triggerToken+"/"

Invoke-RestMethod -Uri $uri -Method Post -ContentType "application/json" -Body $json

Write-Verbose "Invoked Docker Hub build trigger" -Verbose