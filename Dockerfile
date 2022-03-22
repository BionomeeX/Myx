FROM bitnami/dotnet-sdk:6

RUN pwd

COPY . /app
WORKDIR /app

RUN dotnet build

WORKDIR bin/Debug/net6.0/