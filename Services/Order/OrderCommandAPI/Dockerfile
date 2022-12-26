FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Order/OrderCommandAPI/OrderCommandAPI.csproj", "OrderCommandAPI/"]
RUN dotnet restore "Services/Order/OrderCommandAPI/OrderCommandAPI.csproj"
COPY . .
WORKDIR "/src/OrderCommandAPI"
RUN dotnet build "OrderCommandAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderCommandAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderCommandAPI.dll"]
