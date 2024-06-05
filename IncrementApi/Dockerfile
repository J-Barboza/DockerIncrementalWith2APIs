# Usar a imagem do .NET SDK para construir o aplicativo
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar o arquivo de projeto e restaurar as dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar o restante dos arquivos e construir o aplicativo
COPY . ./
RUN dotnet publish -c Release -o out

# Usar a imagem do .NET runtime para executar o aplicativo
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "IncrementApi.dll"]
