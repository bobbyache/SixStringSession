@REM ADD FILES IN ALPHABETICAL ORDER!!!!!!!!!!!!!!!!!!!!!!!
@echo off
SETLOCAL
SET SQLSERVER=%1
SET DATABASE=%2
SET TRUSTED=%3

IF x%1x==xx GOTO :HELP
IF x%DATABASE%x==xx SET DATABASE=SmartSession
IF x%3x==xx SET TRUSTED=Y

IF x%TRUSTED%x==xNx SET CREDENTIALS=-U sa -P bbdadmin
IF x%TRUSTED%x==xEx SET CREDENTIALS=-E

ECHO.
ECHO Applying scripts to %SQLSERVER%.%DATABASE% with credentials %CREDENTIALS%
ECHO.
PAUSE

@echo on
sqlcmd -S %SQLSERVER% %CREDENTIALS% -d %DATABASE% -i "01 - Create Database.sql"
sqlcmd -S %SQLSERVER% %CREDENTIALS% -d %DATABASE% -i "02 - Create Tables.sql"
sqlcmd -S %SQLSERVER% %CREDENTIALS% -d %DATABASE% -i "03 - Populate Lookup Tables.sql"

sqlcmd -S %SQLSERVER% %CREDENTIALS% -d %DATABASE% -i "sp_InsertGoal.sql"
sqlcmd -S %SQLSERVER% %CREDENTIALS% -d %DATABASE% -i "sp_InsertGoalTask.sql"


sqlcmd -S %SQLSERVER% %CREDENTIALS% -d %DATABASE% -i "99 - Insert Test Data.sql"


@echo off
GOTO END

:HELP
ECHO.
ECHO %0 SQLSERVERNAME [DATABASE [E]]
ECHO.
ECHO E for trusted connection otherwise uses sa
ECHO.
ECHO.


:END
