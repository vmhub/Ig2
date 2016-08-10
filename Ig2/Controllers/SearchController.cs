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
        private bool sesssionThere()
        {
            return Session["StoredInfo"] != null;
        }
        private void setSession(FoundItems data)
        {
            Session["StoredInfo"] = data;
        }
        private FoundItems castSession()
        {
            return Session["StoredInfo"] as FoundItems;
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

       [HttpPost]
        public ActionResult getList(SearchInfo info)
        {
           ResponseMethods.Reset(); 
           FoundItems items = new FoundItems();               
           items.ItemList = ResponseMethods.getItemsList(info.searchIndex, info.neededItem);
           FoundItems tempItem = new FoundItems
           {
               ItemList = ResponseMethods.getItemsList(info.searchIndex, info.neededItem),
               searchIndex = info.searchIndex,
               searchItem = info.neededItem
           };
           setSession(tempItem);
           return PartialView(items);
       }

       [HttpGet]
       public ActionResult Forward()
       {   
               FoundItems items = castSession();
               if (items.ItemList.Count == 0)
               {
                   ResponseMethods.Reset();
                   items.ItemList = ResponseMethods.getItemsList(items.searchIndex, items.searchItem);
               }
               FoundItems tempItem = new FoundItems
               {
                   ItemList = ResponseMethods.getItemsList(items.searchIndex, items.searchItem),
                   searchIndex = items.searchIndex,
                   searchItem = items.searchItem 
               };
               setSession(tempItem);
               return PartialView("getList",items);
       }
    }
}