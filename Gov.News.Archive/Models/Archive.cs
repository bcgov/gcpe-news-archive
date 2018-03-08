using System;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Gov.News.Archive.Models
{
    /// <summary>
    /// Collection Database Model
    /// </summary>
    
    public class Archive
    {
        /// <summary>
        /// Collection Constructor (required by entity framework)
        /// </summary>
        public Archive()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Collection" /> class.
        /// </summary>
        /// <param name="id">A system-generated unique identifier for a Collection (required).</param>
        /// <param name="name">The name of the Collection (required).</param>
        public Archive(ObjectId id, string name)
        {   
            Id = id;
            Title = name;
        }

        /// <summary>
        /// A system-generated unique identifier for a Collection
        /// </summary>
        /// <value>A system-generated unique identifier for a Collection</value>
        [Key]
        public ObjectId Id { get; set; }

        //[ForeignKey("Collection")]
        //public ObjectId CollectionId { get; set; }
        //[JsonProperty("Collection")]
        public Models.Collection Collection { get; set; }

        /// <summary>
        /// The name of the Collection
        /// </summary>
        /// <value>The name of the Collection</value> 
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("DateReleased")]
        public BsonDateTime DateReleased { get; set; }
        [BsonElement("MinistryText")]
        public string MinistryText { get; set; }
        [BsonElement("HtmlContent")]
        public string HtmlContent { get; set; }
        [BsonElement("TextContent")]
        public string TextContent { get; set; }
        [BsonElement("Preview")]
        public string Preview { get; set; }
        [BsonElement("Body")]
        public string Body { get; set; }
        
    }
}
