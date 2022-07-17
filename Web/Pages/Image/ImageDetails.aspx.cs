using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions;
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
    public partial class ImageDetails : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnDeleteImage.Visible = false;
            lblDeleteError.Visible = false;
            lblPermissionError.Visible = false;
            lblLike.Visible = false;
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
            ICommentService commentService = iocManager.Resolve<ICommentService>();
            try
            {
                ImageDetailsOutput image = imageService.FindImageById(imageId);

                lblLike.Visible = commentService.FindLike(imageId, userSession.UserProfileId);
                imgFile.ImageUrl = image.ImagePath;
                lblUser.Text = image.UserName;
                lblTitle.Text = image.Title;
                lblDescription.Text = image.Description;
                lblCategory.Text = image.CategoryName;
                lblCreationDate.Text = image.CreationDate.ToString();
                lblAperture.Text = image.Aperture;
                lblBalance.Text = image.Balance;
                lblExposure.Text = image.Exposure;
                lblLikes.Text = image.Likes.ToString();
                if (image.UserId == userSession.UserProfileId)
                {
                    btnDeleteImage.Visible = true;
                }
            }
            catch (InstanceNotFoundException)
            {
                Response.Redirect("~/Pages/Feedback/InternalError.aspx");
            }
        }

        protected void btnDeleteImage_Click(object sender, EventArgs e)
        {
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }
            try
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();

                imageService.DeleteImage(Int64.Parse(Request.Params.Get("imageID")), userSession.UserProfileId);
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/Feedback/SuccessfulOperation.aspx"));
            }
            catch (OperationNotAllowedException)
            {
                lblPermissionError.Visible = false;
            }
        }
    }
}