using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Gui.Models
{
    public class CsvObject
    {
        public string Posts { get; set; }
        public string Member { get; set; }
        public int Comments { get; set; }
        public int Reactions { get; set; }
        public int Views { get; set; }
        public string Link { get; set; }
    }

    public class Filenames
    {
        public string CommentId { get; set; }
        public string PostId { get; set; }
        public string PostContent { get; set; }
        public string ParentCommentId { get; set; }
        public string CommentDate { get; set; }
        public string CommentText { get; set; }
        public string CommentAuthor { get; set; }
        public string CommentAuthorId { get; set; }
        public string CommentLikes { get; set; }
        public string CommentComments { get; set; }
        public string Attachment { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
