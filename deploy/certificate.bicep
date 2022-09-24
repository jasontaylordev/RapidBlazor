@description('The location into which your Azure resources should be deployed.')
param location string = resourceGroup().location

@description('The name of the app service plan.')
param appServicePlanName string = 'blazor-website'

@description('The keyvault id')
param keyVaultId string = '/subscriptions/49cfbc2b-0e46-408a-8fa5-607cbb9120df/resourceGroups/BlazorWebsiteTest/providers/Microsoft.KeyVault/vaults/blazor-keyvault'

var certificateName = 'BlazingCleanArchitectureCertificate123'

var certificateCanonicalName = 'blazor-website-qcyinfirgk6r4.azurewebsites.net'

resource certificate 'Microsoft.Web/certificates@2022-03-01' = {
  name: certificateName
  location:  location
  properties: {
    canonicalName: certificateCanonicalName
    domainValidationMethod: 'http-token'
    keyVaultId: keyVaultId
    keyVaultSecretName: certificateName
    serverFarmId: resourceId('Microsoft.Web/serverfarms', appServicePlanName)
  }
}
