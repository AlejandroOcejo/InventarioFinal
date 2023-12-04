FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY ["Presentation/Inventario.Presentation.csproj", "Presentation/"]
COPY . .
RUN dotnet publish "Presentation/Inventario.Presentation.csproj" -c Release -o /app
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 7244
ENTRYPOINT ["dotnet", "Inventario.Presentation.dll"]
