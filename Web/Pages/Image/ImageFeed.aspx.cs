using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output;
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
    public partial class ImageFeed : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;

            lnkPrevious.Visible = false;
            lnkNext.Visible = false;

            lblNoImages.Visible = false;
            UserSession userSession = SessionManager.GetUserSession(Context);

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
            IImageService imageService = iocManager.Resolve<IImageService>();

            string keyword = Request.Params.Get("keyword");
            ImageBlock imageList;

            if (Request.Params.Get("categoryID") != null)
            {
                long categoryId = Int64.Parse(Request.Params.Get("categoryID"));
                imageList = imageService.FindImagesByFilterAndCategory(keyword, categoryId, startIndex, count);
            }
            else if (keyword != null)
            {
                imageList = imageService.FindImagesByFilter(keyword, startIndex, count);
            }
            else
            {
                imageList = imageService.FindAllImages(startIndex, count);
            }

            if (imageList.Images.Count() <= 0)
            {
                Trace.Warn("0 elements");
                lblNoImages.Visible = true;
                return;
            }
            gvImageFeed.DataSource = imageList.Images;
            gvImageFeed.AllowPaging = true;
            gvImageFeed.DataBind();
            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url = "~/Pages/Image/ImageFeed.aspx" +
                    "?startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (imageList.ExistMoreImages)
            {
                String url =
                    "~/Pages/Image/ImageFeed.aspx" +
                    "?startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }

        protected void btnFilterImages_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Image/FilterImages.aspx");
        }
    }
}