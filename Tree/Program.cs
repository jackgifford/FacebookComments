using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Tree
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

    public class Comment
    {
        public string Id { get; set; }
        public Member Author { get; set; }
        public string CommentText { get; set; }

        // Tree elements
        public Comment Parent { get; set; }
        public List<Comment> Children { get; set; }
    }

    public class Member
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }


    class Program
    {
        public List<Member> GroupList;
        private static List<Comment> heads;

        static void Main(string[] args)
        {
            var anon = new Member
            {
                Id = "anon",
                Name = "Anonymous"
            };

            heads = new List<Comment>();
            var regex = new Regex("([0-9]+)/$");

            Console.WriteLine("Getting names from names.csv");

            using (var sr = new StreamReader(@"names.csv"))
            using (var cs = new CsvReader(sr))
            {
                var records = cs.GetRecords<CsvObject>();
                foreach (var rec in records)
                {
                    var member = new Member
                    {
                        Id = "na",
                        Name = rec.Member,
                    };

                    var idRegexr = regex.Matches(rec.Link);

                    var id = idRegexr[0].Groups[1].Value;
                    var post = NewComment(member, id, rec.Posts);
                    post.Author.Url = rec.Link;

                    heads.Add(post);
                }
            }
            Console.WriteLine("Loaded names from names.csv");
            Console.WriteLine("Getting data from filename.csv");

            using (var sr = new StreamReader(@"filename.csv"))
            using (var cs = new CsvReader(sr))
            {
                var records = cs.GetRecords<Filenames>();
                foreach (var rec in records)
                {
                    var newComment = NewComment(anon, rec.CommentId, rec.CommentText);

                    var parentId = (rec.ParentCommentId == string.Empty) ? rec.PostId.Split('_')[1] : rec.ParentCommentId;

                    foreach (var head in heads)
                    {
                        InsertChild(parentId, head, newComment);
                    }
                }
            }

            Console.WriteLine("Loaded data from filename.csv");

            using (var sw = new StreamWriter("blah.html"))
            {
                foreach (var head in heads)
                {
                    sw.Write(PrettyPrint(head));
                }

                sw.Flush();
            }

            //using (var sw = new StreamWriter(@"test.json"))
            //{
            //    sw.WriteLine(JsonConvert.SerializeObject(heads));
            //    sw.Flush();
            //}

            Console.WriteLine("All Done :)");
        }


        public static string PrettyPrint(Comment head)
        {
            var sb = new StringBuilder();

            sb.AppendLine(@"<ul>");
            sb.AppendLine($"<li>{head.CommentText}</li>");

            foreach (var child in head.Children)
            {
                sb.Append(PrettyPrint(child));
            }
            sb.AppendLine(@"</ul>");

            return sb.ToString();
        }

        public static void InsertChild(string parentId, Comment head, Comment child)
        {
            if (head.Id == parentId)
                InsertNode(head, child);

            else
            {
                foreach (var headChild in head.Children)
                {
                    InsertChild(parentId, headChild, child);
                }
            }
        }

        public static void InsertNode(Comment parent, Comment child)
        {
            // Avoid self referenced loop
            //child.Parent = parent;
            parent.Children.Add(child);
        }

        public static void GetMembers()
        {
            throw new NotImplementedException();
        }

        public static Comment NewComment(Member author, string id, string commentText)
        {
            return new Comment
            {
                Id = id,
                Author = author,
                CommentText = commentText,
                Children = new List<Comment>()
            };
        }
    }
}
