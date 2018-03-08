using Gov.News.Archive.Models;
using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;
using MongoDB.Bson;
using Gov.News.Archive;

namespace Gov.News.Archive.Importer
{
    class Program
    {

        static void ImportDirectory (Models.Collection c, string sourceDir, Client newsArchiveApiClient)
        {
            // spin through all of the htm files in the directory.
            string[] paths = Directory.GetFiles(sourceDir, "*.htm", SearchOption.TopDirectoryOnly);

            foreach (string path in paths)
            {
                string filename = Path.GetFileName(path);
                Console.Out.WriteLine(filename);

                var doc = new HtmlDocument();
                doc.Load(path);

                // get some meta data.

                var node = doc.DocumentNode;

                var title = node.SelectSingleNode("//title")
                    .InnerText;
                string ministry = "Unknown";

                var tempMinistry = node.SelectSingleNode("//td[@style='width: 70%; text-align: right; vertical-align: top;']");
                if (tempMinistry != null)
                {
                    ministry = tempMinistry.InnerText;
                }

                string preview = "";
                string body = "";

                var tempBody = node.SelectSingleNode("//td[@colspan='2' and @style='padding-top: 20px;']");
                if (tempBody != null)
                {
                    body = tempBody.InnerHtml;
                    var firstParagraph = tempBody.SelectSingleNode("//p");
                    if (firstParagraph != null)
                    {
                        preview = firstParagraph.InnerText;
                    }
                }

                DateTime dt = DateTime.Now;

                var tempDate = node.SelectSingleNode("//meta[@name='dc.date']");
                if (tempDate != null)
                {
                    dt = DateTime.Parse(tempDate.Attributes["content"].Value);
                }


                Console.Out.WriteLine("title=" + title);
                Console.Out.WriteLine("ministry=" + ministry);
                Console.Out.WriteLine("datePublished" + dt.ToShortDateString());

                // get the text content.

                string temp = path.Replace (Path.GetExtension(filename),"");

                string textFilename = temp + ".txt";
              
                string pdfFilename = temp + ".pdf";

                // read the text file into a variable.
                string textContent = File.ReadAllText(textFilename);

                string htmlContent = File.ReadAllText(path);


                Models.ArchiveModel payload = new Models.ArchiveModel();

                payload.Collection = c;
                payload.DateReleased = dt;
                payload.MinistryText = ministry;
                payload.Title = title;
                payload.TextContent = preview;
                payload.HtmlContent = htmlContent;
                payload.Preview = preview;
                payload.Body = body;

                newsArchiveApiClient.ApiArchivesPost(payload);
            }
        }
        static void Main(string[] args)
        {
            string apiServiceLocation = "http://localhost:9010";
            string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2NzYzOTg4MTcsImlzcyI6Imh0dHA6Ly9nY3BlIiwiYXVkIjoiaHR0cDovL2djcGUifQ.U3wZHowTyNg7XaHFyEjH4Uoq5Ezo8uRxJjU9aUajAbQ";
            var newsArchiveApiClient = new Client(new Uri(apiServiceLocation));
            newsArchiveApiClient.HttpClient.DefaultRequestHeaders.Clear();
            newsArchiveApiClient.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            string sourceDir = "C:\\Projects\\GCPE\\News Archive files\\Web Site - Releases\\news_releases_2017-2021";

            // Create a Collection for the directory.

            Gov.News.Archive.Models.Collection c = new Models.Collection();
            c.StartDate = DateTime.Parse("2017-07-18");
            c.EndDate = null;
            c.Name = "July 18, 2017 to current date";

            ImportDirectory(c, sourceDir, newsArchiveApiClient);
            c = new Models.Collection();
            c.StartDate = DateTime.Parse("2017-06-12");
            c.EndDate = DateTime.Parse("2017-07-17");
            c.Name = "June 12, 2017 to July 17, 2017";

            sourceDir = "C:\\Projects\\GCPE\\News Archive files\\Web Site - Releases\\news_releases_2017-2017";
            ImportDirectory(c, sourceDir, newsArchiveApiClient);

            c = new Models.Collection();
            c.StartDate = DateTime.Parse("2013-06-10");
            c.EndDate = DateTime.Parse("2017-06-11");
            c.Name = "June 10, 2013 to June 11, 2017";

            sourceDir = "C:\\Projects\\GCPE\\News Archive files\\Web Site - Releases\\news_releases_2013-2017";
            //ImportDirectory(c, sourceDir, newsArchiveApiClient);

            c = new Models.Collection();
            c.StartDate = DateTime.Parse("2011-06-11");
            c.EndDate = DateTime.Parse("2013-06-9");
            c.Name = "June 11, 2009 to June 9, 2013";

            sourceDir = "C:\\Projects\\GCPE\\News Archive files\\Web Site - Releases\\news_releases_2009-2013";
            //ImportDirectory(c, sourceDir, newsArchiveApiClient);
            

            Console.ReadLine();

        }
    }
}
