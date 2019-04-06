using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class IndividualCommit
    {
         public string sha { get; set; }
        public string node_id { get; set; }
        public Commit commit { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string comments_url { get; set; }
        public Author1 author { get; set; }
        public Committer1 committer { get; set; }
        public Parent[] parents { get; set; }
        public Stats stats { get; set; }
        public File[] files { get; set; }
    }

   
    public class Stats
    {
        public int total { get; set; }
        public int additions { get; set; }
        public int deletions { get; set; }
    }


    public class File
    {
        public string sha { get; set; }
        public string filename { get; set; }
        public string status { get; set; }
        public int additions { get; set; }
        public int deletions { get; set; }
        public int changes { get; set; }
        public string blob_url { get; set; }
        public string raw_url { get; set; }
        public string contents_url { get; set; }
        public string patch { get; set; }
    }

}
