using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public string AuthorId { get; set; }
        public string Body { get; set; }
        public System.DateTimeOffset Created { get; set; }
        public System.DateTimeOffset? Updated { get; set; }
        public string UpdateReason { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual BlogPost Post { get; set; }
    }
}