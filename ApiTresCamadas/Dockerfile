FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app
# Copy the csproj and restore all of the nugets
COPY . /app
RUN dotnet restore src/DevIO.Api/DevIO.Api.csproj
RUN dotnet publish src/DevIO.Api/DevIO.Api.csproj -c Release -o /app/publish 

# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "DevIO.Api.dll"]