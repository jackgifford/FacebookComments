using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Tree
{
    class Program
    {
        //public List<Member> GroupList;
        //private static List<Comment> heads;

        static void Main(string[] args)
        {
            /*
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
            */
        }

        //public static void InsertChild(string parentId, Comment head, Comment child)
        //{
        //    if (head.Id == parentId)
        //        InsertNode(head, child);

        //    else
        //    {
        //        foreach (var headChild in head.Children)
        //        {
        //            InsertChild(parentId, headChild, child);
        //        }
        //    }
        //}


        //public static void GetMembers()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
