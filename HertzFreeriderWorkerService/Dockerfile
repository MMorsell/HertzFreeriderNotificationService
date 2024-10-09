# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine as build
WORKDIR /build
COPY HertzFreeriderWorkerService.csproj .
RUN dotnet restore HertzFreeriderWorkerService.csproj
COPY . .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/runtime:9.0-alpine as runtime
WORKDIR /publish
COPY --from=build /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "HertzFreeriderWorkerService.dll"]