#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AspLearningProject/AspLearningProject.csproj", "AspLearningProject/"]
RUN dotnet restore "AspLearningProject/AspLearningProject.csproj"
COPY . .
WORKDIR "/src/AspLearningProject"
RUN dotnet build "AspLearningProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspLearningProject.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspLearningProject.dll"]