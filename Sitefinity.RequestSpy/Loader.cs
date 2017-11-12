using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace Sitefinity.RequestSpy
{
    public class Loader
    {
        public static void LoadModule()
        {
            DynamicModuleUtility.RegisterModule(typeof(RequestSpyHttpModule));
        }
    }
}
