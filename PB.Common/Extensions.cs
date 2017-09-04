using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB.Common
{
  public static class Extensions
  {
    public static string MonthName(this int month)
    {
      switch (month)
      {
        case 1: return "January";
        case 2: return "February";
        case 3: return "March";
        case 4: return "April";
        case 5: return "May";
        case 6: return "June";
        case 7: return "July";
        case 8: return "August";
        case 9: return "September";
        case 10: return "October";
        case 11: return "November";
        case 12: return "December";
        default: return "";
      }
    }
    public static string DOWName(this int dow)
    {
      switch (dow)
      {
        case 1: return "Sunday";
        case 2: return "Monday";
        case 3: return "Tueday";
        case 4: return "Wednesday";
        case 5: return "Thursday";
        case 6: return "Friday";
        case 7: return "Saturday";
        default:
          return "";
      }
    }
  }
}
