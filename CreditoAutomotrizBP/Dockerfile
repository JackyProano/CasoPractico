FROM bancopichinchaec.azurecr.io/bp/cross-iac/dotnet/dotnet-31-runtime-rhel7:latest

COPY ./publish/. .

CMD ["dotnet", "nombremicroservicio.API.dll"]