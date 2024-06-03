FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS publish
WORKDIR /src
COPY ["ExcelTask.Core/ExcelTask.Core.csproj", "ExcelTask.Core/"]
COPY ["ExcelTask.Api/ExcelTask.Api.csproj", "ExcelTask.Api/"]
RUN dotnet restore --runtime linux-musl-x64  "ExcelTask.Api/ExcelTask.Api.csproj"

COPY . .
WORKDIR /src/ExcelTask.Api
RUN dotnet publish "ExcelTask.Api.csproj" -c Release -o /app/publish --no-restore --runtime linux-musl-x64  --self-contained true  /p:PublishSingleFile=true

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-alpine AS final
WORKDIR /app
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser
ENV HTTP_PORTS=5010
ENV ASPNETCORE_URLS=http://+:5010
EXPOSE 5010
COPY --from=publish /app/publish .

ENTRYPOINT ["./ExcelTask.Api"]