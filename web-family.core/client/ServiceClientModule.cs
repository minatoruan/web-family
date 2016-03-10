using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using web_family.core.helper;

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
                if (type.IsServiceContract())
                {
                    yield return new Tuple<string, Type>(type.GetServiceName(), type);
                }
            }
        }

        public override void Load()
        {
            foreach (var bindingdata in GetTypesWithServiceContract())
            {
                Kernel.Bind(bindingdata.Item2)
                    .ToMethod(m =>
                    {
                        var servicename = bindingdata.Item1;
                        var type = bindingdata.Item2;
                        var endpoint = GetCurrentUriService(servicename);
                        var generictype = typeof(ChannelFactory<>).MakeGenericType(type);
                        var constructor = generictype.GetConstructor(new Type[] { typeof(Binding), typeof(EndpointAddress) });
                        var obj = constructor.Invoke(new object[] { new BasicHttpBinding(), endpoint });
                        var methodInfo = obj.GetType().GetMethod("CreateChannel", new Type[] { });
                        return methodInfo.Invoke(obj, new object[] { });
                    }).InSingletonScope();
            }
        }
    }
}
