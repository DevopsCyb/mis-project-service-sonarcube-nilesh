# Use the ASP.NET base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base 
WORKDIR /app

# Use the .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["MIS.Services.Project.Api/MIS.Services.Project.Api.csproj", "./MIS.Services.Project.Api/"]
RUN dotnet restore "./MIS.Services.Project.Api/MIS.Services.Project.Api.csproj"

# Copy the entire project and build
COPY . .
WORKDIR "/src/MIS.Services.Project.Api/."
RUN dotnet build "MIS.Services.Project.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "MIS.Services.Project.Api.csproj" -c Release -o /app/publish

# Use the base image for the final stage
FROM base AS final
WORKDIR /app

# Copy the published output from the publish stage
COPY --from=publish /app/publish .
# Copy the appsettings file for the specified environment
ARG ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
# Copy the appsettings file for shrivirajQA environment
COPY ["MIS.Services.Project.Api/appsettings.${ASPNETCORE_ENVIRONMENT}.json", "./appsettings.json"]

# Set the ASPNETCORE_ENVIRONMENT environment variable
#ENV ASPNETCORE_ENVIRONMENT=shrivirajQA

# Set the entry point
ENTRYPOINT ["dotnet", "MIS.Services.Project.Api.dll"]
