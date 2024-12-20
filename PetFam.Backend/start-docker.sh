#!/bin/bash

if [ "$1" == "dev" ]; then
  docker-compose -f docker-compose.Development.yml up -d
elif [ "$1" == "docker" ]; then
  docker-compose -f docker-compose.yml up -d
else
  echo "Usage: $0 [dev|docker]"
fi