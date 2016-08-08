using Ig2.Models.PlainHolders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ig2.Models.CreateResponse;
namespace Ig2.Controllers
{
    public class SearchController: Controller
    {   
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }



       [HttpPost]
        public ActionResult GetList(SearchInfo info)
       {
           FoundItems items = new FoundItems();
           items.ItemList = ResponseMethods.getItemsList(info.searchIndex, info.neededItem);
           return PartialView(items);
       }
    }
}