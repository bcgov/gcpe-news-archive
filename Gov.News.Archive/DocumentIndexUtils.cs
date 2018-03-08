using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Hangfire.Console;
using Hangfire.Server;
using Newtonsoft.Json;
using Gov.News.Archive.Models;
using Gov.News.Archive.AzureModels;
using Gov.News.Archive.Entity_Extensions;
using MongoDB.Bson;

namespace Gov.News.Archive
{
    public class DocumentIndexUtils
    {
        private static string lastMediaRequestIndexUpdateSetting = "LastMediaRequestIndexUpdate";
        private static string lastContactIndexUpdateSetting = "LastContactIndexUpdate";
        private static string contactIndexLockedSetting = "ContactIndexLock";
        private static string contactIndexLockedValue = "Locked";
        private static string contactIndexUnlockedValue = "Unlocked";

        private static HttpClient Client = new HttpClient();
        private static ILoggerFactory entityLoggerFactory = new LoggerFactory().AddDebug(); // newing a new one each time would create a memory leak!

        public static string LastMediaRequestIndexUpdateSetting { get => lastMediaRequestIndexUpdateSetting; set => lastMediaRequestIndexUpdateSetting = value; }
        public static string LastContactIndexUpdateSetting { get => lastContactIndexUpdateSetting; set => lastContactIndexUpdateSetting = value; }
        public static string ContactIndexLockedSetting { get => contactIndexLockedSetting; set => contactIndexLockedSetting = value; }
        public static string ContactIndexLockedValue { get => contactIndexLockedValue; set => contactIndexLockedValue = value; }
        public static string ContactIndexUnlockedValue { get => contactIndexUnlockedValue; set => contactIndexUnlockedValue = value; }
        /*
        public static DbContextOptions<DbAppContext> GetDbContextOptions(string connectionString)
        {
            // https://github.com/sergeyzwezdin/Hangfire.Mongo

            var options = new DbContextOptionsBuilder<DbAppContext>();
            options.UseSqlServer(connectionString)
                   .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning))
                   .UseLoggerFactory(entityLoggerFactory);
            return options.Options;
        }
        
    */

        /// <summary>
        /// Hangfire job to update a single document.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="Configuration"></param>

        public static async Task ArchiveIndexDocumentJob(string connectionString, string accessToken, string baseUri, string indexName, ObjectId id, PerformContext hangfireContext)
        {
            hangfireContext.WriteLine("Starting Media Request Index Document Job.");
            // sanity check
            if (connectionString != null)
            {
                /*
                // make a database connection and see if there are any records that need to be updated.
                using (var context = new DbAppContext(GetDbContextOptions(connectionString)))
                {
                    var data = context.Archives
                    .FirstOrDefault(x => x.Id == id);

                    if (data != null)
                    {
                        hangfireContext.WriteLine("Found a record to index.");
                        //  convert the results to Azure objects.
                        AzureArchive convertedData = data.ToAzure();
                        List<AzureArchive> listData = new List<AzureArchive>();
                        listData.Add(convertedData);
                        bool indexResult = await IndexDocuments(listData, accessToken, baseUri, indexName, hangfireContext);
                        if (indexResult)
                        {
                            hangfireContext.WriteLine("Sent record.");
                        }
                        else
                        {
                            // Report failure
                            throw new InvalidOperationException("Unable to process documents with ID of " + id);
                        }
                    }
                    else
                    {
                        hangfireContext.WriteLine("No data found for id " + id.ToString());
                    }
                }
                */
            }
            else
            {
                hangfireContext.SetTextColor(ConsoleTextColor.Red);
                hangfireContext.WriteLine("Error!  No connection string.");
                hangfireContext.ResetTextColor();
                throw new InvalidOperationException("No connection string.");
            }

            hangfireContext.WriteLine("Done.");
        }

        private static void ReportDocumentSendFailure(PerformContext hangfireContext, List<Gov.News.Archive.Models.Archive> data)
        {
            hangfireContext.SetTextColor(ConsoleTextColor.Red);
            hangfireContext.WriteLine("Error!  Unable to send files to Azure.");
            hangfireContext.ResetTextColor();
            hangfireContext.WriteLine("The following documents were not sent:");
            foreach (Gov.News.Archive.Models.Archive item in data)
            {
                hangfireContext.WriteLine(item.Id.ToString());
            }
        }

       
        

        /// <summary>
        /// Index a list of documents.
        /// </summary>
        /// <param name="items">List of documents</param>
        /// <param name="accessToken">Service JWT</param>
        /// <param name="baseUri">The base service URI</param>
        /// <param name="hangfireContext">Pass null if not being run in Hangfire</param>
        /// <returns></returns>
        public static async Task<bool> IndexDocuments(List<AzureArchive> items, string accessToken, string baseUri, string indexName, PerformContext hangfireContext)
        {
            string targetUri = baseUri + "/api/index/" + indexName + "/documents";

            if (hangfireContext != null)
            {
                hangfireContext.WriteLine("Sending data to " + targetUri);
            }

            bool result = false;
            // call the microservice

            try
            {
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                string jsonString = JsonConvert.SerializeObject(items);

                // note the double serialization - this allows us to pass arbitrary types encoded as string.
                HttpContent payload = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await Client.PostAsync(targetUri, payload);

                string content = await response.Content.ReadAsStringAsync();
                if (hangfireContext != null)
                {
                    hangfireContext.WriteLine("Received data: " + content);
                }

                result = (response.StatusCode == HttpStatusCode.OK && JsonConvert.DeserializeObject<bool>(content));


                if (!result && hangfireContext != null) // failure to send
                {
                    hangfireContext.SetTextColor(ConsoleTextColor.Red);
                    hangfireContext.WriteLine("Error!  Unable to send documents to search service.");
                    hangfireContext.WriteLine("Status code is " + response.StatusCode);
                    hangfireContext.WriteLine(content);
                    hangfireContext.ResetTextColor();

                }
            }
            catch (Exception e)
            {
                result = false;
                if (hangfireContext != null) // failure to send
                {
                    hangfireContext.SetTextColor(ConsoleTextColor.Red);
                    hangfireContext.WriteLine("Error!  Exception occured during document index.");
                    hangfireContext.ResetTextColor();
                    hangfireContext.WriteLine(e.ToString());
                }
            }

            return result;
        }

        public static async Task<bool> ResetSearchServiceIndex(string baseUri, string accessToken, string indexName)
        {
            string targetUri = baseUri + "/api/index/" + indexName;

            bool result = false;
            // call the microservice

            try
            {
                Client.DefaultRequestHeaders.Clear();
                Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                HttpResponseMessage response = await Client.DeleteAsync(targetUri);

                string content = await response.Content.ReadAsStringAsync();

                result = (response.StatusCode == HttpStatusCode.OK && JsonConvert.DeserializeObject<bool>(content));

            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }
    }
}

