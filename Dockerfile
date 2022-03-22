FROM mcr.microsoft.com/dotnet/sdk:6.0

RUN pwd

COPY . /app
WORKDIR /app

RUN dotnet build

WORKDIR bin/Debug/net6.0/