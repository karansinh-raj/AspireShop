targetScope = 'resourceGroup'

@minLength(1)
@maxLength(64)
@description('Name of the environment that can be used as part of naming resource convention, the name of the resource group for your application will use this name, prefixed with rg-')
param environmentName string

@minLength(1)
@description('The location used for all deployed resources')
param location string

@description('Id of the user or app to assign application roles')
param principalId string = ''

@metadata({azd: {
  type: 'generate'
  config: {length:22,noSpecial:true}
  }
})
@secure()
param basketcache_password string
@metadata({azd: {
  type: 'generate'
  config: {length:22}
  }
})
@secure()
param postgres_password string

var tags = {
  'azd-env-name': environmentName
}

module resources 'resources.bicep' = {
  name: 'resources'
  params: {
    location: location
    tags: tags
    principalId: principalId
  }
}

output MANAGED_IDENTITY_CLIENT_ID string = resources.outputs.MANAGED_IDENTITY_CLIENT_ID
output MANAGED_IDENTITY_NAME string = resources.outputs.MANAGED_IDENTITY_NAME
output AZURE_LOG_ANALYTICS_WORKSPACE_NAME string = resources.outputs.AZURE_LOG_ANALYTICS_WORKSPACE_NAME
output AZURE_CONTAINER_REGISTRY_ENDPOINT string = resources.outputs.AZURE_CONTAINER_REGISTRY_ENDPOINT
output AZURE_CONTAINER_REGISTRY_MANAGED_IDENTITY_ID string = resources.outputs.AZURE_CONTAINER_REGISTRY_MANAGED_IDENTITY_ID
output AZURE_CONTAINER_REGISTRY_NAME string = resources.outputs.AZURE_CONTAINER_REGISTRY_NAME
output AZURE_CONTAINER_APPS_ENVIRONMENT_NAME string = resources.outputs.AZURE_CONTAINER_APPS_ENVIRONMENT_NAME
output AZURE_CONTAINER_APPS_ENVIRONMENT_ID string = resources.outputs.AZURE_CONTAINER_APPS_ENVIRONMENT_ID
output AZURE_CONTAINER_APPS_ENVIRONMENT_DEFAULT_DOMAIN string = resources.outputs.AZURE_CONTAINER_APPS_ENVIRONMENT_DEFAULT_DOMAIN
output SERVICE_BASKETCACHE_VOLUME_ASPIRESHOPAPPHOSTA57D47EE87BASKETCACHEDATA_NAME string = resources.outputs.SERVICE_BASKETCACHE_VOLUME_ASPIRESHOPAPPHOSTA57D47EE87BASKETCACHEDATA_NAME
output AZURE_VOLUMES_STORAGE_ACCOUNT string = resources.outputs.AZURE_VOLUMES_STORAGE_ACCOUNT
