This repository contains code to reproduce an OpenSSL issue connecting to SQL server from .NET Core 1.0 RC2. The version of SQL Server this is known to reproduce is `10.50.1720.0`.

##Steps to Reproduce##

1. This is an optional step, useful if you are NOT already running on a Linux machine with docker installed. Run `vagrant up` in the repository root. When prompted, select an network adapter that has connectivity to a running SQL server that will be used to reproduce this issue. Run `vagrant ssh` to connect to the Vagrant built VM.
1. Navigate to the directory containing the source code for this application.  If within Vagrant VM, enter `cd /vagrant`.
1. Build the Docker container by entering `sudo docker build -t lss .`.
1. Run the Docker container interactively by entering `sudo docker run -i --rm lss`.
1. When prompted, enter or paste a connection string to a running SQL server. This error has been reproduced using a connection that is authenticated using SQL Server Authentication.
1. When prompted, enter or paste a simple `SELECT` query (e.g. `SELECT TOP 10 * FROM Table`).

## Expected Results ##
The console app should connect to the database, execute the query, and dump the results to the console.

## Actual Results ##
The console app attempts to connect to the database and fails, reporting the following exception:

> Unhandled Exception: System.Data.SqlClient.SqlException: A connection was successfully established with the server, but then an error occurred during the pre-login handshake. (provider: TCP Provider, error: 35 - An internal exception was caught) ---System.Security.Authentication.AuthenticationException: A call to SSPI failed, see inner exception. ---Interop+OpenSsl+SslException: SSL Handshake failed with OpenSSL error - SSL_ERROR_SSL. ---Interop+Crypto+OpenSslCryptographicException: error:14077102:SSL routines:SSL23_GET_SERVER_HELLO:unsupported protocol
>    --- End of inner exception stack trace ---
>    at Interop.OpenSsl.DoSslHandshake(SafeSslHandle context, Byte[] recvBuf, Int32 recvOffset, Int32 recvCount, Byte[]& sendBuf, Int32& sendCount)
>    at System.Net.SslStreamPal.HandshakeInternal(SafeFreeCredentials credential, SafeDeleteContext& context, SecurityBuffer inputBuffer, SecurityBuffer outputBuffer, Boolean isServer, Boolean remoteCertRequired)