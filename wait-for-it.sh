#!/usr/bin/env bash

HOST=$1
PORT=$2
TIMEOUT=${3:-60}

echo "⏳ Esperando a $HOST:$PORT (timeout: ${TIMEOUT}s)..."

for ((i=1;i<=TIMEOUT;i++)); do
    nc -z "$HOST" "$PORT" >/dev/null 2>&1
    result=$?
    if [ $result -eq 0 ]; then
        echo "✅ $HOST:$PORT está disponible."
        exec "${@:4}"
        exit 0
    fi
    sleep 1
done

echo "❌ Timeout de ${TIMEOUT}s esperando a $HOST:$PORT"
exit 1
