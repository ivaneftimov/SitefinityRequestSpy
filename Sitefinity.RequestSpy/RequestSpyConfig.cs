using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Sitefinity.RequestSpy
{
    public class RequestSpyConfig : ConfigSection
    {
        /// <summary>
        /// Gets or set PageOnly option.
        /// </summary>
        [ConfigurationProperty("pagesOnly", DefaultValue = false)]
        public bool PagesOnly
        {
            get
            {
                return (bool)this["pagesOnly"];
            }

            set
            {
                this["pagesOnly"] = value;
            }
        }
    }
}