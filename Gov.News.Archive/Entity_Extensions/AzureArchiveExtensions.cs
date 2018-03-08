using Gov.News.Archive.AzureModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.News.Archive.Entity_Extensions
{
    public static class AzureMediaRequestExtension
    {
        public static AzureArchive ToAzure(this Models.Archive model)
        {
            var dto = new AzureArchive();

            if (model != null)
            {
                dto.Id = model.Id.ToString();
                

            }

            return dto;
        }
    }
}
