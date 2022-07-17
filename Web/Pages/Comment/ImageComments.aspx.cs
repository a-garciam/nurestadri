using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService.Resources;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using Es.Udc.DotNet.PracticaMaD.Web.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class ImageComments : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;
            lblNoComments.Visible = false;

            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }
            if (Request.Params.Get("imageID") == null)
            {
                Response.Redirect("~/Pages/Feedback/InternalError.aspx");
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

            long userId = userSession.UserProfileId;
            long imageId = Int64.Parse(Request.Params.Get("imageID"));

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            ICommentService commentService = iocManager.Resolve<ICommentService>();

            CommentBlock commentBlock = commentService.FindCommentsByImage(imageId, startIndex, count);

            if (commentBlock.Comments.Count() == 0)
            {
                lblNoComments.Visible = true;
                return;
            }
            gv_ImageComments.DataSource = commentBlock.Comments;
            gv_ImageComments.AllowPaging = true;
            gv_ImageComments.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = "~/Pages/Comment/ImageComments.aspx" + "?imageID=" + imageId +
                    "&startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (commentBlock.ExistMore)
            {
                String url = "~/Pages/Comment/ImageComments.aspx" + "?imageID=" + imageId +
                "&startIndex=" + (startIndex + count) + "&count=" + count;
                Trace.Warn(url);
                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }
    }
}