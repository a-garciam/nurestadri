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
    public partial class ImageFeed : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNoImages.Visible = false;
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }

            IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
            IImageService imageService = iocManager.Resolve<IImageService>();

            string keyword = Request.Params.Get("keyword");
            IList<ImageOutput> imageList = new List<ImageOutput>();

            if (Request.Params.Get("categoryID") != null)
            {
                long categoryId = Int64.Parse(Request.Params.Get("categoryID"));
                imageList = imageService.FindImagesByFilterAndCategory(keyword, categoryId);
            }
            else if (keyword != null)
            {
                imageList = imageService.FindImagesByFilter(keyword);
            }
            else
            {
                imageList = imageService.FindAllImages();
            }

            if (imageList.Count() <= 0)
            {
                Trace.Warn("0 elements");
                lblNoImages.Visible = true;
                return;
            }
            Session["lstImg"] = imageList;
            gvImageFeed.DataSource = imageList;
            gvImageFeed.AllowPaging = true;
            gvImageFeed.DataBind();
        }

        protected void gvImageFeed_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvImageFeed.PageIndex = e.NewPageIndex;
                gvImageFeed.DataSource = Session["lstImg"];
                gvImageFeed.DataBind();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void btnFilterImages_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Image/FilterImages.aspx");
        }
    }
}