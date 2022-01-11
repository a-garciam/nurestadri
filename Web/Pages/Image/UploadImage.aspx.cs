using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService.Resources.Output;
using Es.Udc.DotNet.PracticaMaD.Web.Session;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web
{
    public partial class UploadImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDDLCategories();
            }
        }

        private void InitDDLCategories()
        {
            try
            {
                IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                IImageService imageService = iocManager.Resolve<IImageService>();
                IList<CategoryOutput> list = imageService.FindCategories();
                ListItem i;
                foreach (CategoryOutput c in list)
                {
                    i = new ListItem(c.Name.ToString(), c.CategoryId.ToString());
                    ddlCategory.Items.Add(i);
                }
            }
            catch (Exception exc)
            {
                lblUploadCompleted.Text = exc.ToString();
            }
        }

        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                //if (fuImageUpload.HasFile)
                //{

                    try
                    {
                        UserSession userSession = SessionManager.GetUserSession(Context);
                        if (userSession == null)
                        {
                            Response.Redirect("~/Pages/User/Authentication.aspx");
                        }
                    string filename = Server.MapPath("~/images/" + userSession.UserProfileId + "/");
                        Directory.CreateDirectory(filename);
                        filename = filename + DateTime.Now.ToString("yyyyMMddHHmm") + fuImageUpload.FileName;
                        fuImageUpload.SaveAs(filename);

                        IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                        IImageService imageService = iocManager.Resolve<IImageService>();

                        IList<CategoryOutput> list = imageService.FindCategories();
                    long categoryId = Convert.ToInt64(ddlCategory.SelectedValue);

                        imageService.UploadImage(userSession.UserProfileId,categoryId, tbTitle.Text, 
                            tbDescription.Text, tbAperture.Text, tbExposure.Text, tbBalance.Text, filename);
                        lblUploadCompleted.Text = filename;
                    }
                    catch (Exception exc)
                    {
                        lblUploadCompleted.Text = exc.ToString();
                    }

            //}
            }
        }

        protected void tbDescription_TextChanged(object sender, EventArgs e)
        {

        }
    }
}