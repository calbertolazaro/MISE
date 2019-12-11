using System;
using System.ComponentModel.DataAnnotations;

namespace MISE.Producer.Core.RelationalDatabases.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class RelationalDatabaseGatewayMetadataAttribute : Attribute
    {
        /// <summary>
        /// O nome dado ao recurso
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// O nome dado ao recurso
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// A entidade responsável em primeira instância pela existência do recurso
        /// </summary>
        [Required]
        public string Creator { get; set; }
        
        /// <summary>
        /// Uma entidade responsável por tornar o recurso acessível
        /// </summary>
        [Required]
        public string Publisher { get; set; }


    }
}
