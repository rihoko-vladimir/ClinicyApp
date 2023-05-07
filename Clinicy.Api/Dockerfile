FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Clinicy.Api/Clinicy.Api.csproj", "Clinicy.Api/"]
RUN dotnet restore "Clinicy.Api/Clinicy.Api.csproj"
COPY . .
WORKDIR "/src/Clinicy.Api"
RUN dotnet build "Clinicy.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clinicy.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clinicy.Api.dll"]
