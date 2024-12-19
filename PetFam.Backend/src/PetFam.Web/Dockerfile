FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PetFam.Web/PetFam.Web.csproj", "src/PetFam.Web/"]
COPY ["src/Accounts/PetFam.Accounts.Application/PetFam.Accounts.Application.csproj", "src/Accounts/PetFam.Accounts.Application/"]
COPY ["src/Shared/PetFam.Core/PetFam.Core.csproj", "src/Shared/PetFam.Core/"]
COPY ["src/Shared/PetFam.SharedKernel/PetFam.SharedKernel.csproj", "src/Shared/PetFam.SharedKernel/"]
COPY ["src/Accounts/PetFam.Accounts.Contracts/PetFam.Accounts.Contracts.csproj", "src/Accounts/PetFam.Accounts.Contracts/"]
COPY ["src/Accounts/PetFam.Accounts.Domain/PetFam.Accounts.Domain.csproj", "src/Accounts/PetFam.Accounts.Domain/"]
COPY ["src/Accounts/PetFam.Accounts.Presentation/PetFam.Accounts.Presentation.csproj", "src/Accounts/PetFam.Accounts.Presentation/"]
COPY ["src/Shared/PetFam.Framework/PetFam.Framework.csproj", "src/Shared/PetFam.Framework/"]
COPY ["src/Accounts/PetFam.Accounts.Infrastructure/PetFam.Accounts.Infrastructure.csproj", "src/Accounts/PetFam.Accounts.Infrastructure/"]
COPY ["src/BreedManagement/PetFam.BreedManagement.Application/PetFam.BreedManagement.Application.csproj", "src/BreedManagement/PetFam.BreedManagement.Application/"]
COPY ["src/PetManagement/PetFam.PetManagement.Contracts/PetFam.PetManagement.Contracts.csproj", "src/PetManagement/PetFam.PetManagement.Contracts/"]
COPY ["src/BreedManagement/PetFam.BreedManagement.Domain/PetFam.BreedManagement.Domain.csproj", "src/BreedManagement/PetFam.BreedManagement.Domain/"]
COPY ["src/BreedManagement/PetFam.BreedManagement.Presentation/PetFam.BreedManagement.Presentation.csproj", "src/BreedManagement/PetFam.BreedManagement.Presentation/"]
COPY ["src/BreedManagement/PetFam.BreedManagement.Contracts/PetFam.BreedManagement.Contracts.csproj", "src/BreedManagement/PetFam.BreedManagement.Contracts/"]
COPY ["src/BreedManagement/PetFam.BreedManagement.Infrastructure/PetFam.BreedManagement.Infrastructure.csproj", "src/BreedManagement/PetFam.BreedManagement.Infrastructure/"]
COPY ["src/Discussions/PetFam.Discussions.Presentation/PetFam.Discussions.Presentation.csproj", "src/Discussions/PetFam.Discussions.Presentation/"]
COPY ["src/Discussions/PetFam.Discussions.Infrastructure/PetFam.Discussions.Infrastructure.csproj", "src/Discussions/PetFam.Discussions.Infrastructure/"]
COPY ["src/Discussions/PetFam.Discussions.Application/PetFam.Discussions.Application.csproj", "src/Discussions/PetFam.Discussions.Application/"]
COPY ["src/Discussions/PetFam.Discussions.Domain/PetFam.Discussions.Domain.csproj", "src/Discussions/PetFam.Discussions.Domain/"]
COPY ["src/Files/PetFam.Files.Application/PetFam.Files.Application.csproj", "src/Files/PetFam.Files.Application/"]
COPY ["src/Files/PetFam.Files.Contracts/PetFam.Files.Contracts.csproj", "src/Files/PetFam.Files.Contracts/"]
COPY ["src/Files/PetFam.Files.Presentation/PetFam.Files.Presentation.csproj", "src/Files/PetFam.Files.Presentation/"]
COPY ["src/Files/PetFam.Files.Infrastructure/PetFam.Files.Infrastructure.csproj", "src/Files/PetFam.Files.Infrastructure/"]
COPY ["src/PetManagement/PetFam.PetManagement.Application/PetFam.PetManagement.Application.csproj", "src/PetManagement/PetFam.PetManagement.Application/"]
COPY ["src/PetManagement/PetFam.PetManagement.Domain/PetFam.PetManagement.Domain.csproj", "src/PetManagement/PetFam.PetManagement.Domain/"]
COPY ["src/PetManagement/PetFam.PetManagement.Presentation/PetFam.PetManagement.Presentation.csproj", "src/PetManagement/PetFam.PetManagement.Presentation/"]
COPY ["src/PetManagement/PetFam.PetManagement.Infrastructure/PetFam.PetManagement.Infrastructure.csproj", "src/PetManagement/PetFam.PetManagement.Infrastructure/"]
COPY ["src/VolunteeringApplications/PetFam.VolunteeringApplications.Infrastructure/PetFam.VolunteeringApplications.Infrastructure.csproj", "src/VolunteeringApplications/PetFam.VolunteeringApplications.Infrastructure/"]
COPY ["src/VolunteeringApplications/PetFam.VolunteeringApplications.Application/PetFam.VolunteeringApplications.Application.csproj", "src/VolunteeringApplications/PetFam.VolunteeringApplications.Application/"]
COPY ["src/VolunteeringApplications/PetFam.VolunteeringApplications.Domain/PetFam.VolunteeringApplications.Domain.csproj", "src/VolunteeringApplications/PetFam.VolunteeringApplications.Domain/"]
COPY ["src/VolunteeringApplications/PetFam.VolunteeringApplications.Presentation/PetFam.VolunteeringApplications.Presentation.csproj", "src/VolunteeringApplications/PetFam.VolunteeringApplications.Presentation/"]
COPY ["src/VolunteeringApplications/PetFam.VolunteeringApplications.Contracts/PetFam.VolunteeringApplications.Contracts.csproj", "src/VolunteeringApplications/PetFam.VolunteeringApplications.Contracts/"]
RUN dotnet restore "src/PetFam.Web/PetFam.Web.csproj"
COPY . .
WORKDIR "/src/src/PetFam.Web"
RUN dotnet build "PetFam.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PetFam.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetFam.Web.dll"]
