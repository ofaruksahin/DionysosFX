using HttpMultipartParser;
using System.Collections.Generic;

namespace DionysosFX.Template.WebAPI.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<FilePart> Files { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FilePart ProfilePhoto { get; set; }
    }
}
