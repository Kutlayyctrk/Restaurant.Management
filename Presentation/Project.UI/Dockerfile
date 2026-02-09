# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/Project.UI/Project.UI.csproj", "Presentation/Project.UI/"]
COPY ["Core/Project.Application/Project.Application.csproj", "Core/Project.Application/"]
COPY ["Core/Project.Contract/Project.Contract.csproj", "Core/Project.Contract/"]
COPY ["Core/Project.Domain/Project.Domain.csproj", "Core/Project.Domain/"]
COPY ["Infrastructure/Project.InnerInfrastructure/Project.InnerInfrastructure.csproj", "Infrastructure/Project.InnerInfrastructure/"]
COPY ["Infrastructure/Project.OuterInfrastructure/Project.OuterInfrastructure.csproj", "Infrastructure/Project.OuterInfrastructure/"]
COPY ["Infrastructure/Project.Persistance/Project.Persistance.csproj", "Infrastructure/Project.Persistance/"]
COPY ["Infrastructure/Project.Validator/Project.Validator.csproj", "Infrastructure/Project.Validator/"]
RUN dotnet restore "./Presentation/Project.UI/Project.UI.csproj"
COPY . .
WORKDIR "/src/Presentation/Project.UI"
RUN dotnet build "./Project.UI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Project.UI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.UI.dll"]
