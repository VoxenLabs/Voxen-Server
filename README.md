# Voxen Server

The Voxen Server is currently intended for **development use only**.
No official release has been published yet.

At this time, Docker images are not pre-built and must be built locally.
Pre-built images will be provided in a future release.

## Prerequisites

Make sure you have the following tools installed:

* Git
* Docker
* Docker Compose (included with modern Docker installations)

## Getting started

### Initialize Docker Image

#### 1. Clone the repository

```bash
git clone https://github.com/VoxenLabs/Voxen-Server.git
```

#### 2. Navigate to the Project Directory

```bash
cd Voxen-Server
```

#### 3. Build and Run the Development Environment

```bash
docker compose -f docker-compose.development.yml up -d
```

This will build the required Docker images locally and start the server in detached mode. If succesful, The **Swagger** environment will be reachable on http://localhost:5000/swagger

### Authenticate user

On creation of the container, a default admin-user will be initialized with the following credentials:
* Username: `admin`
* Password: `Password123!`

To authenticate the **Swagger** environment, make a call to the `/auth/login` endpoint with the above credentials to retrieve a JWT token. Then, use the "Authorize" button in the Swagger UI to input the token and gain access to authenticated endpoints.

## Development Instructions

## Migrations

Since no official version has been released yet, database migrations are not necessary and a new initialization must be performed on each database change. However, as the project evolves and more features are added, database migrations will become necessary at a later stage to manage changes to the database schema.

To run database initialization, start by delete the `.db` files in `src/bundles/Voxen.Server`. Then navigate to the repository root and run the following command:
```bash
dotnet ef migrations add InitialCreate --project .\src\modules\Voxen.Server.Domain --startup-project .\src\bundles\Voxen.Server
```

A new database will be created with the updated schema and the initial data, which is usable both during debug in your chosen IDE, and on testing the generated Docker Image.

## SignalR connection

Voxen Server uses SignalR to provide real-time updates for channel messages. When a user sends a message, all connected clients in that channel will instantly receive it.

### Hub Endpoint

The SignalR hub for text channels is available at `http://localhost:5000/channels/connect`

### Kotlin Multiplatform Example Usage

#### 1. Add Dependencies

```kotlin
implementation("com.microsoft.signalr:signalr:7.0.0")
```

#### 2. Connect to the Hub

```kotlin
val connection = HubConnectionBuilder
    .create("https://your-api/channels/connect")
    .withAccessTokenProvider {
        Single.just("your-jwt-token")
    }
    .build()

connection.start().blockingAwait()
```

#### 3. Connect to the Hub

```kotlin
connection.invoke("JoinChannel", channelId)
```

#### 4. Listen for Messages

```kotlin
connection.on(
    "ReceiveMessage",
    { message: MessageDto ->
        println("New message: ${message.content}")
    },
    MessageDto::class.java
)
```

#### 5. Leave a Channel

```kotlin
connection.invoke("LeaveChannel", channelId)
```

## Notes

* This setup is not intended for production use.
* Configuration, APIs, and behavior are prone to change.
* Documentation will expand as the project approaches its first official release.
