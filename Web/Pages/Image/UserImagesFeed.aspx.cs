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
    public partial class UserImagesFeed : System.Web.UI.Page
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

            IList<ImageOutput> imageList = imageService.FindImagesByUser(userID);

            if (imageList.Count() <= 0) {
                Label1.Text=imageList.Count().ToString();
                return;
            }

            gvUserImages.DataSource = imageList;
            gvUserImages.DataBind();
        }
    }
}