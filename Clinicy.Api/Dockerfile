FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Clinicy.WebApi/Clinicy.WebApi.csproj", "Clinicy.WebApi/"]
RUN dotnet restore "Clinicy.WebApi/Clinicy.WebApi.csproj"
COPY . .
WORKDIR "/src/Clinicy.WebApi"
RUN dotnet build "Clinicy.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clinicy.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clinicy.WebApi.dll"]
