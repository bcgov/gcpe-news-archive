using System;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Gov.News.Archive.ViewModels
{
    /// <summary>
    /// Collection Database Model
    /// </summary>
    
    public  class Ministry 
    {

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
        [MaxLength(150)]        
        public string Name { get; set; }
        
    }
}
