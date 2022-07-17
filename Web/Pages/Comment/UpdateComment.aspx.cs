using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService.Resources.Output;
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
    public partial class UpdateComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblErrorLength.Visible = false;
                lblError.Visible = false;
                lblSuccess.Visible = false;
                UserSession userSession = SessionManager.GetUserSession(Context);
                if (userSession == null)
                {
                    Response.Redirect("~/Pages/User/Authentication.aspx");
                }
                if (Request.Params.Get("commentID") == null)
                {
                    Response.Redirect("~/Pages/Feedback/InternalError.aspx");
                }
                long commentId = Int64.Parse(Request.Params.Get("commentID"));
                try
                {
                    IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                    ICommentService commentService = iocManager.Resolve<ICommentService>();

                    CommentOutput comment = commentService.FindCommentsById(commentId);
                    tbComment.Text = comment.CommentText;
                }
                catch (Exception)
                {
                    Response.Redirect("~/Pages/Feedback/InternalError.aspx");
                }
            }
        }

        protected void btnUpdateComment_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    UserSession userSession = SessionManager.GetUserSession(Context);

                    if (Request.Params.Get("commentID") == null)
                    {
                        Response.Redirect("~/Pages/Feedback/InternalError.aspx");
                    }
                    long commentId = Int64.Parse(Request.Params.Get("commentID"));

                    IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                    ICommentService commentService = iocManager.Resolve<ICommentService>();

                    commentService.UpdateComment(userSession.UserProfileId, commentId, tbComment.Text);

                    lblSuccess.Visible = true;
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