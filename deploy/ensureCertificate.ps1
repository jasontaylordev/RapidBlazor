Param(
    [Parameter(Mandatory)]
    [String]$resourceGroupName,

    [Parameter(Mandatory)]
    [String]$appServiceName,

    [Parameter(Mandatory)]
    [String]$keyVaultName,

    [ValidateNotNullOrEmpty()]
    [ValidateLength(1, 127)]
    [String]$certificateName = "RapidBlazorSigningCert"
)

Write-Host -NoNewLine 🔍 Finding Signing Certificate...
$certificateCount = az keyvault certificate list --vault-name $keyVaultName --query "length([?name == '$certificateName'])"

if ($certificateCount -eq 0) {
    Write-Host Not Found! 🤷
    Write-Host -NoNewLine 🔒 Generating Signing Certificate...
    $certificatePolicy = az keyvault certificate get-default-policy | ConvertFrom-Json
    $certificatePolicy.x509CertificateProperties.subject = "CN=$certificateName"
    $certificatePolicy | ConvertTo-Json -Depth 3 | Out-File certificatePolicy.json
    az keyvault certificate create --vault-name $keyVaultName --name $certificateName --policy `@certificatePolicy.json --output none
    Remove-Item ./certificatePolicy.json
    Write-Host Done! ✅
}
else {
    Write-Host Found! ✅
}

Write-Host -NoNewLine 🔧 Updating App Service Configuration...
$x509ThumbprintHex = az keyvault certificate show --name $certificateName --vault-name $keyVaultName --query "x509ThumbprintHex"

az webapp config appsettings set --resource-group $resourceGroupName --name $appServiceName --settings WEBSITE_LOAD_CERTIFICATES=$x509ThumbprintHex --output none
Write-Host Done! ✅

Write-Host -NoNewLine 🔒 Import Certificate to App Service...
az webapp config ssl import --resource-group $resourceGroupName --name $appServiceName --key-vault $keyVaultName --key-vault-certificate-name $certificateName
Write-Host Done! ✅