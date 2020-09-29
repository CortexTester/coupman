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
dotnet ef migrations add -c IdentityDataContext init
dotnet ef migrations add -c DataContext init

dotnet ef database update

***Sep 22***
test coupon controller get all

***Sep 21***
account service -- get all
change current AccountsController to AuthenticationContorller -- breake ui callback
create a new AccountController

********Aug 24 *****
DataContext is need to drop and recreate. becuase partyid and userid setting is wrong, it caused problem. but no time to correct for now

***Sep 5***
config of api and ui cannot be correctly read?
to switch azure and local
1. AccountsContorller.cs line 53 return Redirect("http://klickon.canadacentral.cloudapp.azure.com" + "/#/account/login");
2. AccountService.cs line 96  var resetUrl = appSettings.Client_URL + $@"http://klickon.canadacentral.cloudapp.azure.com/#/account/reset-password?token={decodeToken}&email={model.Email}";
3. environment.ts // export const environment = {
//   production: false,
//   apiUrl: 'http://klickon.canadacentral.cloudapp.azure.com:5051'
// };

***Sep 1st***Identity sample
https://code-maze.com/asp-net-core-identity-series/
https://jasonwatmore.com/post/2020/08/29/angular-10-boilerplate-email-sign-up-with-verification-authentication-forgot-password#account-layout-component-ts
https://jasonwatmore.com/post/2020/07/06/aspnet-core-3-boilerplate-api-with-email-sign-up-verification-authentication-forgot-password
