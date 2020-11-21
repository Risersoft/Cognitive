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
using System.Net;
using risersoft.shared.portable.Model;
namespace CognitiveServiceRsMx
{

    /// <summary>
    /// Base Server Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    /// <remarks></remarks>
    public class ServerRepositoryBase<T, TId> : RepositoryBase<T, TId, T,bool, RSCallerInfo, HttpStatusCode> where T : BaseInfo
    {


        protected override HttpStatusCode GetStatus(bool IsError)
        {
            if (IsError)
                return HttpStatusCode.InternalServerError;
            else
                return HttpStatusCode.OK;
        }
        
    }
}
