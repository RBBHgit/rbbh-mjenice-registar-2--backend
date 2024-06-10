FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Raiffeisen.RegistarMjenicaBackend/Raiffeisen.RegistarMjenicaBackend.csproj", "Raiffeisen.RegistarMjenicaBackend/"]
COPY ["Raiffeisen.RegistarMjenicaBackend.DataAccess/Raiffeisen.RegistarMjenicaBackend.DataAccess.csproj", "Raiffeisen.RegistarMjenicaBackend.DataAccess/"]
COPY ["Raiffeisen.RegistarMjenicaBackend.Services/Raiffeisen.RegistarMjenicaBackend.Services.csproj", "Raiffeisen.RegistarMjenicaBackend.Services/"]
RUN dotnet restore "Raiffeisen.RegistarMjenicaBackend/Raiffeisen.RegistarMjenicaBackend.csproj"
COPY . .
WORKDIR "/src/Raiffeisen.RegistarMjenicaBackend"
RUN dotnet build "Raiffeisen.RegistarMjenicaBackend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Raiffeisen.RegistarMjenicaBackend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Raiffeisen.RegistarMjenicaBackend.dll"]
