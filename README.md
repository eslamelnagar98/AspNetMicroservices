# AspNetMicroservices

1. Run MongoDb Container Using Docker In Your Local Machine run below command:
```csharp
docker run -d -p 27017:27017 --name shopping-mongo mongo

2. At the root directory which include **docker-compose.yml** files, run below command:
```csharp
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d