# Get base runtime Image from Microsoft
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Get base SDK Image from Microsoft
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the CSPROJ file and restore dependencies (via NUGET)
COPY ["./WebHost/WebHost.csproj", "WebHost/"]
RUN dotnet restore "WebHost/WebHost.csproj"
COPY . .
WORKDIR "/src/WebHost"
RUN dotnet build "WebHost.csproj" -c Release -o /app/build

# Copy the project files and build our release
FROM build AS publish
RUN dotnet publish "WebHost.csproj" -c Release -o /app/publish

# Generate runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebHost.dll"]