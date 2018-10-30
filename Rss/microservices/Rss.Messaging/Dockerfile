FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY microservices/Rss.Messaging/Rss.Messaging.csproj microservices/Rss.Messaging/
COPY microservices/Rss.Writer/Rss.Writer.csproj microservices/Rss.Writer/
COPY src/Rss.Container/Rss.Container.csproj src/Rss.Container/
COPY src/Rss.Persistence.MySQL/Rss.Persistence.MySQL.csproj src/Rss.Persistence.MySQL/
COPY src/Rss.Persistence/Rss.Persistence.csproj src/Rss.Persistence/
COPY src/Rss.CDO/Rss.CDO.csproj src/Rss.CDO/
COPY src/Rss.RabbitMQ/Rss.RabbitMQ.csproj src/Rss.RabbitMQ/
COPY src/Rss.Domain/Rss.Domain.csproj src/Rss.Domain/
COPY src/Rss.Application/Rss.Application.csproj src/Rss.Application/
RUN dotnet restore microservices/Rss.Messaging/Rss.Messaging.csproj
COPY . .
WORKDIR /src/microservices/Rss.Messaging
RUN dotnet build Rss.Messaging.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Rss.Messaging.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Rss.Messaging.dll"]
