FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY src/. ./src/
COPY .editorconfig .
COPY codechallenge.ruleset .
COPY stylecop.json .

WORKDIR /app/src
RUN dotnet restore
RUN dotnet publish --no-restore -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/src/out .
ENTRYPOINT ["dotnet", "customer-service.dll"]
