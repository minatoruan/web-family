using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace web_family.web.App_Start
{
    public class NinjectHttpDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectHttpDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Dispose()
        {
            
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}