using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Flexi.Website.Utilities.Extentions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddTransientInNameSpace(this IServiceCollection serviceCollection, Assembly assembly, string @namespace) 
        {
            var types = assembly.GetTypes().Where(t => t.Namespace == @namespace && t.IsClass && t.IsPublic);

            foreach(var type in types)
            {
                serviceCollection.AddTransient(type);
            }
            
            return serviceCollection;
        }
    }
}