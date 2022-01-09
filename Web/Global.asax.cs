using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.ModelUtil.Log;
using Es.Udc.DotNet.PracticaMaD.Web.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Util;
using Ninject;
using System;

namespace Es.Udc.DotNet.PracticaMaD.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application.Lock();

            IIoCManager ioCManager = new IoCManagerNinject();
            ioCManager.Configure();
            Application["ManagerIoC"] = ioCManager;

            Application.UnLock();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            SessionManager.TouchSession(Context);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            ((IKernel)Application["kernelIoC"]).Dispose();

            LogManager.RecordMessage("NInject kernel container disposed", MessageType.Info);
        }
    }
}