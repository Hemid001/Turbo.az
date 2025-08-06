# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем csproj файлы
COPY ./TurboProject.APILayer/TurboProject.APILayer.csproj ./TurboProject.APILayer/
COPY ./TurboProject.BusinessLayer/TurboProject.BusinessLayer.csproj ./TurboProject.BusinessLayer/
COPY ./TurboProject.DataLayer/TurboProject.DataLayer.csproj ./TurboProject.DataLayer/
COPY ./TurboProject.DomainLayer/TurboProject.DomainLayer.csproj ./TurboProject.DomainLayer/

# Восстанавливаем зависимости
RUN dotnet restore ./TurboProject.APILayer/TurboProject.APILayer.csproj

# Копируем всё остальное
COPY . .

# Сборка проекта
WORKDIR /src/TurboProject.APILayer
RUN dotnet publish -c Release -o /app/publish

# Этап выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TurboProject.APILayer.dll"]