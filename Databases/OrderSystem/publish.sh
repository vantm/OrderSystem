#!/bin/bash

DACPAC=bin/Debug/OrderSystem.dacpac

if [ "$1" != "--no-build" ]; then
    dotnet build -c Debug
fi

dotnet sqlpackage \
    /Action:Publish \
    /Quiet:False \
    /SourceFile:$DACPAC \
    /TargetServerName:localhost \
    /TargetDatabaseName:OrderSystem \
    /TargetUser:sa \
    /TargetPassword:P@ssw0rd \
    /TargetTrustServerCertificate:True