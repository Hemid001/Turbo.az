# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем csproj и восстанавливаем зависимости
COPY TurboProject.APILayer/TurboProject.APILayer.csproj ./TurboProject.APILayer/
COPY TurboProject.BusinessLayer/TurboProject.BusinessLayer.csproj ./TurboProject.BusinessLayer/
COPY TurboProject.DataLayer/TurboProject.DataLayer.csproj ./TurboProject.DataLayer/
COPY TurboProject.DomainLayer/TurboProject.DomainLayer.csproj ./TurboProject.DomainLayer/

RUN dotnet restore ./TurboProject.APILayer/TurboProject.APILayer.csproj

# Копируем весь остальной код и собираем
COPY . .
WORKDIR /src/TurboProject.APILayer
RUN dotnet publish -c Release -o /app/publish

# Этап запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TurboProject.APILayer.dll"]