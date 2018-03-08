using System;
using System.Text;
using Newtonsoft.Json;



namespace Gov.News.Archive.ViewModels
{
    /// <summary>
    /// Collection Database Model
    /// </summary>
    
    public class Archive
    {

        /// <summary>
        /// A system-generated unique identifier for a Collection
        /// </summary>
        /// <value>A system-generated unique identifier for a Collection</value>

        public string Id { get; set; }

        //[ForeignKey("Collection")]
        //public ObjectId CollectionId { get; set; }
        //[JsonProperty("Collection")]
        public ViewModels.Collection Collection { get; set; }

        /// <summary>
        /// The name of the Collection
        /// </summary>
        /// <value>The name of the Collection</value> 

        public string Title { get; set; }

        public DateTime DateReleased { get; set; }

        public string MinistryText { get; set; }

        public string HtmlContent { get; set; }

        public string TextContent { get; set; }

        public string Preview { get; set; }

        public string Body { get; set; }
        
    }
}
