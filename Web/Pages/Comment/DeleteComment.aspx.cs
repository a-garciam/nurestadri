using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService.Resources.Output;
using Es.Udc.DotNet.PracticaMaD.Web.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class DeleteComment : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            long userId = userSession.UserProfileId;
            try
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICommentService commentService = iocManager.Resolve<ICommentService>();

                commentService.DeleteComment(userId, commentId);
            }
            catch (Exception)
            {
                Response.Redirect("~/Pages/Feedback/InternalError.aspx");
            }
        }
    }
}