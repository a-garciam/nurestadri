using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output;
using Es.Udc.DotNet.PracticaMaD.Web.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Image
{
    public partial class ImageFeed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }
            long userID = userSession.UserProfileId;

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageService imageService = iocManager.Resolve<IImageService>();

            IList<ImageOutput> imageList = imageService.FindAllImages();

            if (imageList.Count() <= 0)
            {
                Trace.Warn("0 elements");
                return;
            }
            Session["lstImg"] = imageList;
            gvImageFeed.DataSource = imageList;
            gvImageFeed.AllowPaging = true;
            gvImageFeed.DataBind();
        }

        protected void gvImageFeed_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvImageFeed.PageIndex = e.NewPageIndex;
                gvImageFeed.DataSource = Session["lstImg"];
                gvImageFeed.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}