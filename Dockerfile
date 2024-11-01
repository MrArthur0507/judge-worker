FROM mrarthur0507/judge_dotnet_isolate:1.0 

WORKDIR /app

COPY . . 

RUN dotnet restore JudgeWorker.csproj

RUN dotnet publish JudgeWorker.csproj -c Release -o /app/out 

WORKDIR /app/out

EXPOSE 5002
ENV ASPNETCORE_URLS=http://*:5002
ENV ASPNETCORE_ENVIRONMENT=Development

CMD ["dotnet", "JudgeWorker.dll"]
