using System;
using System.Collections.Generic;
using System.Linq;

namespace CapstoneReworked.Models
{
    

    public partial class RSVP
    {
        public string totalFoodList
        {
            get {
                var foodNames = FoodMenus.Select(x => x.name).ToList();
                foodNames.Add(FoodItems);
               
                return string.Join(", ", foodNames.ToArray()); }

        }
    }
}
