using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Log;
using Es.Udc.DotNet.PracticaMaD.Model.Services.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Session.View.ApplicationObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
    public partial class UpdateUserProfile : SpecificCulturePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserSession userSession = SessionManager.GetUserSession(Context);
            if (userSession == null)
            {
                Response.Redirect("~/Pages/User/Authentication.aspx");
            }
            if (!IsPostBack)
            {
                UserDetails userDetails =
                    SessionManager.FindUserDetails(Context);

                txtFirstName.Text = userDetails.FirstName;
                txtSurname.Text = userDetails.LastName;
                txtEmail.Text = userDetails.Email;

                /* Combo box initialization */
                UpdateComboLanguage(userDetails.Language);
                UpdateComboCountry(userDetails.Language,
                    userDetails.Country);
            }
        }

        /// <summary>
        /// Loads the languages in the comboBox in the *selectedLanguage*.
        /// Also, the selectedLanguage will appear selected in the
        /// ComboBox
        /// </summary>
        private void UpdateComboLanguage(String selectedLanguage)
        {
            this.comboLanguage.DataSource = Languages.GetLanguages(selectedLanguage);
            this.comboLanguage.DataTextField = "text";
            this.comboLanguage.DataValueField = "value";
            this.comboLanguage.DataBind();
            this.comboLanguage.SelectedValue = selectedLanguage;
        }

        /// <summary>
        /// Loads the countries in the comboBox in the *selectedLanguage*.
        /// Also, the *selectedCountry* will appear selected in the
        /// ComboBox
        /// </summary>
        private void UpdateComboCountry(String selectedLanguage, String selectedCountry)
        {
            this.comboCountry.DataSource = Countries.GetCountries(selectedLanguage);
            this.comboCountry.DataTextField = "text";
            this.comboCountry.DataValueField = "value";
            this.comboCountry.DataBind();
            this.comboCountry.SelectedValue = selectedCountry;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UserDetails userDetailsVO =
                    new UserDetails(null, txtFirstName.Text, txtSurname.Text, txtEmail.Text,
comboLanguage.SelectedValue, comboCountry.SelectedValue);

                SessionManager.UpdateUserDetails(Context, userDetailsVO);

                Response.Redirect(Response.
                    ApplyAppPathModifier("~/Pages/MainPage.aspx"));
            }
        }

        protected void comboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateComboCountry(comboLanguage.SelectedValue,
                comboCountry.SelectedValue);
        }
    }
}