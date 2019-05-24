using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tree.Gui.Models;

namespace Tree.Gui.Services
{
    public class LoadCommentsService : LoadDataService
    {
        public Comment[] LoadComments(string filePath)
        {
            var dataStream = new FileStream(filePath, FileMode.Open);
            var recs = LoadFromStream<Filenames>(dataStream);

            var comments = new Comment[recs.Count()];

            int i = 0;
            foreach (var rec in recs)
            {
                comments[i] = MapToComment(rec);
                i += 1;
            }

            return comments;
        }

        private Comment MapToComment(Filenames rec) => new Comment
        {
            Id = rec.CommentId,
            //Author = rec.CommentAuthor,
            ParentCommentId = (rec.ParentCommentId == string.Empty) ? rec.PostId.Split('_')[1] : rec.ParentCommentId,
            CommentText = rec.CommentText,
            Children = new List<Comment>()
        };
    }
}
