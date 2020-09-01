todo 
1. make sure postgresql working with EF
2. ui talk with api
3. docker file to build images
4. docker compose file to run together db, api and ui   

***generate sql schema script 
1. dotnet ef migrations script -o ./script/start.sql
2. add create db on top
    CREATE DATABASE coupman; 
    \c coupman
3.  add volume to docker-compose.yml. under script folder can have multi sql script and order name by 0-init.sql , 1-update.sql etc 
    volumes:
        - ./script:/docker-entrypoint-initdb.d

***build images
cd webapi -- docker build -t tester8cortex/coupman-api:0.0.1 .
cd webui -- docker build -t tester8cortex/coupman-ui:0.0.1 .
cd coupman docker-compose up

*** 
http://calman.canadacentral.cloudapp.azure.com:8080/

1. database host in api -- workround is using docker-compose name
2. database is not exist --when api up,


***entity framework 
dotnet ef migrations add Initial
dotnet ef database update



********Aug 24 *****
DataContext is need to drop and recreate. becuase partyid and userid setting is wrong, it caused problem. but no time to correct for now
