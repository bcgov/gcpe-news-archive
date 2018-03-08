using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.News.Archive.ViewModels;
using MongoDB.Bson;

namespace Gov.News.Archive.Entity_Extensions
{
    public static class ViewModelExtensions
    { 
        public static ViewModels.Archive ToViewModel(this Models.Archive model)
        {
            ViewModels.Archive result = null;

            if (model != null)
            {
                result = new ViewModels.Archive();
                if (model.Collection != null) { result.Collection = model.Collection.ToViewModel(); }
                if (model.DateReleased != null) { result.DateReleased = (DateTime)model.DateReleased; }
                result.HtmlContent = model.HtmlContent;
                if (model.Id != null) { result.Id = model.Id.ToString(); }
                result.TextContent = model.TextContent;
                result.MinistryText = model.MinistryText;
                result.Title = model.Title;
                result.Preview = model.Preview;
                result.Body = model.Body;
            }
            return result; 
        }

        public static ViewModels.Collection ToViewModel(this Models.Collection model)
        {
            ViewModels.Collection result = null;

            if (model != null)
            {
                result = new ViewModels.Collection();
                if (model.EndDate != null) { result.EndDate = (DateTime) model.EndDate; }
                if (model.StartDate != null) { result.StartDate = (DateTime) model.StartDate; }
                if (model.Id != null) { result.Id = model.Id.ToString(); }
                result.Name = model.Name;
            }
            return result;
        }

        public static Models.Archive FromViewModel(this ViewModels.Archive viewModel)
        {
            Models.Archive result = null;

            if (viewModel != null)
            {
                result = new Models.Archive();
                if (viewModel.Collection != null) { result.Collection = viewModel.Collection.FromViewModel(); }
                if (viewModel.DateReleased != null) { result.DateReleased = (DateTime)viewModel.DateReleased; }
                result.HtmlContent = viewModel.HtmlContent;
                if (viewModel.Id != null) { result.Id = new ObjectId(viewModel.Id); }
                result.TextContent = viewModel.TextContent;
                result.MinistryText = viewModel.MinistryText;
                result.Title = viewModel.Title;
                result.Preview = viewModel.Preview;
                result.Body = viewModel.Body;
            }
            return result;
        }

        public static Models.Collection FromViewModel(this ViewModels.Collection viewModel)
        {
            Models.Collection result = null;

            if (viewModel != null)
            {
                result = new Models.Collection();
                if (viewModel.EndDate != null) { result.EndDate = viewModel.EndDate; }
                if (viewModel.StartDate != null) { result.StartDate =  viewModel.StartDate; }
                if (viewModel.Id != null) { result.Id = new ObjectId(viewModel.Id); }
                result.Name = viewModel.Name;
            }
            return result;
        }
    }
}
