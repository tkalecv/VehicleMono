using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Models.Common.Paging__Sorting__Filtering
{
   public interface ISorting
    {
         string sortOrder { get; set; }
    }
}
