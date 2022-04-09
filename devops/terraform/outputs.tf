output "micApiId" {
  value = module.micapplicationobject.micAppApplicationClientId
}

output "micReadClientId" {
  value = module.micapplicationobjectclient.micAppReadClientApplicationClientId
}

output "micWriteClientId" {
  value = module.micapplicationobjectclient.micAppWriteClientServicePrincipalClientId
}
