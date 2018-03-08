using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Hangfire;
using Newtonsoft.Json;
using Gov.News.Archive.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson;
using Gov.News.Archive.Entity_Extensions;

namespace Gov.News.Archive.Controllers
{
    [Route("api/collections")]
    public class CollectionsController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly DataAccess db; 
        public CollectionsController(DataAccess db, IConfiguration configuration)
        {
            Configuration = configuration;
            this.db = db;        
        }
        
        /// <summary>
        /// GET api/collections
        /// </summary>
        /// <returns>List of collections</returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetCollections()
        {
            List<Models.Collection> collections = db.GetCollections();
            List<ViewModels.Collection> result = new List<ViewModels.Collection>();

            collections.ForEach(x => result.Add(x.ToViewModel()));

            return new ObjectResult(result);
        }
        
        [AllowAnonymous]
        [HttpGet("{collectionId:length(24)}/archives")]
        public IActionResult GetCollectionArchives(string collectionId)
        {
            ObjectId id = new ObjectId(collectionId);

            List<Models.Archive> archives = db.GetCollectionArchives(id);
            List<ViewModels.Archive> result = new List<ViewModels.Archive>();
            archives.ForEach(x => result.Add(x.ToViewModel()));
            return new ObjectResult(result);
        }

        [AllowAnonymous]
        [HttpGet("{collectionId:length(24)}")]
        public IActionResult GetCollection(string collectionId)
        {
            ObjectId id = new ObjectId(collectionId);

            Models.Collection collection = db.GetCollection(id);
            ViewModels.Collection result = collection.ToViewModel();
            return new ObjectResult(result);
        }
    }
}
