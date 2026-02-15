# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY Voxen.sln ./
COPY src/Voxen.Server/Voxen.Server.csproj ./Voxen.Server/
RUN dotnet restore Voxen.Server/Voxen.Server.csproj

# Copy everything else
COPY src/ ./   # copies src/Voxen.Server/ and other projects if any

WORKDIR /src/Voxen.Server
RUN dotnet publish -c Release -o /app

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app ./

# Expose port
EXPOSE 5000
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Entry point
ENTRYPOINT ["dotnet", "Voxen.Server.dll"]
