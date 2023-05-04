FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Clinicy.Auth/Clinicy.Auth.csproj", "Clinicy.Auth/"]
RUN dotnet restore "Clinicy.Auth/Clinicy.Auth.csproj"
COPY . .
WORKDIR "/src/Clinicy.Auth"
RUN dotnet build "Clinicy.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clinicy.Auth.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clinicy.Auth.dll"]
