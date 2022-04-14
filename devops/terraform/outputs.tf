output "micApiId" {
  value = module.micapi.micAppApplicationClientId
}

output "micReadClientId" {
  value = module.micapiclient.micAppReadClientApplicationClientId
}

output "micWriteClientId" {
  value = module.micapiclient.micAppWriteClientServicePrincipalClientId
}
