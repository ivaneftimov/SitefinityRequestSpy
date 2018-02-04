# SitefinityRequestSpy 

### Sitefinity module which listens to every request which comes to the Sitefinity application and visualises it in the backend.

SitefintiyWebApp project (10.1.6527.0) is included for dev convenince.
The database is in the App_Data folder. A license file is needed to run the website.

### Dependencies
 - Telerik.Sitefinity.dll
 - Telerik.Sitefinity.Services.Events.dll
 - Microsoft.Web.Infrastructure.dll
 
I removed the dependency to the Telerik.Sitefinity.Core NuGet package because it brings ton of other dependencies (Authentication, etc.) which are not needed for this tool. 
Instead, I referred the required DLLs directly from the NuGet package folder, but without actually use NuGet packges within its code project.
In any case, this module will work only for a Sitefinity application, so all the required DLLs will already be in the web application bin folder.

