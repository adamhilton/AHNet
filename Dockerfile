FROM microsoft/aspnetcore-build:1.0.1
COPY . /app
WORKDIR /app/src/AHNet.Web
ENV ASPNETCORE_URLS http://+:80
ENV ASPNETCORE_ENVIRONMENT Production
EXPOSE 80/tcp
RUN ["dotnet", "restore"]
ENTRYPOINT ["sh", "run.sh"]