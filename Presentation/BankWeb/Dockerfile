#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Presentation/BankWeb/BankWeb.csproj", "Presentation/BankWeb/"]
COPY ["Business/Domain/Domain.csproj", "Business/Domain/"]
COPY ["Infrastructure/Persistence/Persistence.csproj", "Infrastructure/Persistence/"]
COPY ["Business/Application/Application.csproj", "Business/Application/"]
COPY ["Infrastructure/Bootstrap/Bootstrap.csproj", "Infrastructure/Bootstrap/"]
COPY ["Tests/Unit/Bank.Domain.Test/Bank.Domain.Test.csproj", "Tests/Unit/Bank.Domain.Test/"]
RUN dotnet restore "Presentation/BankWeb/BankWeb.csproj"
COPY . .
WORKDIR "/src/Presentation/BankWeb"
RUN dotnet build "BankWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BankWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BankWeb.dll"]