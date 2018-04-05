param(
    [ValidateSet("Start", "Stop", "Info")]
    $Action = "Info"
)

Function GetImageAndStartBitbucketServer {

    Push-Location $PSScriptRoot

    # get bitbucket-server image
    docker pull atlassian/bitbucket-server

    # create container
    docker create --name "my-bitbucket" -u root -v /data/stash:/var/atlassian/application-data/bitbucket -p 7990:7990 atlassian/bitbucket-server

    # copy automated setup file
    docker cp bitbucket.properties my-bitbucket:/var/atlassian/application-data/bitbucket

    # start container
    docker start my-bitbucket

    # (optional) exec bash on container
    #docker exec -it my-bitbucket bash

    Write-Output 'Running BitBucket automated setup...'
    Write-Output '   check the progress at http://localhost:7990'   # TO FIX: gather this value from Docker
    Write-Output '   login using: username => admin     password => 123'    # TO FIX: gather this value from bitbucket.properties
    # TO FIX: periodic check until BitBucket is fully setup

    Pop-Location
}

Function GetContainerStatus {
    $status = docker ps -a --filter name=my-bitbucket --format "{{.Status}}"

    if($status -eq $null) {
        Write-Output 'BitBucket server container not created...'
    } else {
        Write-Output "BitBucket server container status: '$status'"
    }
}

Function StopAndDeleteBitbucketContainer {
    # stop container
    docker stop my-bitbucket
    # delete container
    docker rm my-bitbucket
    
    Write-Output 'BitBucket container stopped and removed...'
}

Switch ($Action)
{
    "Info" { GetContainerStatus }
    "Start" { GetImageAndStartBitbucketServer }
    "Stop" { StopAndDeleteBitbucketContainer }
}