# Welcome to Shopabyte

Shopabyte a online shop demo for development microservices applications. It uses .NET6, EF Core, CQRS, Docker, RabbitMQ, Mediatr, Masstransit, Automapper, gRPC

Before you test it you should run this docker commands;

intialize rabbitmq;
```
docker run -d --hostname my-rabbit --name -p 8080:15672 -p 5672:5672 rabbitmq:3-management
```

intialize redis;
```
docker run -d -p 6379:6379 --name my-redis redis
```

# About the demo
![Application diagram](https://raw.githubusercontent.com/muratalalmis/shopabyte/main/diagrams/export/architecture.png)

The services are uses gRPC when accessing to other. Each service has polygot databases. When you need public api then you should use the rest api.

## About the CQRS implementation
I was implement to Meditr from nuget package store. It is a common way of the cqrs pattern implementation. Lookup to diagram for more information
![mediatr implmentation diagram](https://raw.githubusercontent.com/muratalalmis/shopabyte/main/diagrams/export/ordering-uml.png)