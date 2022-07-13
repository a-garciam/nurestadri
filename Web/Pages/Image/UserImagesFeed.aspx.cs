using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using Es.Udc.DotNet.PracticaMaD.Web.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Image
{
    public partial class UserImagesFeed : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNoImages.Visible = false;
            lblFirstName.Visible = false;
            lblSurname.Visible = false;
            lblEmail.Visible = false;
            long userID;
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }
            try
            {
                userID = Int64.Parse(Request.Params.Get("userID"));
            }
            catch (ArgumentNullException)
            {
                userID = userSession.UserProfileId;

                lblFirstName.Visible = true;
                lblSurname.Visible = true;
                lblEmail.Visible = true;
            }

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();
            IImageService imageService = iocManager.Resolve<IImageService>();

            UserDetails user = userService.FindUserDetails(userID);
            lblUserName.Text = user.LoginName;
            lblFirstName.Text = user.FirstName + " ";
            lblSurname.Text = user.LastName;
            lblEmail.Text = user.Email;

            IList<ImageOutput> imageList = imageService.FindImagesByUser(userID);

            if (imageList.Count() <= 0)
            {
                lblNoImages.Visible = true;
                return;
            }
            Session["lstImg"] = imageList;

            gvUserImages.DataSource = imageList;
            gvUserImages.AllowPaging = true;
            gvUserImages.DataBind();
        }

        protected void gvUserImages_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvUserImages.PageIndex = e.NewPageIndex;
                gvUserImages.DataSource = Session["lstImg"];
                gvUserImages.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}