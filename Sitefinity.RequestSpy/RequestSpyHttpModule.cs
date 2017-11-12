using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Sitefinity.RequestSpy
{
    public class RequestSpyHttpModule : IHttpModule
    {
        public static BlockingCollection<RequestDto> RequestBuffer
        {
            get
            {
                return RequestSpyHttpModule.requestBuffer;
            }
        }

        public static string ServiceUrl
        {
            get
            {
                return RequestSpyHttpModule.requestSpyUrl;
            }
        }

        public void Init(HttpApplication context)
        {
            RequestSpyHttpModule.InitRequestBuffer();
            context.BeginRequest += this.Context_BeginRequest;
            context.EndRequest += this.Context_EndRequest;
        }
        
        public void Dispose()
        {
            //RequestSpyHttpModule.DisposeRequestBuffer();
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            if (!Bootstrapper.IsReady)
            {
                return;
            }

            HttpApplication application = (HttpApplication)sender;

            if (application.Context.Request.Url.AbsolutePath == RequestSpyHttpModule.requestSpyUrl)
            {
                RequestSpyHttpModule.ReturnServiceResponse(application.Context);
                return;
            }
        }

        private void Context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;

            bool isBackendRequest = application.Context.Items.Contains(SystemManager.IsBackendRequestKey) && (bool)application.Context.Items[SystemManager.IsBackendRequestKey];            

            if (!Bootstrapper.IsReady 
                || application.Context.Request.Url.AbsolutePath == RequestSpyHttpModule.requestSpyUrl
                || isBackendRequest)                
            {
                return;
            }

            if (Config.Get<RequestSpyConfig>().PagesOnly)
            {
                SiteMapProvider siteMapProvider = SiteMapBase.GetSiteMapProvider("FrontendSiteMap");
                var frontendPage = siteMapProvider.FindSiteMapNode(application.Context.Request.Url.AbsolutePath);
                bool isFrontendPage = frontendPage != null;

                if (!isFrontendPage && application.Context.Response.StatusCode == 200) // status code condition added in order to catch non-200 requests, e.g. 404
                {
                    return;
                }
            }

            if (RequestSpyHttpModule.requestBuffer.Count == 15)
            {
                RequestSpyHttpModule.requestBuffer.Take();
            }

            RequestSpyHttpModule.requestBuffer.Add(new RequestDto()
            {
                Id = Guid.NewGuid().ToString(),
                Url = application.Context.Request.Url.AbsoluteUri,
                Protocol = application.Context.Request.Url.Scheme,
                Response = new ResponseDto(application.Context.Response.StatusCode)
            });
        }

        /// <summary>
        /// Gets response for the request spy service request.
        /// </summary>
        /// <param name="context">Http context.</param>
        private static void ReturnServiceResponse(HttpContext context)
        {
            context.Response.StatusCode = 200;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Content-Type", "application/json; charset=" + context.Response.Charset);

            var serializer = new JavaScriptSerializer();
            context.Response.Write(serializer.Serialize(requestBuffer.Reverse()));
            context.ApplicationInstance.CompleteRequest();
        }

        private static void InitRequestBuffer()
        {
            if (RequestSpyHttpModule.requestBuffer != null)
            {
                return;
            }

            RequestSpyHttpModule.requestBuffer = new BlockingCollection<RequestDto>(15);
        }

        private static void DisposeRequestBuffer()
        {
            if (RequestSpyHttpModule.requestBuffer == null)
            {
                return;
            }

            RequestSpyHttpModule.requestBuffer.Dispose();
            RequestSpyHttpModule.requestBuffer = null;
        }

        private static BlockingCollection<RequestDto> requestBuffer;
        private static readonly string requestSpyUrl = (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty) + "/request-spy";
    }
}