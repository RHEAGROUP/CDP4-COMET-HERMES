# syntax=docker/dockerfile:1.4

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

COPY CDP4.COMET.HERMES CDP4.COMET.HERMES

RUN --mount=type=secret,id=DEVEXPRESS_NUGET_KEY export DEVEXPRESS_NUGET_KEY=$(cat /run/secrets/DEVEXPRESS_NUGET_KEY) \ 
 && dotnet nuget add source https://nuget.devexpress.com/api -n DXFeed -u DevExpress -p ${DEVEXPRESS_NUGET_KEY} --store-password-in-clear-text \
 && dotnet restore CDP4.COMET.HERMES

RUN dotnet build CDP4.COMET.HERMES -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish CDP4.COMET.HERMES -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "CDP4.COMET.HERMES.dll"]