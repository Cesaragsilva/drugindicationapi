FROM mcr.microsoft.com/dotnet/aspnet:8.0.2-alpine3.18 AS base
RUN apk add --no-cache icu-libs
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["/src/DrugIndication.API/DrugIndication.Api.csproj", "DrugIndication.API/"]
COPY ["/src/DrugIndication.Application/DrugIndication.Application.csproj", "DrugIndication.Application/"]
COPY ["/src/DrugIndication.Domain/DrugIndication.Domain.csproj", "DrugIndication.Domain/"]
COPY ["/src/DrugIndication.Infrastructure/DrugIndication.Infrastructure.csproj", "DrugIndication.Infrastructure/"]
RUN dotnet restore "DrugIndication.API/DrugIndication.Api.csproj"
COPY ./src .
WORKDIR "/src/DrugIndication.API"
RUN dotnet build "DrugIndication.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DrugIndication.Api.csproj" -c Release -o /app/publish

FROM base AS final
ENV ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DrugIndication.Api.dll"]