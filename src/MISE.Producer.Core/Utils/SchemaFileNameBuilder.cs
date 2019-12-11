using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;

namespace MISE.Producer.Core.Utils
{
    public enum SchemaFileNameKey
    {
        Category,
        Now,
        Format
    }
    
    public class SchemaFileNameBuilder
    {
        private readonly string _schemaFileNameTemplate;

        private readonly Dictionary<SchemaFileNameKey, Func<string>> _keyValue =
            new Dictionary<SchemaFileNameKey, Func<string>>();

        public SchemaFileNameBuilder(string schemaFileNameTemplate, string category, string format)
        {
            Guard.Against.NullOrEmpty(schemaFileNameTemplate, nameof(schemaFileNameTemplate));
            _schemaFileNameTemplate = schemaFileNameTemplate;
            AddKeyValue(SchemaFileNameKey.Category, () => category);
            AddKeyValue(SchemaFileNameKey.Format, () => format);
            AddKeyValue(SchemaFileNameKey.Now, () => DateTime.Now.ToString("yyyyMMddThhmmss"));
        }

        public SchemaFileNameBuilder AddKeyValue(SchemaFileNameKey key, Func<string> func)
        {
            _keyValue[key] = func;
            return this;
        }

        public string Build()
        {
            // TIP: https://stackoverflow.com/questions/379328/c-sharp-named-parameters-to-a-string-that-replace-to-the-parameter-values

            Regex re = new Regex(@"\{(\w*?)\}", RegexOptions.Compiled);
            
            string fileName = re.Replace(_schemaFileNameTemplate, match =>
            {
                string value = match.Groups[1].Value;
                if (Enum.TryParse(value, out SchemaFileNameKey key))
                    value = _keyValue[key]();
                return value;
            });

            return fileName;
        }
    }
}
