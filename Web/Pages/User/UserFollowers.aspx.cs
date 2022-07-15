using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService.Resources.Output;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using Es.Udc.DotNet.PracticaMaD.Web.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class UserFollowers : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            long userID;
            lblNoFollows.Visible = false;
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

            UserFollows userFollowers = userService.FindUserFollowers(userID, startIndex, count);
            lblUserName.Text = userFollowers.UserName;
            if (userFollowers.FollowList.Count == 0)
            {
                lblNoFollows.Visible = true;
                return;
            }
            gvFollowers.DataSource = userFollowers.FollowList;
            gvFollowers.AllowPaging = true;
            gvFollowers.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = "~/Pages/User/UserFollowers.aspx" + "?userID=" + userID +
                    "&startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (userFollowers.ExistMore)
            {
                String url =
                    "~/Pages/User/UserFollowers.aspx" + "?userID=" + userID +
                    "&startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }
    }
}