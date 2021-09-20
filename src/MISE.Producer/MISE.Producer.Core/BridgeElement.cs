namespace MISE.Producer.Core
{
    /// <summary>
    ///  Ponte com os sistemas-fonte de metadata (metadata)
    /// </summary>
    public class BridgeMetadata
    {
        /// <summary>
        /// O nome dado ao recurso (DC Term)
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// A entidade responsável em primeira instância pela existência do recurso (DC Term)
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// Uma descrição do conteúdo do recurso (DC Term)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Uma entidade responsável por tornar o recurso acessível. (DC Term)
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Categoria
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Tipo de objecto a ser criado (AssemblyQualifiedName)
        /// </summary>
        public string TypeToCreate { get; set; }
        /// <summary>
        /// Caminho completo do componente
        /// </summary>
        public string Location { get; set; }
    }
}