using BlockChainInterview.Services;
using Microsoft.Ajax.Utilities;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace BlockChainInterview.Utilities
{
    public class DepedencyInjection
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            //container.RegisterType<eDocumentContext>();
            container.RegisterType<IEtherService, EtherService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}