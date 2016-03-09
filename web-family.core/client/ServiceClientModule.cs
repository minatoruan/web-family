using Ninject.Modules;
using Ninject.Planning.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Xml;

namespace web_family.core.client
{
    public class ServiceClientModule : NinjectModule
    {
        private const string _urlparttern = "http://localhost:8082/{0}";
        /*
        private BasicHttpBinding _serviceBinding = new BasicHttpBinding
        {
            CloseTimeout = new TimeSpan(0, 1, 0),
            OpenTimeout = new TimeSpan(0, 1, 0),
            ReceiveTimeout = new TimeSpan(0, 10, 0),
            SendTimeout = new TimeSpan(0, 10, 0),
            AllowCookies = false,
            BypassProxyOnLocal = false,
            HostNameComparisonMode = HostNameComparisonMode.StrongWildcard,
            MaxBufferSize = 2147483647,
            MaxBufferPoolSize = 2147483647,
            MaxReceivedMessageSize = 2147483647,
            MessageEncoding = WSMessageEncoding.Text,
            TextEncoding = new UTF8Encoding(),
            TransferMode = TransferMode.Buffered,
            UseDefaultWebProxy = true,
            ReaderQuotas = new XmlDictionaryReaderQuotas
            {
                MaxDepth = 64,
                MaxStringContentLength = 2147483647,
                MaxArrayLength = 2147483647,
                MaxBytesPerRead = 2147483647,
                MaxNameTableCharCount = 2147483647
            },
            Security = new BasicHttpSecurity
            {
                Mode = BasicHttpSecurityMode.None,
                Transport = new HttpTransportSecurity
                {
                    ClientCredentialType = HttpClientCredentialType.None,
                    ProxyCredentialType = HttpProxyCredentialType.None,
                    Realm = ""
                },
                Message = new BasicHttpMessageSecurity
                {
                    ClientCredentialType = BasicHttpMessageCredentialType.UserName
                }
            }
        };
        */

        public ServiceClientModule()
            : base()
        {
        }

        private EndpointAddress GetCurrentUriService(string servicename)
        {
            return new EndpointAddress(new Uri(string.Format(_urlparttern, servicename)));
        }

        private IEnumerable<Tuple<string, Type>> GetTypesWithServiceContract()
        {
            foreach (var type in this.GetType().Assembly.GetTypes())
            {
                if (type.Namespace.Equals("web_family.core") &&
                    type.GetCustomAttributes(typeof(ServiceContractAttribute), true).Any())
                {
                    var attribute = type.GetCustomAttributes(typeof(ServiceContractAttribute), true)
                                        .Cast<ServiceContractAttribute>().FirstOrDefault();
                    yield return new Tuple<string, Type>(attribute.Name, type);
                }
            }
        }

        public override void Load()
        {
            /*
            foreach(var bindingdata in GetTypesWithServiceContract())
            {
                Kernel.Bind(bindingdata.Item2)
                    .ToMethod(m =>
                    {
                        var serivcename = bindingdata.Item1;
                        var type = bindingdata.Item2;
                        var endpoint = GetCurrentUriService(serivcename);
                        return Activator.CreateInstance(type, new BasicHttpBinding(), endpoint);
                    }).InSingletonScope();
            }*/

            Kernel.Bind<IUpgradeService>().ToMethod<IUpgradeService>(x =>
            {
                var endpoint = GetCurrentUriService("UpgradeService");
                return ChannelFactory<IUpgradeService>.CreateChannel(new BasicHttpBinding(), endpoint);
            });
        }
    }
}
