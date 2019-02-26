## Dokcerfile for build app located in source2 directory 

FROM microsoft/dotnet:sdk AS build-env
ENV http_proxy=http://sunproxy.ux.hra.nycnet:3128
ENV https_proxy=http://sunproxy.ux.hra.nycnet:3128

WORKDIR /app
COPY source2/*.csproj ./
RUN dotnet restore

COPY source2/. ./
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:aspnetcore-runtime
ENV http_proxy=http://sunproxy.ux.hra.nycnet:3128
ENV https_proxy=http://sunproxy.ux.hra.nycnet:3128
WORKDIR /app
COPY --from=build-env /app/out .
COPY sources.list /etc/apt/sources.list
RUN apt-get update -y 
RUN apt-get install apt-utils -y 
RUN apt-get install apt-transport-https -y 
RUN apt-get install msttcorefonts -y
RUN apt-get install -y --reinstall ttf-mscorefonts-installer
RUN apt-get update \
    && apt-get install -y --no-install-recommends libgdiplus libc6-dev \
    && rm -rf /var/lib/apt/lists/*
ENTRYPOINT ["dotnet", "PDFTestCore.dll"]
