# Consultez https://aka.ms/customizecontainer pour savoir comment personnaliser votre conteneur de débogage et comment Visual Studio utilise ce Dockerfile pour générer vos images afin d’accélérer le débogage.

# En fonction du système d’exploitation du ou des ordinateurs hôtes qui génèrent ou exécutent les conteneurs, vous devrez peut-être modifier l’image spécifiée dans l’instruction FROM.
# Pour obtenir plus d’informations, veuillez consulter https://aka.ms/containercompat

# Cet index est utilisé lors de l’exécution à partir de VS en mode rapide (par défaut pour la configuration de débogage)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Cette phase est utilisée pour générer le projet de service
FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PFE2024-QUIZZ-API/PFE2024-QUIZZ-API.csproj", "PFE2024-QUIZZ-API/"]
RUN dotnet restore "./PFE2024-QUIZZ-API/PFE2024-QUIZZ-API.csproj"
COPY . .
WORKDIR "/src/PFE2024-QUIZZ-API"
RUN dotnet build "./PFE2024-QUIZZ-API.csproj" -c %BUILD_CONFIGURATION% -o /app/build

# Cette étape permet de publier le projet de service à copier dans la phase finale
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PFE2024-QUIZZ-API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

# Cette phase est utilisée en production ou lors de l’exécution à partir de VS en mode normal (par défaut quand la configuration de débogage n’est pas utilisée)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PFE2024-QUIZZ-API.dll"]