FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Product/ProductCommandAPI/ProductCommandAPI.csproj", "ProductCommandAPI/"]
RUN dotnet restore "Services/Product/ProductCommandAPI/ProductCommandAPI.csproj"
COPY . .
WORKDIR "/src/ProductCommandAPI"
RUN dotnet build "ProductCommandAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductCommandAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductCommandAPI.dll"]
