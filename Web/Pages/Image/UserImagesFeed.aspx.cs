using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources;
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
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            bool ownProfile = false;
            lblNoImages.Visible = false;
            lblNameTitle.Visible = false;
            lblFirstName.Visible = false;
            lblSurname.Visible = false;
            lblEmailTitle.Visible = false;
            lblEmail.Visible = false;
            btnUnfollow.Visible = false;
            btnFollow.Visible = false;

            long userID;
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }
            try
            {
                userID = Int64.Parse(Request.Params.Get("userID"));
                if (userID == userSession.UserProfileId)
                {
                    ownProfile = true;
                }
            }
            catch (ArgumentNullException)
            {
                userID = userSession.UserProfileId;
                ownProfile = true;
            }
            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = Settings.Default.PracticaMaD_defaultCount;
            }

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();
            IImageService imageService = iocManager.Resolve<IImageService>();

            UserDetails user = userService.FindUserDetails(userID);
            Session["userId"] = userID;
            lblUserName.Text = user.LoginName;
            if (ownProfile)
            {
                lblFirstName.Visible = true;
                lblSurname.Visible = true;
                lblNameTitle.Visible = true;
                lblEmail.Visible = true;
                lblEmailTitle.Visible = true;
                lblFirstName.Text = user.FirstName + " ";
                lblSurname.Text = user.LastName;
                lblEmail.Text = user.Email;
            }
            else
            {
                if (userService.IsFollowing(userSession.UserProfileId, userID))
                {
                    btnUnfollow.Visible = true;
                }
                else
                {
                    btnFollow.Visible = true;
                }
            }

            ImageBlock imageList = imageService.FindImagesByUser(userID, startIndex, count);

            if (imageList.Images.Count() <= 0)
            {
                lblNoImages.Visible = true;
                return;
            }
            hlFollowers.NavigateUrl = Response.ApplyAppPathModifier("~/Pages/User/UserFollowers.aspx?userID=" + userID);
            hlFollowed.NavigateUrl = Response.ApplyAppPathModifier("~/Pages/User/UserFollowed.aspx?userID=" + userID);
            gvUserImages.DataSource = imageList.Images;
            gvUserImages.AllowPaging = true;
            gvUserImages.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = "~/Pages/Image/UserImagesFeed.aspx" + "?userID=" + userID +
                    "&startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (imageList.ExistMoreImages)
            {
                String url =
                    "~/Pages/Image/UserImagesFeed.aspx" + "?userID=" + userID +
                    "&startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }

        protected void btnFollow_Click(object sender, EventArgs e)
        {
            long userID;
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }
            userID = Int64.Parse(Request.Params.Get("userID"));

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            userService.FollowUser(userSession.UserProfileId, userID);
            btnFollow.Visible = !btnFollow.Visible;
            btnUnfollow.Visible = !btnUnfollow.Visible;
        }
    }
}