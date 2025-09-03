FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["WebAppCoreMVC.csproj", "./"]
RUN dotnet restore "./WebAppCoreMVC.csproj"
COPY . .
# WORKDIR "/src"
RUN dotnet build "WebAppCoreMVC.csproj" -c Release -o /app/build
# Publish the application
FROM build AS publish
RUN dotnet publish "WebAppCoreMVC.csproj" -c Release -o /app/publish
# Create the final image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAppCoreMVC.dll"]