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
        [NonAction] // ?
        private bool sesssionThere()
        {
            return Session["StoredInfo"] != null;
        }
        [NonAction]
        private void setSession(FoundItems data)
        {
            Session["StoredInfo"] = data;
        }
        [NonAction]
        private FoundItems castSession()
        {
            return Session["StoredInfo"] as FoundItems;
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        //foward / backward + random return... 3 methods mozno bez parametra?GET
        //(forward)
       [HttpPost]
        public ActionResult GetList(SearchInfo info)
       {
           FoundItems items = null;
           FoundItems tempItem = null;
           if (!sesssionThere())
           {
               items = new FoundItems();
               items.ItemList = ResponseMethods.getItemsList(info.searchIndex, info.neededItem);

               tempItem = new FoundItems
               {
                   ItemList = ResponseMethods.getItemsList(info.searchIndex, info.neededItem, "2"),
                   pageNr = 2
               };
               setSession(tempItem);
           }
           else
           {    
               items = castSession();
               byte page = items.pageNr;
               if ((page+1) > ResponseMethods.returnPages())
                   page = 1;
               items.ItemList = ResponseMethods.getItemsList(info.searchIndex, info.neededItem,page.ToString());
               tempItem = new FoundItems
               {
                   ItemList = ResponseMethods.getItemsList(info.searchIndex, info.neededItem, (page+1).ToString()),
                   pageNr = (byte)(page + 1)
               };
               setSession(tempItem);
           }
            
           return PartialView(items);
       }
    }
}