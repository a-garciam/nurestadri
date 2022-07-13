using Es.Udc.DotNet.ModelUtil.IoC;
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
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }
            if (Request.Params.Get("imageID") == null)
            {
                Response.Redirect("~/Pages/User/MainPage.aspx");
            }
            long imageId = Int64.Parse(Request.Params.Get("imageID"));
            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageService imageService = iocManager.Resolve<IImageService>();

            ImageDetailsOutput image = imageService.FindImageById(imageId);

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
        }
    }
}