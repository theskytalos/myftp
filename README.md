# MyFTP

### Introduction

This project was made for the Networks class lectured by the Dr. Daniel Ludovico Guidoni in 2019/2 period of the Computing Science at the Federal University of São João del-Rei.

The objective was to develop a client-server architecture application which must be capable of transfering large files over the network using TCP Sockets. The server must've been able to accept multiple clients at runtime.

**Disclaimer**: Even though the project contains "FTP" in it's name, there is absolutely **NO** relation **NOR** compatibility with the real FTP ([RFC 959](https://tools.ietf.org/html/rfc959)).

### Implemented Commands

The commands that are accepted by the server and therefore implemented by the client are:

Command | Effect  
------------ | -------------
login \<username\> \<password\> | Logs in the user. If the username and password has a match in the database, the server returns `OK`, if not or this username is already logged in, the server returns a `FAIL` message.
adduser \<username\> \<password\> | Creates a new user in the database and it's folder at the server file system. If this username is already registered, the server returns a `FAIL` message, `OK` otherwise.
removeuser \<username\> \<password\> | Removes an user from the database and deletes it's folder at the server file system with everything inside it. Returns `OK` if the username and password matches any record in the datbase, `FAIL` otherwise.
changepw \<username\> \<old-password\> \<new-password\> | Changes an already registered username's password. If the pair username and old-password matches any record in the database, the old-password is replaced by the new-password. Returns `OK` if the last described process goes without any problems, `FAIL` otherwise.
put \<file\> | Uploads a file to the server. You must be logged in order to successfully execute this command. The `file` argument is just the name of the file. If this file name ins't already in the user's folder in the server, this command returns `OK`, otherwise it will return `FAIL`. After the `OK` message, the server expects the size in bytes of the file, and right after, the file content bytes sent in `8192` buffer size. After the file is sent, the server returns `OK`.
get \<file\> | Downloads a file from the server. You must be logged in order to successfully execute this command. The `file` argument is just the name of the file which must be in the user's folder at the server. If everything succeeds, the server returns `OK`. Right after the server will sent to the client the size in bytes of the file and then are going to sent the file in `8192` buffer size packets.
delete \<file\> | Deletes a file in the server. ou must be logged in order to successfully execute this command. The `file` argument is just the name of the file which must be in the user's folder at the server. If everything succeeds, the server returns `OK` and the file is deleted from the user's folder at the server. If the file doesn't exists in the user's folder or the client isn't logged in or the file couldn't be deleted for any other reason, the server returns `FAIL` to this command.
ls | Lists all the content inside the user's folder at the server. The client must be logged in order to successfully execute this command. The server returns separated by `:` char all the content in the user's folder.

### External Dependencies

The server uses SQLite database for user control, therefore there is a package for SQLite database manipulation in the server project.

Several icons were used in the client application from [iconfinder](https://www.iconfinder.com/):

* Blue, folder icon by [Paomedia](https://www.iconfinder.com/paomedia)
* Multiple icons from [Lexter Flat ColorFull (file formats)](https://www.iconfinder.com/iconsets/lexter-flat-colorfull-file-formats) by [Honza Dousek](https://www.iconfinder.com/Lexter)

Also, at the server, was used one icon from [iconfinder](https://www.iconfinder.com/) aswell:

* Center, data, rack, server, servers icon by [Alexiuz AS](https://www.iconfinder.com/WHCompare)

### Compiling & Running

To compile this project, Microsoft Visual Studio and Microsoft .NET Framework will be needed.

To run the application, you can run the executable file generated in the Debug folder after the compiling, or compile it as a release.

### Screenshots

![Server Screenshot](https://github.com/theskytalos/myftp/blob/master/myftp-server-prtsc.jpg)

![Client Login Form Screenshot](https://github.com/theskytalos/myftp/blob/master/myftp-client-login-prtsc.jpg)

![Client Main Form Screenshot](https://github.com/theskytalos/myftp/blob/master/myftp-client-uploading-prtsc.jpg)

![Client Settings Form Screenshot](https://github.com/theskytalos/myftp/blob/master/myftp-client-settings-prtsc.jpg)
