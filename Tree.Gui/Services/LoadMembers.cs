using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tree.Gui.Models;

namespace Tree.Gui.Services
{
    public class LoadMemberService : LoadDataService
    {
        public void LoadMembers(string filePath)
        {
            var dataStream = new FileStream(filePath, FileMode.Open);
            var recs = LoadFromStream<CsvObject>(dataStream);

            var members = new Comment[recs.Count()];

            // This is likely to get much more complicated.

            // This sucks, see if foreach has an overload for adding an iterator
            // python style.
            int i = 0;
            foreach (var rec in recs)
            {
                members[i] = MapHeads(rec);
                i += 1;
            }
        }

        private Comment MapHeads(CsvObject rec)
        {
            var member = MapToMember(rec);

            var regex = new Regex("([0-9]+)/$");

            var idRegexr = regex.Matches(rec.Link);
            var id = idRegexr[0].Groups[1].Value;
            var post = new Comment
            {
                Author = member,
                Id = id,
                CommentText = rec.Posts,
                ParentCommentId = string.Empty,
                Children = new List<Comment>()
            };

            post.Author.Url = rec.Link;

            return post;
        }

        private Member MapToMember(CsvObject rec) => new Member
        {
            Id = "na",
            Name = rec.Member
        };
    }
}
