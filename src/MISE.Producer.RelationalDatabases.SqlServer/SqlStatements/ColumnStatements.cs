namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlStatements
{
    internal static class ColumnStatements
    {
        /*internal static string SELECT_ALL_COLUMNS
               = @"SELECT     TABLE_SERVER_NAME = SERVERPROPERTY ('ServerName')
                            , TABLE_CATALOG_ID = DB_ID()
                            , TABLE_CATALOG = c.TABLE_CATALOG
                            , TABLE_SCHEMA = c.TABLE_SCHEMA
                            , TABLE_NAME = c.TABLE_NAME
                            , COLUMN_NAME = c.COLUMN_NAME
                            , ORDINAL_POSITION
                            , DOMAIN_NAME
                            , DATA_TYPE
                            , CHARACTER_MAXIMUM_LENGTH
                            , NUMERIC_PRECISION
                            , NUMERIC_SCALE
                            , COLUMN_DEFAULT
                            , IS_NULLABLE
                            , IS_PRIMARY_KEY = CASE WHEN pk.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END
                            , IS_FOREIGN_KEY = CASE WHEN fk.COLUMN_NAME IS NOT NULL THEN 1 ELSE 0 END
                    FROM    INFORMATION_SCHEMA.COLUMNS AS c
                    LEFT JOIN 
                    (
                        SELECT  ku.TABLE_CATALOG, ku.TABLE_SCHEMA, ku.TABLE_NAME, ku.COLUMN_NAME
                        FROM    INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ku
                            ON  tc.CONSTRAINT_TYPE = 'PRIMARY KEY' 
                            AND tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME
                    ) pk 
                        ON  c.TABLE_CATALOG = pk.TABLE_CATALOG
                        AND c.TABLE_SCHEMA = pk.TABLE_SCHEMA
                        AND c.TABLE_NAME = pk.TABLE_NAME
                        AND c.COLUMN_NAME = pk.COLUMN_NAME
                    LEFT JOIN 
                    (
                        SELECT ku.TABLE_CATALOG, ku.TABLE_SCHEMA, ku.TABLE_NAME, ku.COLUMN_NAME
                        FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ku
                            ON tc.CONSTRAINT_TYPE = 'FOREIGN KEY' 
                            AND tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME
                    ) fk
                        ON  c.TABLE_CATALOG = fk.TABLE_CATALOG
                        AND c.TABLE_SCHEMA = fk.TABLE_SCHEMA
                        AND c.TABLE_NAME = fk.TABLE_NAME
                        AND c.COLUMN_NAME = fk.COLUMN_NAME
                ORDER BY TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, c.ORDINAL_POSITION";*/

        internal static string FromStatement =
                 @" SELECT TABLE_SERVER_NAME
                          ,TABLE_CATALOG_ID 
                          ,TABLE_CATALOG
                          ,TABLE_SCHEMA
                          ,TABLE_NAME
                          ,COLUMN_NAME
                          ,ORDINAL_POSITION
                          ,DOMAIN_SCHEMA                          
                          ,DOMAIN_NAME
                          ,DATA_TYPE
                          ,CHARACTER_MAXIMUM_LENGTH
                          ,NUMERIC_PRECISION
                          ,NUMERIC_SCALE
                          ,COLUMN_DEFAULT
                          ,IS_NULLABLE
                          ,IS_PRIMARY_KEY
                          ,IS_FOREIGN_KEY 
                    FROM
                    (
                        SELECT    TABLE_SERVER_NAME = SERVERPROPERTY ('ServerName')
                                , TABLE_CATALOG_ID = DB_ID()
                                , TABLE_CATALOG = c.TABLE_CATALOG
                                , TABLE_SCHEMA = c.TABLE_SCHEMA
                                , TABLE_NAME = c.TABLE_NAME
                                , COLUMN_NAME = c.COLUMN_NAME
                                , ORDINAL_POSITION
                                , DOMAIN_SCHEMA
                                , DOMAIN_NAME                                
                                , DATA_TYPE
                                , CHARACTER_MAXIMUM_LENGTH
                                , NUMERIC_PRECISION
                                , NUMERIC_SCALE
                                , COLUMN_DEFAULT
                                , IS_NULLABLE
                                , IS_PRIMARY_KEY = CONVERT(varchar(3), CASE WHEN pk.COLUMN_NAME IS NOT NULL THEN 'YES' ELSE 'NO' END)
                                , IS_FOREIGN_KEY = CONVERT(varchar(3), CASE WHEN fk.COLUMN_NAME IS NOT NULL THEN 'YES' ELSE 'NO' END)
                        FROM    INFORMATION_SCHEMA.COLUMNS AS c
                        LEFT JOIN 
                        (
                            SELECT  ku.TABLE_CATALOG, ku.TABLE_SCHEMA, ku.TABLE_NAME, ku.COLUMN_NAME
                            FROM    INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
                            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ku
                                ON  tc.CONSTRAINT_TYPE = 'PRIMARY KEY' 
                                AND tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME
                        ) pk 
                            ON  c.TABLE_CATALOG = pk.TABLE_CATALOG
                            AND c.TABLE_SCHEMA = pk.TABLE_SCHEMA
                            AND c.TABLE_NAME = pk.TABLE_NAME
                            AND c.COLUMN_NAME = pk.COLUMN_NAME
                        LEFT JOIN 
                        (
                            SELECT ku.TABLE_CATALOG, ku.TABLE_SCHEMA, ku.TABLE_NAME, ku.COLUMN_NAME
                            FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
                            INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS ku
                                ON tc.CONSTRAINT_TYPE = 'FOREIGN KEY' 
                                AND tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME
                        ) fk
                            ON  c.TABLE_CATALOG = fk.TABLE_CATALOG
                            AND c.TABLE_SCHEMA = fk.TABLE_SCHEMA
                            AND c.TABLE_NAME = fk.TABLE_NAME
                            AND c.COLUMN_NAME = fk.COLUMN_NAME
            ) QRY_COLUMN";
    }
}
