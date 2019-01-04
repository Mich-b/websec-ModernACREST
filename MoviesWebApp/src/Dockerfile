FROM microsoft/dotnet:2.2.0-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80


FROM microsoft/dotnet:2.2.100-sdk AS build
COPY . .
WORKDIR ./src/MoviesWebApp
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app
#workaround for docker on Linux, along with changes in the movieswebapp.csproj file
COPY src/MoviesWebApp/wwwroot/Posters/** /app/wwwroot/Posters/
COPY src/MoviesWebApp/wwwroot/css/** /app/wwwroot/css/
COPY src/MoviesWebApp/wwwroot/js/** /app/wwwroot/js/
#need to find a better way to copy crt and pfx
COPY src/MoviesWebApp/*.crt /app
COPY src/MoviesWebApp/*.pfx /app


FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MoviesWebApp.dll"]
EXPOSE 80