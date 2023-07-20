# specify 'consul' as the first argument to use consul for name resolution
#    "../dapr/config/config.yaml"
# --config $configFile `

dapr run `
    --app-id finecollectionservice `
    --app-port 6001 `
    --dapr-http-port 3601 `
    --dapr-grpc-port 60001 `
    --resources-path ../dapr/components `
    dotnet run