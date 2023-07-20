# specify 'consul' as the first argument to use consul for name resolution
#    "../dapr/config/config.yaml"

dapr run `
    --app-id trafficcontrolservice `
    --app-port 6000 `
    --dapr-http-port 3600 `
    --dapr-grpc-port 60000 `
    --resources-path ../dapr/components `
    dotnet run