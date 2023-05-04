FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Clinicy.ImportExport/Clinicy.ImportExport.csproj", "Clinicy.ImportExport/"]
RUN dotnet restore "Clinicy.ImportExport/Clinicy.ImportExport.csproj"
COPY . .
WORKDIR "/src/Clinicy.ImportExport"
RUN dotnet build "Clinicy.ImportExport.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Clinicy.ImportExport.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clinicy.ImportExport.dll"]
