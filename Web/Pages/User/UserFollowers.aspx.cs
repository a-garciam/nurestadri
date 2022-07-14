﻿using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService.Resources.Output;
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
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IUserService userService = iocManager.Resolve<IUserService>();

            UserFollows userFollowers = userService.FindUserFollowers(userID);
            lblNumberFollowers.Text = userFollowers.NumberFollows.ToString();
            lblUserName.Text = userFollowers.UserName;
            if (userFollowers.NumberFollows == 0)
            {
                lblNoFollows.Visible = true;
                return;
            }
            gvFollowers.DataSource = userFollowers.FollowList;
            gvFollowers.AllowPaging = true;
            gvFollowers.DataBind();
        }
    }
}