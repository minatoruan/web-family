using System;
using System.Linq;
using System.ServiceModel;

namespace web_family.core.helper
{
    public static class ServiceNameHelper
    {
        public static string GetServiceName(this Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(ServiceContractAttribute), true)
                                .Cast<ServiceContractAttribute>().Any();
            if (attribute == false || !type.IsInterface)
            {
                throw new InvalidOperationException("Type should be a service contract");
            }
            return type.Name.Substring(1).ToLower();
        }

        public static bool IsServiceContract(this Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(ServiceContractAttribute), true)
                                .Cast<ServiceContractAttribute>().Any();
            return (attribute && type.IsInterface);
        }
    }
}
