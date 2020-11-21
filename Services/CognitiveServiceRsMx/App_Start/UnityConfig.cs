using Microsoft.VisualBasic;
using risersoft.shared.portable.Models.Nav;
using risersoft.shared.web;
using risersoft.shared.cloud.Cache;
using risersoft.shared.web.Controllers;
using risersoft.shared.cloud.IRepository;
using risersoft.shared.cloud.Providers;
using risersoft.shared.cloud.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Http;
using Unity.WebApi;
using Unity;

namespace CognitiveServiceRsMx
{

    public class UnityConfig
    {

        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();
            // register all your components with the container here
            // it Is Not necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            //RegisterMockObjects(container)
            RegisterObjects(container);
            config.DependencyResolver = new UnityDependencyResolver(container);

        }

        private static void RegisterObjects(UnityContainer container)
        {
            container.RegisterType<ISampleRepository, SampleRepository>();

        }

        //Private Shared Sub RegisterMockObjects(container As UnityContainer)
        //    container.RegisterType(Of IFileRepository, FileMockRepository)
        //End Sub
    }
}
