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
    public partial class FilterImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
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
                ddlCategory.DataSource = list;
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception)
            {
                //lblError.Text = "Error al cargar las categorias";
            }
        }

        protected void BtnFilterImages_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    UserSession userSession = SessionManager.GetUserSession(Context);
                    if (userSession == null)
                    {
                        Response.Redirect("~/Pages/User/Authentication.aspx");
                    }
                    string keyword = tbKeyword.Text.Trim();
                    string url =
                    String.Format("./ImageFeed.aspx?keyword={0}", keyword);
                    if (ddlCategory.SelectedValue != null && ddlCategory.SelectedValue != "")
                    {
                        long categoryId = Convert.ToInt64(ddlCategory.SelectedValue);
                        url = String.Format("./ImageFeed.aspx?keyword={0}&categoryID={1}", keyword, categoryId.ToString());
                    }
                    Response.Redirect(Response.ApplyAppPathModifier(url));
                }
                catch (Exception)
                {
                    lblError.Text = "Se ha producido un error";
                    lblError.Visible = true;
                }
            }
        }
    }
}