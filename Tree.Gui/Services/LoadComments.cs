using CsvHelper;
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
        public IEnumerable<Comment> LoadComments(string filePath)
        {
            var dataStream = new FileStream(filePath, FileMode.Open);
            using (var sr = new StreamReader(dataStream))
            using (var cs = new CsvReader(sr))
            {
                var recs = cs.GetRecords<Filenames>(); // LoadFromStream<Filenames>(dataStream);
                var comments = new List<Comment>();

                foreach (var rec in recs)
                {
                    comments.Add(MapToComment(rec));
                }

                return comments;

            }
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
