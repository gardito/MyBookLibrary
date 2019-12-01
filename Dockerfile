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
CMD bash -c "dotnet ef database update --project MyBookLibrary.Api.dll"
EXPOSE 5001
ENTRYPOINT ["dotnet", "MyBookLibrary.Api.dll", "--environment", "Development", "--urls", "https://0.0.0.0:5001"]