# See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 8080

# Instalar as bibliotecas ICU
RUN apk add --no-cache icu-libs

# Definir variável de ambiente para habilitar o ICU
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Criar um usuário não root
RUN adduser -D appuser
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EclipseWorks.TaskManagement/EW.TaskManagement.Presentation.csproj", "EclipseWorks.TaskManagement/"]
COPY ["EclipseWorks.TaskManagement.Domain/EW.TaskManagement.Domain.csproj", "EclipseWorks.TaskManagement.Domain/"]
COPY ["EW.TaskManagement.Application/EW.TaskManagement.Application.csproj", "EW.TaskManagement.Application/"]
COPY ["EW.TaskManagement.Infrastructure/EW.TaskManagement.Infrastructure.csproj", "EW.TaskManagement.Infrastructure/"]
RUN dotnet restore "./EclipseWorks.TaskManagement/EW.TaskManagement.Presentation.csproj"
COPY . .
WORKDIR "/src/EclipseWorks.TaskManagement"
RUN dotnet build "./EW.TaskManagement.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EW.TaskManagement.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "EW.TaskManagement.Presentation.dll"]
