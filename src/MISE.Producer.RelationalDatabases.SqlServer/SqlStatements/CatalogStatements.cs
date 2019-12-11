namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlStatements
{
    internal static class CatalogStatements
    {
        internal static string SELECT_ALL_CATALOGS

            = @"sp_MSforeachdb 
                  @precommand = 'CREATE TABLE ##Resultados (SERVER_NAME nvarchar(50), CATALOG_ID int, CATALOG_NAME sysname, CATALOG_CREATE_DATE datetime, CATALOG_MODIFY_DATE datetime)'
                , @command1 = 'USE [?] INSERT ##Resultados SELECT CONVERT(nvarchar(50), SERVERPROPERTY (''ServerName'')), sysdb.database_id, sysdb.name, sysdb.create_date, sysobj.modify_date
                FROM    [?].sys.databases AS sysdb
                INNER JOIN
                (
                    SELECT  DB_ID() AS database_id
                          , MAX(o.modify_date) AS modify_date
                    FROM    [?].sys.objects o
                    LEFT JOIN [?].sys.schemas s 
                        ON  o.schema_id = s.schema_id
                    LEFT JOIN [?].sys.extended_properties ext
                        ON  ext.class_desc = ''OBJECT_OR_COLUMN''
                        AND ext.major_id = o.object_id
                    WHERE   o.type IN 
                            (
                              ''U'' --USER_TABLE
                            , ''V'' --VIEW
                            , ''TF''--SQL_TABLE_VALUED_FUNCTION
                            , ''P'' --SQL_STORED_PROCEDURE
                            , ''IF''--SQL_INLINE_TABLE_VALUED_FUNCTION
                            )
                        and o.is_ms_shipped = 0
                        and s.name NOT IN (''guest'',''INFORMATION_SCHEMA'',''sys'')
                        and (ext.name NOT IN (''microsoft_database_tools_support'') OR ext.name IS NULL)
                ) sysobj
                    ON  sysobj.database_id = sysdb.database_id'
                , @postcommand = 'SELECT ''SERVER_TEST_MACHINE'' AS SERVER_NAME, CATALOG_ID, CATALOG_NAME, CATALOG_CREATE_DATE, CATALOG_MODIFY_DATE FROM ##Resultados; DROP TABLE ##Resultados'";

        internal static string SELECT_SINGLE_CATALOG
            = @"SELECT 'SERVER_TEST_MACHINE' AS SERVER_NAME, sysdb.database_id AS CATALOG_ID, sysdb.name AS CATALOG_NAME, sysdb.create_date AS CATALOG_CREATE_DATE, sysobj.modify_date AS CATALOG_MODIFY_DATE
                FROM    sys.databases AS sysdb
                INNER JOIN
                (
                    SELECT  DB_ID() AS database_id
                          , MAX(o.modify_date) AS modify_date
                    FROM    sys.objects o
                    LEFT JOIN sys.schemas s 
                        ON  o.schema_id = s.schema_id
                    LEFT JOIN sys.extended_properties ext
                        ON  ext.class_desc = 'OBJECT_OR_COLUMN'
                        AND ext.major_id = o.object_id
                    WHERE   o.type IN 
                            (
                              'U' --USER_TABLE
                            , 'V' --VIEW
                            , 'TF'--SQL_TABLE_VALUED_FUNCTION
                            , 'P' --SQL_STORED_PROCEDURE
                            , 'IF'--SQL_INLINE_TABLE_VALUED_FUNCTION
                            )
                        and o.is_ms_shipped = 0
                        and s.name NOT IN ('guest','INFORMATION_SCHEMA','sys')
                        and (ext.name NOT IN ('microsoft_database_tools_support') OR ext.name IS NULL)
                ) sysobj
                    ON  sysobj.database_id = sysdb.database_id";

    }
}
