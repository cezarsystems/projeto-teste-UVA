USE [C:\USERS\CEZAR LISBOA - DEV\DOCUMENTS\DBCADASTROUVA.MDF]

CREATE LOGIN admuva WITH PASSWORD = 'senha$01';
CREATE USER admuva FOR LOGIN admuva;
EXEC sp_addrolemember 'db_owner', 'admuva'