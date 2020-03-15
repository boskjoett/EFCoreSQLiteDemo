FROM mcr.microsoft.com/dotnet/core/runtime:3.1

WORKDIR /app

# Copy binaries from local relative path to WORKDIR in image
COPY SqLiteDemo/bin/Release/netcoreapp3.1/publish/ ./

# Copy database file to /database in image
COPY Database/blogging.db /database/blogging.db

ENTRYPOINT ["dotnet", "SqLiteDemo.dll"]
