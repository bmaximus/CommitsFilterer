using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace ConsoleApp2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string commitsPath = @"https://api.github.com/repos/NickeManarin/ScreenToGif/commits?since=2018-03-01T11:11:11T&per_page=10000";
            string tagsPath = @"https://api.stackexchange.com/2.2/tags?order=desc&sort=popular&site=stackoverflow";

            var container = new Dictionary<string, int>();

            var allTags = Get(tagsPath, true);
            var allCommits = Get(commitsPath);
            var allCommitObjects = JsonConvert.DeserializeObject<List<CommitObject>>(allCommits);
            var allTagsObjects = JsonConvert.DeserializeObject<TagObjects>(allTags);
            foreach (var commit in allCommitObjects)
            {
                foreach (var tag in allTagsObjects.items)
                {
                    if (commit.commit.message.Contains(tag.name))
                    {
                        if (container.TryGetValue(tag.name, out var count))
                        {
                            count += 1;
                            container[tag.name] = count;

                        }
                        else
                        {
                            container.Add(tag.name, 1);
                        }
                    }
                }
            }
            foreach (var exists in container)
            {
                Console.WriteLine($"The keyword: {exists.Key} exists: {exists.Value} times");
            }
            Console.ReadLine();
        }

        public static string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.UseDefaultCredentials = true;
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.Referer = "https://api.github.com";
            request.UserAgent = "Mozilla/5.0";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static string Get(string url, bool stackexchange)
        {
            string html = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            return html;
        }

    }
}
