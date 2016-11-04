FROM microsoft/dotnet:latest

COPY . /app

WORKDIR /app

ENV ASPNETCORE_URLS http://+:80

ENV ASPNETCORE_ENVIRONMENT Production

EXPOSE 80/tcp

CMD ["dotnet", "AHNet.Web.dll", "--server.urls", "http://*:80"]