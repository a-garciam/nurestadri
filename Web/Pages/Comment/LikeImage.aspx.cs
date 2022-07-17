using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output;
using Es.Udc.DotNet.PracticaMaD.Web.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class LikeImage : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                long imageId = Int64.Parse(Request.Params.Get("imageID"));
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();
                try
                {
                    ImageDetailsOutput image = imageService.FindImageById(imageId);

                    imgFile.ImageUrl = image.ImagePath;

                    lblUser.Text = image.UserName;
                    lblTitle.Text = image.Title;
                }
                catch (InstanceNotFoundException)
                {
                    Response.Redirect("~/Pages/Feedback/InternalError.aspx");
                }
            }
        }

        protected void btnLike_Click(object sender, EventArgs e)
        {
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }
            try
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                ICommentService commentService = iocManager.Resolve<ICommentService>();
                commentService.LikeImage(Int64.Parse(Request.Params.Get("imageID")), userSession.UserProfileId);
                Response.Redirect("~/Pages/Image/ImageDetails.aspx?imageID=" + Request.Params.Get("imageID"));
            }
            catch (Exception)
            {
            }
        }
    }
}