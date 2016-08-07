using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Ig2.Models.PlainHolders
{
    public class SearchInfo
    {
        public string neededItem { get; set; }
        public string searchIndex { get; set; }
        public readonly ReadOnlyCollection<SelectListItem> selectList;
        public SearchInfo()
        {
            selectList = new ReadOnlyCollection<SelectListItem>
                (
                new List<SelectListItem>
                         {   new SelectListItem{Value = "All",Text = "All"},
                             new SelectListItem{Value = "Apparel",Text = "Apparel"},
                             new SelectListItem{Value = "Automotive",Text = "Automotive"},
                             new SelectListItem{Value = "Baby",Text = "Baby"},
                             new SelectListItem{Value = "Beauty",Text = "Beauty"},
                             new SelectListItem{Value = "Books",Text = "Books"},
                             new SelectListItem{Value = "Classical",Text = "Classical"},
                             new SelectListItem{Value = "DVD",Text = "DVD"},
                             new SelectListItem{Value = "Electronics",Text = "Electronics"},
                             new SelectListItem{Value = "ForeignBooks",Text = "ForeignBooks"},
                             new SelectListItem{Value = "Grocery",Text = "Grocery"},
                             new SelectListItem{Value = "HealthPersonalCare",Text = "HealthPersonalCare"},
                             new SelectListItem{Value = "HomeGarden",Text = "HomeGarden"},
                             new SelectListItem{Value = "Jewelry",Text = "Jewelry"},
                             new SelectListItem{Value = "KindleStore",Text = "KindleStore"},
                             new SelectListItem{Value = "Kitchen",Text = "Kitchen"},
                             new SelectListItem{Value = "Lighting",Text = "Lighting"},
                             new SelectListItem{Value = "Magazines",Text = "Magazines"},
                             new SelectListItem{Value = "Marketplace",Text = "Marketplace"},
                             new SelectListItem{Value = "MP3Downloads",Text = "MP3Downloads"},
                             new SelectListItem{Value = "Music",Text = "Music"},
                             new SelectListItem{Value = "MusicalInstruments",Text = "MusicalInstruments"},
                             new SelectListItem{Value = "MusicTracks",Text = "MusicTracks"},
                             new SelectListItem{Value = "OfficeProducts",Text = "OfficeProducts"},
                             new SelectListItem{Value = "OutdoorLiving",Text = "OutdoorLiving"},
                             new SelectListItem{Value = "Outlet",Text = "Outlet"},
                             new SelectListItem{Value = "PCHardware",Text = "PCHardware"},
                             new SelectListItem{Value = "Photo",Text = "Photo"},
                             new SelectListItem{Value = "Shoes",Text = "Shoes"},
                             new SelectListItem{Value = "Software",Text = "Software"},
                             new SelectListItem{Value = "SoftwareVideoGames",Text = "SoftwareVideoGames"},
                             new SelectListItem{Value = "SportingGoods",Text = "SportingGoods"},
                             new SelectListItem{Value = "Tools",Text = "Tools"},
                             new SelectListItem{Value = "Toys",Text = "Toys"},
                             new SelectListItem{Value = "VHS",Text = "VHS"},
                             new SelectListItem{Value = "Video",Text = "Video"},
                             new SelectListItem{Value = "VideoGames",Text = "VideoGames"},
                             new SelectListItem{Value = "Watches",Text = "Watches"}
                         }
                );
        }
    }
}