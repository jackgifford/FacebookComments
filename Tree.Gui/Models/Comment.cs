using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree.Gui.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public Member Author { get; set; }
        public string CommentText { get; set; }

        // Tree elements
        public Comment Parent { get; set; }
        public List<Comment> Children { get; set; }
    }
}
