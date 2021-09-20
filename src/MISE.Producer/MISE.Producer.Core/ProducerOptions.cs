namespace MISE.Producer.Core
{
    public class ProducerOptions
    {
        public string DefaultCommand { get; set; } = "help";
        //public string OnBeforeMemberInvokeMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SqlSchemaFileTemplate { get; set; } =
            "mise-producer-default-{bridge:category}-{now}.{format}{compression}";
    }
}
