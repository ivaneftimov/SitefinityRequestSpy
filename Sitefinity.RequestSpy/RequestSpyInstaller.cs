using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Services;

namespace Sitefinity.RequestSpy
{
    /// <summary>
    /// Module installer class
    /// </summary>
    /// <remarks>
    /// This installer is registered in the /Properties/AssemblyInfo.cs file
    /// The purpose of it is to register the module in Sitefinity automatically.
    /// The User will have to enable the module from Administration -> Modules & Services
    /// </remarks>
    public static class RequestSpyInstaller
    {
        /// <summary>
        /// Called before the application start.
        /// </summary>
        public static void PreApplicationStart()
        {
            DynamicModuleUtility.RegisterModule(typeof(RequestSpyHttpModule));
            Bootstrapper.Initialized += RequestSpyInstaller.OnBootstrapperInitialized;
        }

        /// <summary>
        /// Called when the Bootstrapper is initialized.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
        private static void OnBootstrapperInitialized(object sender, ExecutedEventArgs e)
        {
            if (e.CommandName == "RegisterRoutes")
            {
                // We have to register the module at a very early stage when sitefinity is initializing
                RequestSpyInstaller.RegisterSfModule();
            }
        }

        /// <summary>
        /// Registers the PageInfoDashboardInstaller module.
        /// </summary>
        private static void RegisterSfModule()
        {
            var configManager = ConfigManager.GetManager();
            var modulesConfig = configManager.GetSection<SystemConfig>().ApplicationModules;
            if (!modulesConfig.Elements.Any(el => el.GetKey().Equals(RequestSpyModule.ModuleName)))
            {
                modulesConfig.Add(RequestSpyModule.ModuleName, new AppModuleSettings(modulesConfig)
                {
                    Name = RequestSpyModule.ModuleName,
                    Title = RequestSpyModule.ModuleTitle,
                    Description = RequestSpyModule.ModuleDescription,
                    Type = typeof(RequestSpyModule).AssemblyQualifiedName,
                    StartupType = StartupType.Disabled
                });

                configManager.SaveSection(modulesConfig.Section);

                // Uncomment if you change the StartupType to OnApplicationStart
                //SystemManager.RestartApplication(false);
            }
        }
    }
}
