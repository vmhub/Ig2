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

        static int i = 1;
        static int state;
        static XDocument tempDoc;
        static byte totalPages;

        #endregion

        static ResponseMethods()
        {
            accessKey=ConfigurationManager.AppSettings["accessKey"];
            secretKey=ConfigurationManager.AppSettings["secretKey"];
            destination=ConfigurationManager.AppSettings["destination"];
            associateTag=ConfigurationManager.AppSettings["associateTag"];
        }

         private static XDocument formDocument (string page="1")
         {

            SignedRequestHelper helper = new SignedRequestHelper(accessKey, secretKey, destination);
            IDictionary<string, string> req = new Dictionary<string, String>();
            req["Service"] = "AWSECommerceService";
            req["Keywords"] = "qq 33"; // change
            req["Operation"] = "ItemSearch";
            req["SearchIndex"] = "All"; // change
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
         public static IList<string> getItemsList(string page)
         {
             XDocument itemDoc = null;
             if (totalPages == 0) 
             {
                 itemDoc = formDocument();
                 totalPages = Byte.Parse(itemDoc.Descendants().First(e => e.Name.LocalName.Equals("TotalPages")).Value);
                 totalPages = totalPages > (byte)5 ? (byte)5 : totalPages;
             }
             int j = 0;
             IList<string> itemList = new List<string>(13);
             for (; i <= totalPages; i++)
             {

                  if (state == 0)
                  {
                      itemDoc = i == 1 ? itemDoc : formDocument(i.ToString());
                      tempDoc = itemDoc;
                  }
                  else itemDoc = tempDoc;
                  XNamespace ns = itemDoc.Root.GetDefaultNamespace();
                  IList<XElement> elementz = itemDoc.Descendants(ns + "Item").ToList();

                  j = state;
                  state = 0;

                  for (; j < elementz.Count; j++)
                  {
                      IEnumerable<XElement> df = elementz[j].Descendants();
                      string title = df.First(qq => qq.Name.LocalName.Equals("Title")).Value;
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

    }
}