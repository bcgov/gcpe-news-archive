
using System;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace Gov.News.Archive.AzureModels
{
    [SerializePropertyNamesAsCamelCase]
    public class AzureArchive
    {
        [System.ComponentModel.DataAnnotations.Key]
        [IsFilterable]
        public string Id { get; set; }

        [IsFilterable, IsSearchable, IsSortable]
        public string RequestTopic { get; set; }

        [IsFilterable, IsSearchable]
        public string Content { get; set; }

        [IsFilterable, IsSortable, IsFacetable]
        public DateTimeOffset? ReleaseDate { get; set; }

        [IsFilterable, IsSortable, IsFacetable]
        public DateTimeOffset? ResponsedAt { get; set; }

        [IsFilterable]
        public Int64? ResponsibleUserId { get; set; }           

        [IsFilterable]
        public string MinistryId { get; set; }

        [IsFilterable, IsSearchable, IsSortable, IsFacetable]
        public string MinistryDisplayName { get; set; }
            
        [IsFilterable, IsSortable, IsFacetable]
        public DateTimeOffset? ModifiedAt { get; set; }

    }

}
