FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["/src/Presentation/NightTasker.Storage.Presentation/NightTasker.Storage.Presentation.csproj", "Presentation/NightTasker.Storage.Presentation/"]
COPY ["/src/Infrastructure/NightTasker.Storage.Infrastructure.Messaging/NightTasker.Storage.Infrastructure.Messaging.csproj", "Infrastructure/NightTasker.Storage.Infrastructure.Messaging/"]
COPY ["/src/Core/NightTasker.Storage.Application/NightTasker.Storage.Application.csproj", "Core/NightTasker.Storage.Application/"]
COPY ["/src/Core/NightTasker.Storage.Domain/NightTasker.Storage.Domain.csproj", "Core/NightTasker.Storage.Domain/"]
COPY ["/src/Infrastructure/NightTasker.Storage.Infrastructure.Persistence/NightTasker.Storage.Infrastructure.Persistence.csproj", "Infrastructure/NightTasker.Storage.Infrastructure.Persistence/"]
RUN dotnet restore "Presentation/NightTasker.Storage.Presentation/NightTasker.Storage.Presentation.csproj"
COPY . .
WORKDIR "/src/Presentation/NightTasker.Storage.Presentation"
RUN dotnet build "NightTasker.Storage.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NightTasker.Storage.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NightTasker.Storage.Presentation.dll"]
