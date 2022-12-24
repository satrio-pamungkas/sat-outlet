FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Product/ProductQueryAPI/ProductQueryAPI.csproj", "ProductQueryAPI/"]
RUN dotnet restore "Services/Product/ProductQueryAPI/ProductQueryAPI.csproj"
COPY . .
WORKDIR "/src/ProductQueryAPI"
RUN dotnet build "ProductQueryAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductQueryAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductQueryAPI.dll"]
