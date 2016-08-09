using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ig2.Models.PlainHolders
{
    public class FoundItems
    {
        public string searchIndex { get; set; }
        public string searchItem { get; set; }
        public IList<ItemInfo> ItemList { get; set; }
        public byte pageNr { get; set; }
    }
}