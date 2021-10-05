using System.Diagnostics;
using System.Reflection;

namespace ErrorHandling.Domain {

      public static class Sources {

        private static readonly AssemblyName AssemblyName
        = typeof(Sources).Assembly.GetName();
        internal static readonly ActivitySource ActivitySource
            = new(AssemblyName.Name, AssemblyName.Version.ToString());

        public static readonly ActivitySource DemoSource = new("ErrorHandling");

    }

}