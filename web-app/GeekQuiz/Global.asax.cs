using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GeekQuiz
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //var cfgRootWebConfig = WebConfigurationManager.OpenWebConfiguration("~");

            //try { cfgRootWebConfig.ConnectionStrings.ConnectionStrings.Remove("MyDefaultConnection"); }
            //catch (Exception ex) { }

            //var connectionstringwin = "SERVER=10.93.158.84;DATABASE=geekquizmvcmysql4;UID=boss;PASSWORD=Beograd011";
            //var connectionstringlin = string.Format("SERVER=db;DATABASE={0};UID={1};PASSWORD={2}",
            //    Environment.GetEnvironmentVariable("DB_ENV_MYSQL_DATABASE"),
            //    Environment.GetEnvironmentVariable("DB_ENV_MYSQL_USER"),
            //    Environment.GetEnvironmentVariable("DB_ENV_MYSQL_PASSWORD")
            //    );

            //cfgRootWebConfig.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings()
            //{
            //    Name = "MyDefaultConnection",
            //    ConnectionString = Environment.GetEnvironmentVariable("DB_ENV_MYSQL_DATABASE") == null ?
            //    connectionstringwin : connectionstringlin
            //});

            //cfgRootWebConfig.Save();

            //var settings = ConfigurationManager.ConnectionStrings["MyDefaultConnection"].ConnectionString;
            //MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder(settings);
            //builder.ConnectionString = Environment.GetEnvironmentVariable("DB_ENV_MYSQL_DATABASE") == null ?
            //    connectionstringwin : connectionstringlin;


            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}