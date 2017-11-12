using Telerik.Sitefinity.Modules.GenericContent.Configuration;

namespace Sitefinity.RequestSpy
{
    public class RequestSpyConfig : ModuleConfigBase
    {
        /// <summary>
        /// Initialize the default providers for the module.
        /// </summary>
        /// <param name="providers">
        /// The dictionary of the provider definitions.
        /// </param>
        protected override void InitializeDefaultProviders(Telerik.Sitefinity.Configuration.ConfigElementDictionary<string, Telerik.Sitefinity.Configuration.DataProviderSettings> providers)
        {
            // do nothing for now
        }
    }
}