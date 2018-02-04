# SitefinityRequestSpy 

### Sitefinity module which listens to every request which comes to the Sitefinity application and visualises it in the backend.
![alt text](https://raw.githubusercontent.com/ivaneftimov/SitefinityRequestSpy/master/screenshot.PNG "Demo Screenshot")

### Install
NuGet Package: https://www.nuget.org/packages/Sitefinity.RequestSpy


### Dependencies
 - Telerik.Sitefinity.dll
 - Telerik.Sitefinity.Services.Events.dll
 - Microsoft.Web.Infrastructure.dll
 
I removed the dependency to the Telerik.Sitefinity.Core NuGet package because it brings ton of other dependencies (Authentication, etc.) which are not needed for this tool. 
Instead, I referred the required DLLs directly from the NuGet package folder, but without actually use NuGet packges within its code project.
In any case, this module will work only for a Sitefinity application, so all the required DLLs will already be in the web application bin folder.



**The module is build agains build `10.1.6527.0`**


Make sure you add binding redirect if you install it on a different build. E.g on build `6526`:
```xml
<dependentAssembly>
  <assemblyIdentity name="Telerik.Sitefinity" publicKeyToken="b28c218413bdf563" culture="neutral" />
  <bindingRedirect oldVersion="0.0.0.0-10.1.6527.0" newVersion="10.1.6526.0" />
</dependentAssembly>

<dependentAssembly>
  <assemblyIdentity name="Telerik.Sitefinity.Services.Events" publicKeyToken="b28c218413bdf563" culture="neutral" />
  <bindingRedirect oldVersion="0.0.0.0-10.1.6527.0" newVersion="10.1.6526.0" />
</dependentAssembly>
```
*SitefintiyWebApp project (10.1.6527.0) is included for dev convenince.
The database is in the App_Data folder. A license file is needed to run the website.*
