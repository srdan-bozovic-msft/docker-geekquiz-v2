<#

    .SYNOPSIS 
        Creates new RG and Dev QA enviroment for testing. Enviroment has Ubuntu Linux with needed Docker bits  

    .DESCRIPTION
        This sample runbooks creates fresh, new QA enviroment after Docker build initiates/triggers webhook

        Note: If you do not uncomment the "Select-AzureSubscription" statement (on line 58) and insert
        the name of your Azure subscription, the runbook will use the default subscription.

    .PARAMETER 
       No parameter required for this runbook

    .REQUIREMENTS 
        This runbook requires the Azure Resource Manager PowerShell module has been imported into 
        your Azure Automation instance.

        
    .NOTES
        AUTHOR: Aleksandar Dordevic, @Alex_ZZ_
        LASTEDIT: Nov 11, 2015
#>

workflow docker-geekquiz-v2
{
	#The name of the Automation Credential Asset this runbook will use to authenticate to Azure.
    $CredentialAssetName = 'Azure-PS-Cred'
   
    #Get the credential with the above name from the Automation Asset store
    $Cred = Get-AutomationPSCredential -Name $CredentialAssetName

    if(!$Cred) {
        Throw "Could not find an Automation Credential Asset named '${CredentialAssetName}'. Make sure you have created one in this Automation Account."
    }

    #Connect to your Azure Account
    $Account = Add-AzureAccount -Credential $Cred
    
    if(!$Account) {
        Throw "Could not authenticate to Azure using the credential asset '${CredentialAssetName}'. Make sure the user name and password are correct."
    }
	

 #Getting json template for ARM
 $TemplateFile = 'https://raw.githubusercontent.com/srdjan-bozovic/docker-geekquiz/master/docker-deploy.json'

 #Transfroming local system time&date to proper format for ARM usafe
 $invalidChars = [io.path]::GetInvalidFileNamechars() 
 $get_date = Get-Date -format s
 $arm_rg_name= "docker-QA-"+($get_date.ToString() -replace "[$invalidChars]","-")
	$tmp_name_transform_0 = "dockerqa"+($get_date.ToString() -replace "[$invalidChars]","-")
	$tmp_name_transform_1 = ($tmp_name_transform_0.ToString() -replace "T","t")
 $arm_storage_account = ($tmp_name_transform_1.ToString() -replace "-","")
 $arm_pub_name = $arm_storage_account

 #Getting PWD from Assets repository
 $pwdtemp = Get-AutomationVariable -Name 'docker_passes'
 $pwd = ""+$pwdtemp.ToString()

 #Creating Object with properties for ARM template parameters
 $parameterObject = @{newStorageAccountName = $arm_storage_account; `
            location = "West Europe"; `
            mysqlPassword =  $pwd; `
            adminUsername = "boss"; `
            adminPassword = $pwd; `
            dnsNameForPublicIP = $arm_pub_name; `            
             }
			 
 #Creating RG with all needed objects via ARM 			
 AzureResourceManager\New-AzureResourceGroup -Name $arm_rg_name `
                            -Location 'West Europe' `
                            -Force `
                            -TemplateUri $TemplateFile `
                            -TemplateParameterObject $parameterObject `
                            -locationFromTemplate 'West Europe'
                            
}
