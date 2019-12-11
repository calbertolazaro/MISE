namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlStatements
{
    internal static class TableStatements
    {
        internal static string From 
            = @"SELECT    TABLE_SERVER_NAME = SERVERPROPERTY ('ServerName')
                        , TABLE_CATALOG_ID = DB_ID()
                        , TABLE_CATALOG
                        , TABLE_SCHEMA
                        , TABLE_NAME
                        , TABLE_TYPE
                        , TABLE_CREATE_DATE = o.create_date
                        , TABLE_MODIFY_DATE = o.modify_date
                FROM INFORMATION_SCHEMA.TABLES 
                LEFT JOIN sys.objects o
                    ON o.name = TABLES.TABLE_NAME
                    AND o.schema_id = SCHEMA_ID(TABLE_SCHEMA)";

        /*internal static string SELECT_ALL_TABLES
            = @"SELECT    TABLE_SERVER_NAME = SERVERPROPERTY ('ServerName')
                        , TABLE_CATALOG_ID = DB_ID()
                        , TABLE_CATALOG
                        , TABLE_SCHEMA
                        , TABLE_NAME
                        , TABLE_TYPE
                        , TABLE_CREATE_DATE = o.create_date
                        , TABLE_MODIFY_DATE = o.modify_date        
                FROM INFORMATION_SCHEMA.TABLES 
                LEFT JOIN sys.objects o
                    ON o.name = TABLES.TABLE_NAME
                ORDER BY TABLE_SCHEMA, TABLE_NAME";*/
    }
}
