FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build-env
WORKDIR /app

# copy generated files
COPY . ./

# build project in release mode in out folder
RUN dotnet publish MyBookLibrary.sln -c Release -o out

# build runtime
FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "MyBookLibrary.dll"]