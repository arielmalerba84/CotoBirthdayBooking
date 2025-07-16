# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["CotoBirthdayBooking.Api/CotoBirthdayBooking.Api.csproj", "CotoBirthdayBooking.Api/"]
RUN dotnet restore "CotoBirthdayBooking.Api/CotoBirthdayBooking.Api.csproj"

COPY . .
WORKDIR "/src/CotoBirthdayBooking.Api"
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 8081

CMD ["dotnet", "CotoBirthdayBooking.Api.dll"]
