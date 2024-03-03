# AspNetMicroservices

# The Following Section For The Separate Docker Images Used In These Microservices

1. Run MongoDb Container Using Docker In Your Local Machine run below command:

```csharp
docker run -d -p 27017:27017 --name shopping-mongo mongo
```

2. Run Redis Container Using Docker In Your Local Machine run below command:

```csharp
docker run -d -p 6379:6379 --name aspnetrun-redis redis:alpine
```

# Run Docker Compose To Orchestrating AspNetMicroservices

1. At the root directory which include **docker-compose.yml** files, run below command:
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --build
```

2. Stop All Containers Orchestrating Using **docker-compose** , run below command:
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml down
```
