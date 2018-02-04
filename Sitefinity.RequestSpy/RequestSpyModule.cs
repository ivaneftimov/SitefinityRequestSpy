using System;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Events;

namespace Sitefinity.RequestSpy
{
    public class RequestSpyModule : ModuleBase
    {
        #region Properties
        /// <summary>
        /// Gets the id of the module landing (home) page.
        /// </summary>
        public override Guid LandingPageId
        {
            get
            {
                return RequestSpyModule.ModulePageId;
            }
        }

        /// <summary>
        /// Gets the array of manager types that this module supports
        /// </summary>
        /// <value>The array of manager types.</value>
        public override Type[] Managers
        {
            get
            {
                // this module does not support managers
                return new Type[0];
            }
        }
        #endregion

        #region Public and overriden methods
        /// <summary>
        /// Initializes the module for usage. This method is called every time 
        /// application pool is recycled and the module is requested.
        /// </summary>
        /// <param name="settings">
        /// The instance of <see cref="ModuleSettings"/> which provides information
        /// about the module and helps to initialize the module properly.
        /// </param>
        public override void Initialize(ModuleSettings settings)
        {
            App.WorkWith()
               .Module(RequestSpyModule.ModuleName)
               .Initialize()
               .Configuration<RequestSpyConfig>();

            Bootstrapper.Initialized -= this.Bootstrapper_Initialized;
            Bootstrapper.Initialized += this.Bootstrapper_Initialized;

            base.Initialize(settings);
        }

        /// <summary>
        /// Installs the module. This method is called only once, when the
        /// module is very first time installed.
        /// </summary>
        /// <param name="initializer">
        /// The instance of <see cref="SiteInitializer"/> which provides an installation
        /// context.
        /// </param>
        public override void Install(SiteInitializer initializer)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Uninstalls the specified initializer.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public override void Uninstall(SiteInitializer initializer)
        {
            Bootstrapper.Initialized -= this.Bootstrapper_Initialized;
            EventHub.Unsubscribe<IPagePreRenderCompleteEvent>(RequestSpyModule.OnPagePreRenderCompleteEventHandler);
            
            base.Uninstall(initializer);
        }

        /// <summary>
        /// Retrieves the module configuration section.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="ConfigSection"/> which contains configuration
        /// for the module.
        /// </returns>
        protected override ConfigSection GetModuleConfig()
        {
            return Config.Get<RequestSpyConfig>();
        }
        #endregion

        private void Bootstrapper_Initialized(object sender, Telerik.Sitefinity.Data.ExecutedEventArgs e)
        {
            EventHub.Subscribe<IPagePreRenderCompleteEvent>(RequestSpyModule.OnPagePreRenderCompleteEventHandler);
        }

        private static void OnPagePreRenderCompleteEventHandler(IPagePreRenderCompleteEvent @event)
        {
            if (@event.PageSiteNode.IsBackend && !@event.Page.Controls.Contains(requestSpyControl))
            {
                @event.Page.Controls.Add(requestSpyControl);
            }
        }

        #region Private fields & constants

        private static RequestSpyControl requestSpyControl = new RequestSpyControl();

        // <summary>
        /// The name of the module
        /// </summary>
        public const string ModuleName = "SitefinityRequestSpy";

        /// <summary>
        /// The title of the module
        /// </summary>
        public const string ModuleTitle = "Sitefinity Request Spy";

        /// <summary>
        /// The description of the module
        /// </summary>
        public const string ModuleDescription = "Listens to every request which comes to the Sitefinity application and visualises it in the backend";

        /// <summary>
        /// The module page id
        /// </summary>
        public static readonly Guid ModulePageId = new Guid("405E8086-5BF9-46BD-A04B-0D840D02FB01");

        #endregion
    }
}