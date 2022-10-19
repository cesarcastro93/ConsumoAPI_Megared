using System.Web;
using System.Web.Mvc;

namespace ConsumoDeAPI_MegaRed
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
