using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            var members = new Member[recs.Count()];

            // This is likely to get much more complicated.

            // This sucks, see if foreach has an overload for adding an iterator
            // python style.
            int i = 0;
            foreach (var rec in recs)
            {
                members[i] = MapToMember(rec);
                i += 1;
            }
        }

        private Member MapToMember(CsvObject rec) => new Member
        {
            Id = "na",
            Name = rec.Member
        };
    }
}
