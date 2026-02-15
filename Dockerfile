# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution, props, and project file
COPY Voxen-Server.slnx ./
COPY Directory.Build.props ./
COPY src/Voxen.Server/Voxen.Server.csproj ./Voxen.Server/

# Restore dependencies
RUN dotnet restore ./Voxen.Server/Voxen.Server.csproj

# Copy the rest of the source
COPY src/ ./

WORKDIR /src/Voxen.Server
RUN dotnet publish -c Release -o /app

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000
ENV DOTNET_RUNNING_IN_CONTAINER=true

ENTRYPOINT ["dotnet", "Voxen.Server.dll"]
