using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

public static class ServiceCollectionExtensions
{
    public static void AddAttributedServices(this IServiceCollection services, Assembly assembly)
    {
        var typesWithAttribute = assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<ScopedServiceAttribute>() != null)
            .ToList();

        foreach (var type in typesWithAttribute)
        {
            var interfaces = type.GetInterfaces().ToList();
            if (interfaces.Count == 1) // Register the service with its implemented interface
            {
                services.AddScoped(interfaces.First(), type);
            }
            else if (interfaces.Count > 1) // Handle cases where the class implements multiple interfaces
            {
                foreach (var iface in interfaces)
                {
                    services.AddScoped(iface, type);
                }
            }
            else // Register the service as itself if it doesn't implement any interface
            {
                services.AddScoped(type);
            }
        }
    }
}
