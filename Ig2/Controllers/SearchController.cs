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
        /// <summary>
        /// Checks if session exists
        /// </summary>
        /// <returns></returns>
        private bool sesssionThere()
        {
            return Session["StoredInfo"] != null;
        }
        /// <summary>
        /// Sets the session to passed value
        /// </summary>
        /// <param name="data"></param>
        private void setSession(FoundItems data)
        {
            Session["StoredInfo"] = data;
        }
        /// <summary>
        /// Type casts session
        /// </summary>
        /// <returns></returns>
        private FoundItems castSession()
        {
            return Session["StoredInfo"] as FoundItems;
        }
        /// <summary>
        /// Index page controller
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        /// <summary>
        /// Returns first item list if there is one
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Forward click controller / returns more data (loop 5>5:1)
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Returns currency JSON
        /// </summary>
        /// <param name="baze"></param>
        /// <returns></returns>
       [HttpPost]
       public string Json(string baze)
       {    
           return Ig2.Models.GetRates.Rates.getJson(baze);
       }
    }
}