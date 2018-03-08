using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Hangfire;
using Newtonsoft.Json;
using Gov.News.Archive.Models;
using MongoDB.Bson;

namespace Gov.News.Archive.Controllers
{
    [Route("api/index")]
    public class IndexController : Controller
    {
        private readonly IConfiguration Configuration;
        //private readonly DbAppContext db;
        private readonly string accessToken;
        private readonly string baseUri;

        public IndexController(IConfiguration configuration)
        {
            Configuration = configuration;
            //this.db = db;
            accessToken = Configuration["SearchService:AccessToken"];
            baseUri = Configuration["SearchService:BaseUri"];
        }

        /// <summary>
        /// GET api/search
        /// </summary>
        /// <param name="query">space delimited query keywords</param>
        /// <param name="_skip">Search Result Offset</param>
        /// <param name="_limit">Maximum number of search results to return</param>
        /// <returns>List of GUID id fields for requests matching the query.</returns>
        [HttpGet("{indexName}/add/{documentId}")]
        public bool IndexAdd(string indexName, ObjectId documentId)
        {
            bool result = false;
            //string connectionString = db.Database.GetDbConnection().ConnectionString;
            //var jobId = BackgroundJob.Enqueue(() => DocumentIndexUtils.ArchiveIndexDocumentJob(null, accessToken, baseUri, indexName, documentId, null));
            //result = (jobId != null);
            return result;
        }
        
    }
}
