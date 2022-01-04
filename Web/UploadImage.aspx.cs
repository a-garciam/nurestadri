using Es.Udc.DotNet.ModelUtil.IoC;
using Es.Udc.DotNet.PracticaMaD.Model.Services.ImageService;
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

        }

        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
            if (fuImageUpload.HasFile)
            {
                try
                {
                    string filename = Server.MapPath("~/images" + "/userName/");
                    Directory.CreateDirectory(filename);
                    filename = filename + DateTime.Now.ToString("yyyyMMddHHmm") + fuImageUpload.FileName;
                    fuImageUpload.SaveAs(filename);

                    IIoCManager iocManager = (IIoCManager)HttpContext.Current.Application["managerIoC"];
                    IImageService imageService = iocManager.Resolve<IImageService>();

                    imageService.UploadImage();
                    Label1.Text = filename;
                }
                catch (Exception)
                {
                }
            }
        }
    }
}