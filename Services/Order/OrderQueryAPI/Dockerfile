FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Order/OrderQueryAPI/OrderQueryAPI.csproj", "OrderQueryAPI/"]
RUN dotnet restore "Services/Order/OrderQueryAPI/OrderQueryAPI.csproj"
COPY . .
WORKDIR "/src/OrderQueryAPI"
RUN dotnet build "OrderQueryAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderQueryAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderQueryAPI.dll"]
