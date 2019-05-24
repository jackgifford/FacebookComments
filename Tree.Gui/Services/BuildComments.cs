using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.Gui.Models;

namespace Tree.Gui.Services
{
    public class BuildCommentsService
    {

        public void BuildComments(IEnumerable<Comment> heads, IEnumerable<Comment> comments)
        {
            foreach (var comment in comments)
            {
                var parent = heads.First(x => x.Id == comment.ParentCommentId);

                if (parent != null)
                    parent.Children.Add(comment);
            }
        }
    }
}
