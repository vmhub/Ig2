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
     private static byte totalPages;

        #endregion

        static ResponseMethods()
        {
            accessKey=ConfigurationManager.AppSettings["accessKey"];
            secretKey=ConfigurationManager.AppSettings["secretKey"];
            destination=ConfigurationManager.AppSettings["destination"];
            associateTag=ConfigurationManager.AppSettings["associateTag"];
        }

         private static XDocument formDocument (string index,string item,string page)
         {

            SignedRequestHelper helper = new SignedRequestHelper(accessKey, secretKey, destination); // KEY FAILSSSSSSSSSS
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
                //TODO: 
            }
            return responseDocument;
         }
         public static IList<string> getItemsList(string index, string item, string page="1")
         {
             //XDocument itemDoc = null;
             if (totalPages == 0) 
             {
                 itemDoc = formDocument(index,item,page);
                 totalPages = Byte.Parse(itemDoc.Descendants().First(e => e.Name.LocalName.Equals("TotalPages")).Value);
                 totalPages = totalPages > (byte)5 ? (byte)5 : totalPages;
             }
             int j = 0;
             IList<string> itemList = new List<string>(13);
             for (; i <= totalPages; i++)
             {

                  if (state == 0)
                //  {
                      itemDoc = i == 1 ? itemDoc : formDocument(index, item,i.ToString());
                      //tempDoc = itemDoc;
                //  }
                //  else itemDoc = tempDoc;
                  XNamespace ns = itemDoc.Root.GetDefaultNamespace();
                  IList<XElement> items = itemDoc.Descendants(ns + "Item").ToList();

                  j = state;
                  state = 0;

                  for (; j < items.Count; j++)
                  {
                      IEnumerable<XElement> itemProps = items[j].Descendants();
                      string title = itemProps.First(prop => prop.Name.LocalName.Equals("Title")).Value;
                      //List<XElement> fef = elementz[j].Descendants().ToList();
                      //string ccc2 = fef[43].Value; //can't use for performance, xml inconsistent...
                      itemList.Add(title);
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
            state = totalPages = 0;
            itemDoc = null;
         }

    }
}