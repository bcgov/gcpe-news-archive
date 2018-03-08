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
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;
using Gov.News.Archive.Entity_Extensions;

namespace Gov.News.Archive.Controllers
{
    [Route("api/archives")]
    public class ArchivesController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly DataAccess db;


        public ArchivesController(DataAccess db, IConfiguration configuration)
        {
            Configuration = configuration;
            this.db = db;
        }


        /// <summary>
        /// GET api/collections
        /// </summary>
        /// <returns>List of Archives</returns>

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetArchives()
        {
            List<Models.Archive> archives = db.GetArchives();
            List<ViewModels.Archive> result = new List<ViewModels.Archive>();

            archives.ForEach(x => result.Add( x.ToViewModel()));

            return new ObjectResult(result);
        }

        [HttpPost]
        public IActionResult AddArchive([FromBody] ViewModels.Archive data)
        {

            Models.Archive newArchive = data.FromViewModel();
            
            // first add the collection.
            ViewModels.Collection c = data.Collection;

            Models.Collection dbCollection = null;

            if (! string.IsNullOrEmpty(c.Id))
            {
                dbCollection = db.GetCollection(new ObjectId (c.Id));
            }
            else
            {
                // try to find it by searching other fields.
                dbCollection = db.GetCollection(c.StartDate, c.EndDate, c.Name);
            }
            if (dbCollection == null)
            {
                Models.Collection newCollection = c.FromViewModel();

                dbCollection = db.CreateCollection(newCollection); 
            }

            if (dbCollection != null)
            {
                newArchive.Collection = dbCollection;
            }

            db.CreateArchive(newArchive);

            return new ObjectResult(newArchive.ToViewModel());

        }

        [AllowAnonymous]
        [HttpGet("{archiveId:length(24)}")]
        public IActionResult GetArchive( string archiveId)
        {
            ObjectId id = new ObjectId(archiveId);
            Models.Archive result = db.GetArchive(id);
            return new ObjectResult(result.ToViewModel());
        }
        
    }
}
