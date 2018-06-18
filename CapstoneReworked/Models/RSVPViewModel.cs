using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneReworked.Models
{
    public class RSVPViewModel
    {
        public string Name { get; set; }
        public string Attending { get; set; }
        public Nullable<int> GuestNumber { get; set; }
        public string FoodItems { get; set; }
        public string Allergies { get; set; }
        public string Comments { get; set; } 
        public IEnumerable<int> FoodMenuIDs { get; set; }

        public RSVP ToRSVP(CapstoneEntities db)
        {
            var result = new RSVP
            {
                Allergies = this.Allergies, 
                Attending = this.Attending,
                Comments = this.Comments,
                FoodItems = this.FoodItems,
                GuestNumber = this.GuestNumber,
                Name = this.Name
            };

            result.FoodMenus = this.FoodMenuIDs?.Select(x => db.FoodMenus.Find(x)).ToList();
            return result;
        }
    }


}