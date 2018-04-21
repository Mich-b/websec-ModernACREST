FROM microsoft/aspnetcore:2.0.6 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0.6-2.1.101 AS build
COPY . .
WORKDIR ./ProductAPI
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProductAPI.dll"]
