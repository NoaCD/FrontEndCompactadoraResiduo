#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FrontEndCompactadoraResiduos/FrontEndCompactadoraResiduos.csproj", "FrontEndCompactadoraResiduos/"]
COPY ["FrontEndCompactadoraResiduos.Bussiness/FrontEndCompactadoraResiduos.Bussiness.csproj", "FrontEndCompactadoraResiduos.Bussiness/"]
COPY ["FrontEndCompactadoraResiduos.Model/FrontEndCompactadoraResiduos.Model.csproj", "FrontEndCompactadoraResiduos.Model/"]
RUN dotnet restore "FrontEndCompactadoraResiduos/FrontEndCompactadoraResiduos.csproj"
COPY . .
WORKDIR "/src/FrontEndCompactadoraResiduos"
RUN dotnet build "FrontEndCompactadoraResiduos.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FrontEndCompactadoraResiduos.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FrontEndCompactadoraResiduos.dll"]