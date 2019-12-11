namespace MISE.Producer.Infrastructure.RelationalDatabases.SqlServer.SqlStatements
{
    internal static class DomainStatements
    {
        internal static string FromStatement = 
            @"SELECT DOMAIN_CATALOG_ID = DB_ID()
                    ,DOMAIN_CATALOG = DB_NAME()
	                ,DOMAIN_SCHEMA
	                ,DOMAIN_NAME
                    ,DATA_TYPE
                    ,NUMERIC_PRECISION 
                    ,NUMERIC_SCALE
              FROM INFORMATION_SCHEMA.DOMAINS
              UNION ALL
              SELECT DOMAIN_CATALOG_ID = DB_ID()
                    ,DOMAIN_CATALOG = DB_NAME()
	                ,DOMAIN_SCHEMA = SCHEMA_NAME(schema_id)
	                ,DOMAIN_NAME = 'mssql-' + name
                    ,DATA_TYPE = TYPE_NAME(system_type_id)
                    ,NUMERIC_PRECISION = CONVERT(tinyint, CASE -- int/decimal/numeric/real/float/money
		                WHEN system_type_id IN (48, 52, 56, 59, 60, 62, 106, 108, 122, 127) THEN precision END)
		            ,NUMERIC_SCALE = convert(int, CASE -- datetime/smalldatetime  
			            WHEN system_type_id IN (40, 41, 42, 43, 58, 61) THEN NULL  ELSE odbcscale(system_type_id, scale) END)
              FROM    sys.types t
              WHERE   user_type_id <= 256";
    }
}