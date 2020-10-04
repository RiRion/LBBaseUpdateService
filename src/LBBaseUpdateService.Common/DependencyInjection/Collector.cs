using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace LBBaseUpdateService.Common.DependencyInjection
{
    public class Collector
    {
        public static Assembly[] GetAssemblies(string partOfName)
        {
            return DependencyContext.Default.CompileLibraries
                .Where(c => c.Name.Contains(partOfName))
                .Select(a => Assembly.Load(new AssemblyName(a.Name)))
                .ToArray();
        }

        public static Assembly GetAssembly(string name)
        {
            return Assembly.Load(DependencyContext.Default.CompileLibraries
                .First(c => c.Name.Equals(name)).Name);
        }
    }
}