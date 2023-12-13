FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY ["Presentation/Inventario.Presentation.csproj", "Presentation/"]
COPY . .
RUN dotnet publish "Presentation/Inventario.Presentation.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 7244
VOLUME /app/data
ENTRYPOINT ["dotnet", "Inventario.Presentation.dll"]

