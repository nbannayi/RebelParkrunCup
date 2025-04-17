#!/bin/bash

echo "Starting startup.sh script..."

# Check if gsutil is installed
if ! command -v gsutil &> /dev/null
then
    echo "gsutil not found! Exiting."
    exit 1
fi

echo "Downloading parkrun.db from GCS..."
gsutil cp gs://parkrun-cup-db/parkrun.db /app/parkrun.db

if [ $? -ne 0 ]; then
    echo "Failed to download parkrun.db from GCS"
    exit 1
fi

echo "Database downloaded. Starting app..."
exec dotnet RebelParkrunCup.Server.dll
