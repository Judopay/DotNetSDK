using System.Reflection;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
using System.Runtime.CompilerServices;

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
[assembly:
    InternalsVisibleTo("Judo.Tests.Base"),
    InternalsVisibleTo("Judo.Tests.Regression"),
    InternalsVisibleTo("JudoPayDotNetTests"), 
	InternalsVisibleTo("JudoPayDotNetDotNet"), 
	InternalsVisibleTo("JudoPayDotNetIntegrationTests"), 
	InternalsVisibleTo("JudoPayDotNetWindowsPhone"),
    InternalsVisibleTo("JudoDotNetXamarin"),
	InternalsVisibleTo("WindowsPhoneDotNetSDK")]
