using System;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Gov.News.Archive.ViewModels
{
    /// <summary>
    /// Collection Database Model
    /// </summary>
    
    public class Collection 
    {
        /// <summary>
        /// Collection Constructor (required by entity framework)
        /// </summary>

        /// <summary>
        /// Initializes a new instance of the <see cref="Collection" /> class.
        /// </summary>
        /// <param name="id">A system-generated unique identifier for a Collection (required).</param>
        /// <param name="name">The name of the Collection (required).</param>
        /*

        public Collection(ObjectId id, string name)
        {   
            Id = id;
            Name = name;
        }
        */

        /// <summary>
        /// A system-generated unique identifier for a Collection
        /// </summary>
        /// <value>A system-generated unique identifier for a Collection</value>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// The name of the Collection
        /// </summary>
        /// <value>The name of the Collection</value>

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
       
        
    }
}
