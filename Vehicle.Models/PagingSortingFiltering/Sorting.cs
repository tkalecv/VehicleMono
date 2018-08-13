using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Models.Common.Paging__Sorting__Filtering;

namespace Vehicle.Models.PagingSortingFiltering
{
   public class Sorting : ISorting
    {
       public string sortOrder { get; set; }
    }
}
