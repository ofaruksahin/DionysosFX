using DionysosFX.Module.OpenApi.Attributes;

namespace DionysosFX.Template.WebAPI.Entities
{
    /// <summary>
    /// 
    /// </summary>
    [Description("User Entity")]
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("User Id")]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Surname { get; set; }        
    }
}
