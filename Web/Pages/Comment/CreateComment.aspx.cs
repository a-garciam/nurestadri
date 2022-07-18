using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class CreateComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrorLength.Visible = false;
            lblError.Visible = false;
            if (!IsPostBack)
            {
                UserSession userSession = SessionManager.GetUserSession(Context);
                if (userSession == null)
                {
                    Response.Redirect("~/Pages/User/Authentication.aspx");
                }
                if (Request.Params.Get("imageID") == null)
                {
                    Response.Redirect("~/Pages/Feedback/InternalError.aspx");
                }
            }
        }

        protected void btnComment_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    UserSession userSession = SessionManager.GetUserSession(Context);

                    if (Request.Params.Get("imageID") == null)
                    {
                        Response.Redirect("~/Pages/Feedback/InternalError.aspx");
                    }
                    long imageId = Int64.Parse(Request.Params.Get("imageID"));

                    IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                    ICommentService commentService = iocManager.Resolve<ICommentService>();

                    commentService.CommentImage(userSession.UserProfileId, imageId, tbComment.Text);

                    String url = "~/Pages/Comment/ImageComments.aspx" + "?imageID=" + imageId;

                    Response.Redirect(url);
                }
                catch (ExceededLengthException)
                {
                    lblErrorLength.Text += "200";
                    lblErrorLength.Visible = true;
                }
                catch (Exception exc)
                {
                    lblError.Visible = true;
                    Trace.Warn(exc.ToString());
                }
            }
        }
    }
}