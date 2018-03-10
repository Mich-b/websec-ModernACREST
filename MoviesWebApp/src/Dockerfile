FROM microsoft/aspnetcore:latest AS base
WORKDIR /app
#This docker image runs the application on port 80
EXPOSE 80

FROM microsoft/aspnetcore-build:latest AS build
WORKDIR /src
COPY *.sln ./
COPY src/MoviesWebApp/MoviesWebApp.csproj src/MoviesWebApp/
COPY src/MoviesLibrary/MoviesLibrary.csproj src/MoviesLibrary/
RUN dotnet restore
COPY . .
WORKDIR /src/src/MoviesWebApp
RUN dotnet publish -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final 
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MoviesWebApp.dll"]
