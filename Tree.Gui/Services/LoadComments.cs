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
        public void LoadComments(string filePath)
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

        }

        private Comment MapToComment(Filenames rec) => new Comment
        {
            //Id = rec.Id,
            //Author = rec.CommentAuthor,
            CommentText = rec.CommentText,
            Children = new List<Comment>()
        };
    }
}
