@description('The app service app name')
param appServiceAppName string

@description('The default app settings')
param defaultAppSettings object

@description('The existing app settings (if any)')
param existingAppSettings object

resource appServiceApp 'Microsoft.Web/sites@2021-01-15' existing = {
  name: appServiceAppName
}

resource siteconfig 'Microsoft.Web/sites/config@2022-03-01' = {
  name: 'appsettings'
  parent: appServiceApp
  properties: union(defaultAppSettings, existingAppSettings)
}
