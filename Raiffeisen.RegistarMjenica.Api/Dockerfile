FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Raiffeisen.RegistarMjenica.Api/Raiffeisen.RegistarMjenica.Api.csproj", "Raiffeisen.RegistarMjenica.Api/"]
COPY ["Raiffeisen.RegistarMjenica.Api.DataAccess/Raiffeisen.RegistarMjenica.Api.DataAccess.csproj", "Raiffeisen.RegistarMjenica.Api.DataAccess/"]
COPY ["Raiffeisen.RegistarMjenica.Api.Services/Raiffeisen.RegistarMjenica.Api.Services.csproj", "Raiffeisen.RegistarMjenica.Api.Services/"]
RUN dotnet restore "Raiffeisen.RegistarMjenica.Api/Raiffeisen.RegistarMjenica.Api.csproj"
COPY . .
WORKDIR "/src/Raiffeisen.RegistarMjenica.Api"
RUN dotnet build "Raiffeisen.RegistarMjenica.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Raiffeisen.RegistarMjenica.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Raiffeisen.RegistarMjenica.Api.dll"]
