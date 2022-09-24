@description('The location into which your Azure resources should be deployed.')
param location string = resourceGroup().location

@description('The name of the app service plan.')
param appServicePlanName string = 'blazor-website'

param keyVaultName string = 'blazor-keyvault'

resource keyVault 'Microsoft.KeyVault/vaults@2022-07-01' = {
  name: keyVaultName
  location: location
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: 'bf54c0f1-b0c5-406c-9d9a-013ab0361764'
    accessPolicies: [
      {
        tenantId: 'bf54c0f1-b0c5-406c-9d9a-013ab0361764'
        objectId: 'a0b3ee26-2d00-4384-a60a-e27cbc716ff4'
        permissions: {
          secrets: [
            'Get'
            'List'
            'Set'
          ]
          keys: []
          certificates: []
        }
      }
      {
        tenantId: 'bf54c0f1-b0c5-406c-9d9a-013ab0361764'
        objectId: '77bbd9e2-ad94-4850-922f-d9590310e295'
        permissions: {
          certificates: [
            'Get'
            'List'
            'Update'
            'Create'
            'Import'
            'Delete'
            'Recover'
            'Backup'
            'Restore'
            'ManageContacts'
            'ManageIssuers'
            'GetIssuers'
            'ListIssuers'
            'SetIssuers'
            'DeleteIssuers'
          ]
          keys: [
            'Get'
            'List'
            'Update'
            'Create'
            'Import'
            'Delete'
            'Recover'
            'Backup'
            'Restore'
            'GetRotationPolicy'
            'SetRotationPolicy'
            'Rotate'
          ]
          secrets: [
            'Get'
            'List'
            'Set'
            'Delete'
            'Recover'
            'Backup'
            'Restore'
          ]
        }
      }
    ]
    enabledForDeployment: false
    enabledForDiskEncryption: false
    enabledForTemplateDeployment: true
    enableSoftDelete: true
    softDeleteRetentionInDays: 90
    provisioningState: 'Succeeded'
    publicNetworkAccess: 'Enabled'
  }
}

resource appServiceApp 'Microsoft.Web/sites@2021-01-15' existing = {
  name: 'blazor-website-qcyinfirgk6r4'
}

resource keyVaultAccessPolicy 'Microsoft.KeyVault/vaults/accessPolicies@2019-09-01' = {
  name: '${keyVaultName}/add'
  properties: {
    accessPolicies: [
      {
        tenantId: appServiceApp.identity.tenantId
        objectId: appServiceApp.identity.principalId
        permissions: {
          keys: [
            'get'
          ]
          secrets: [
            'list'
            'get'
          ]
        }
      }
    ]
  }
}


resource connectionStringSecret 'Microsoft.KeyVault/vaults/secrets@2022-07-01' = {
  parent: keyVault
  name: 'ConnectionStrings--DefaultConnection'
  properties: {
    value: 'Test'
    contentType: 'string'
    attributes: {
      enabled: true
    }
  }
}

// var certificateName = 'BlazingCleanArchitectureCertificate1234'

// var certificateCanonicalName = 'blazor-website-qcyinfirgk6r4.azurewebsites.net'

// resource certificate 'Microsoft.Web/certificates@2022-03-01' = {
//   name: certificateName
//   location:  location
//   properties: {
//     canonicalName: certificateCanonicalName
//     domainValidationMethod: 'http-token'
//     keyVaultId: keyVault.id
//     keyVaultSecretName: certificateName
//     serverFarmId: resourceId('Microsoft.Web/serverfarms', appServicePlanName)
//   }
// }

// resource certificate 'Microsoft.KeyVault/vaults/secrets@2022-07-01' = {
//   parent: keyVault
//   name: 'TestCert'
//   properties: {
//     contentType: 'application/x-pkcs12'
//     attributes: {
//       enabled: true
//       nbf: 1663981882
//       exp: 1695518482
//     }
//   }
// }
