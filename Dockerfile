# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything into container
COPY . .

# Restore dependencies
RUN dotnet restore RebelParkrunCup.Server/RebelParkrunCup.Server.csproj

# Publish the app (includes Client and Shared)
RUN dotnet publish RebelParkrunCup.Server/RebelParkrunCup.Server.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Install Google Cloud SDK (includes gsutil)
RUN apt-get update && apt-get install -y curl gnupg \
  && echo "deb [signed-by=/usr/share/keyrings/cloud.google.gpg] http://packages.cloud.google.com/apt cloud-sdk main" \
  | tee -a /etc/apt/sources.list.d/google-cloud-sdk.list \
  && curl https://packages.cloud.google.com/apt/doc/apt-key.gpg \
  | apt-key --keyring /usr/share/keyrings/cloud.google.gpg add - \
  && apt-get update && apt-get install -y google-cloud-sdk

# Copy in startup script
COPY startup.sh /startup.sh
RUN chmod +x /startup.sh

# Copy your build output (from previous stage)
COPY --from=build /app/publish .

EXPOSE 8080

# Run the app
ENTRYPOINT ["/startup.sh"]
