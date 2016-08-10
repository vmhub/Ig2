using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using AmazonProductAdvtApi;
using System.Net;
using System.Configuration;
using Ig2.Models.PlainHolders;
namespace Ig2.Models.CreateResponse
{
    public static class ResponseMethods
    {
        #region AmazonKeys

        private static readonly string accessKey; 
        private static readonly string secretKey;
        private static readonly string destination;
        private static readonly string associateTag;
        #endregion

        #region StateMachineValues

     private static int i = 1;
     private static int state;
     private static XDocument itemDoc;
     private static  uint totalPages;

        #endregion

     private static log4net.ILog log;
        static ResponseMethods()
        {
            accessKey=ConfigurationManager.AppSettings["accessKey"];
            secretKey=ConfigurationManager.AppSettings["secretKey"];
            destination=ConfigurationManager.AppSettings["destination"];
            associateTag=ConfigurationManager.AppSettings["associateTag"];
            log=log4net.LogManager.GetLogger(typeof(ResponseMethods));
        }

         private static XDocument formDocument (string index,string item,string page)
         {

            SignedRequestHelper helper = new SignedRequestHelper(accessKey, secretKey, destination);
            IDictionary<string, string> req = new Dictionary<string, String>();
            req["Service"] = "AWSECommerceService";
            req["Keywords"] = item; 
            req["Operation"] = "ItemSearch";
            req["SearchIndex"] = index; 
            req["AssociateTag"] = associateTag;
            req["ItemPage"] = page;
            req["ResponseGroup"] = "Offers,Images,ItemAttributes";
            req["Timestamp"] = DateTime.Now.ToString("yyyy-mm-ddThh:mm:ssZ");
            string newUri = helper.Sign(req);

            XDocument responseDocument = null;
            try
            {
                Uri requestUri = new Uri(newUri, UriKind.Absolute);
                WebRequest request = HttpWebRequest.Create(requestUri);
                request.Timeout = 5000;
                WebResponse response = request.GetResponse();
                XmlReader reader = XmlReader.Create(response.GetResponseStream());
                responseDocument = XDocument.Load(reader);
            }
            catch (Exception ex)
            {
                log.Error("Error while getting response: " + ex.ToString());
            }
            return responseDocument;
         }
         public static uint returnPages()
         {
             return totalPages;
         }
         public static IList<ItemInfo> getItemsList(string index, string item,string page = "1")
         {
             if (totalPages == 0)
             {
                 try
                 {
                     itemDoc = formDocument(index,item,page);
                     totalPages = UInt32.Parse(itemDoc.Descendants().First(e => e.Name.LocalName.Equals("TotalPages")).Value);
                     totalPages = totalPages > (byte)5 ? (byte)5 : totalPages;
                 }
                 catch(InvalidOperationException ex)
                 {
                     totalPages = 0;
                     log.Error("Cannot find needed element in lambda: " + ex.ToString());
                 }
                 catch (ArgumentNullException ex)
                 {
                     totalPages = 0;
                     log.Error("Null pointer: " + ex.ToString());
                 }
             }
             int j = 0;
             IList<ItemInfo> itemList = new List<ItemInfo>(13);
             for (; i <= totalPages; i++)
             {

                  if (state == 0)
                      itemDoc = i == 1 ? itemDoc : formDocument(index, item,i.ToString());
                  XNamespace ns = itemDoc.Root.GetDefaultNamespace();
                  IList<XElement> items = itemDoc.Descendants(ns + "Item").ToList();

                  j = state;
                  state = 0;

                  for (; j < items.Count; j++)
                  {
                      IEnumerable<XElement> itemProps = items[j].Descendants();
                      ItemInfo itemInfo=null;
                      string imag =null;
                      try
                      {
                          XElement image = itemProps.First(prop => prop.Name.LocalName.Equals("SmallImage"));
                          imag=image.Descendants().ToList()[0].Value;
                      }
                      catch (InvalidOperationException ec)
                      {
                          log.Error("Cannot find needed element in lambda: " + ec.ToString());
                          imag="";

                      }
                      string prc=null;
                      try
                      {
                          XElement price = itemProps.First(prop => prop.Name.LocalName.Equals("OfferSummary"));
                          prc= price.Descendants().ToList()[3].Value;
                      }
                      catch (InvalidOperationException ec)
                      {
                          log.Error("Cannot find needed element in lambda: " + ec.ToString());
                          prc = "$0.00";
                         
                      }
                         itemInfo = new ItemInfo
                         {
                             title = itemProps.First(prop => prop.Name.LocalName.Equals("Title")).Value,
                             img = imag,
                             price =prc

                         };
                         /* have to use .First() instead of direct indexing like
                          * 
                          * List<XElement> fef = items[j].Descendants().ToList();                       
                          * string ccc2 = fef[43].Value;
                          * 
                          * since xml indexes are not consistent => less performance
                          */
                         itemList.Add(itemInfo);
                         if (itemList.Count == 13)
                         {
                             state = j + 1;
                             return itemList;
                         }
                   
                     
                  }

             }
             return itemList;
         }
         public static void Reset()
         {
            i = 1;
            state = 0;
            itemDoc = null;
         }

    }
}