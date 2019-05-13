using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Tree
{

    public class Comment
    {
        // Tree elements
        public Comment Parent { get; set; }
        public List<Comment> Children { get; set; }


        public string Id { get; set; }
        public Member Author { get; set; }
        public string CommentText { get; set; }

    }

    public class Member
    {
        public string Id { get; set; }
        public string Name { get; set; }
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

            using (var sr = new StreamReader(@"names.csv"))
            {
                string line;
                // Skip the first we don't care
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {

                    var splitLine = line.Split(',');
                    var member = new Member
                    {
                        Id = "na",
                        Name = splitLine[0],

                    };
                        
                    var idRegexr = regex.Matches(splitLine[4]);

                    var id = idRegexr[0].Groups[1].Value;
                    var post = NewComment(member, id, splitLine[0]);

                    heads.Add(post);
                }
            }

            using (var sr = new StreamReader(@"filename.csv"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var splitLine = line.Split(',');
                    var newComment = NewComment(anon, splitLine[0], splitLine[5]);

                    var parentId = (splitLine[3] == string.Empty) ? splitLine[1].Split('_')[1] : splitLine[3];

                    foreach (var head in heads)
                    {
                        InsertChild(parentId, head, newComment);
                    }
                }
            }
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
            child.Parent = parent;
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
