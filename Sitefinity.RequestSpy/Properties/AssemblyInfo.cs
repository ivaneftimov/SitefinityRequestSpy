using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Sitefinity.RequestSpy")]
[assembly: AssemblyDescription("Sitefinity module which listens to every request which comes to the Sitefinity application and visualises it in the backend")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("IvanEft")]
[assembly: AssemblyProduct("Sitefinity.RequestSpy")]
[assembly: AssemblyCopyright("Copyright © 2018 IvanEft")]
[assembly: AssemblyTrademark("IvanEft")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e5e6d99c-ae54-4895-98fc-7c78a6f3f653")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: PreApplicationStartMethod(typeof(Sitefinity.RequestSpy.RequestSpyInstaller), "PreApplicationStart")]
