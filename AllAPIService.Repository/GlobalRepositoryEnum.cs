using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAPIService.Repository
{
    internal class GlobalRepositoryEnum
    {
        public enum GlobalValues
        {
            PA1257L = 12570
        }

        public enum AgeEnum
        {
            [Display(Name = "Under66")] Under_66,
            [Display(Name = "Between6674")] Between_66_74,
            [Display(Name = "Over74")] Over_74,
        }
    }
}
