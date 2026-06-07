FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/HRMS.WebApi/HRMS.WebApi.csproj", "src/HRMS.WebApi/"]
COPY ["src/HRMS.Application/HRMS.Application.csproj", "src/HRMS.Application/"]
COPY ["src/HRMS.Domain/HRMS.Domain.csproj", "src/HRMS.Domain/"]
COPY ["src/HRMS.Infrastructure/HRMS.Infrastructure.csproj", "src/HRMS.Infrastructure/"]
COPY ["src/HRMS.Contracts/HRMS.Contracts.csproj", "src/HRMS.Contracts/"]
COPY ["src/HRMS.Shared/HRMS.Shared.csproj", "src/HRMS.Shared/"]
RUN dotnet restore "src/HRMS.WebApi/HRMS.WebApi.csproj"
COPY . .
WORKDIR "/src/src/HRMS.WebApi"
RUN dotnet build "HRMS.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HRMS.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRMS.WebApi.dll"]
