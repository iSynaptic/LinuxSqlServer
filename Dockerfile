FROM microsoft/dotnet:1.0.0-preview1
MAINTAINER Jordan Terrell <jterrell@wans.net>

ADD ./src/LinuxSqlServer/. /src/

RUN \
	cd /src && \
	dotnet restore && \
	dotnet build && \
	:

ENTRYPOINT (cd /src; dotnet run)