using CsvHelper;
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
        public IEnumerable<Comment> LoadMembers(string filePath)
        {
            using (var sr = new StreamReader(filePath))
            using (var cs = new CsvReader(sr))
            {
                var recs = cs.GetRecords<CsvObject>(); 
                var list = new List<Comment>();

                foreach (var rec in recs)
                {
                    list.Add(MapHeads(rec));
                }

                return list;
            }
        }

        private Comment MapHeads(CsvObject rec)
        {
            var member = MapToMember(rec);

            //var regex = new Regex("([0-9]+)/$");
            var regex = new Regex("([0-9]+)");

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
