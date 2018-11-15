using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServerControl
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ServerCalendar runat=server></{0}:ServerCalendar>")]
    public class ServerCalendar : WebControl
    {
        #region DECLARATION
        TextBox txtDate;
        ImageButton imgDate;
        Calendar cldDate;
        #endregion

        #region CALLING CHILDCONTROL
        protected override void CreateChildControls()
        {
            txtDate = new TextBox();
            txtDate.ID = "txtDate";
            txtDate.Width = Unit.Pixel(150);

            imgDate = new ImageButton();
            imgDate.ID = "imgDate";
            imgDate.Click += ImgDate_Click;

            cldDate = new Calendar();
            cldDate.ID = "cldDate";
            cldDate.Visible = false;
            cldDate.PrevMonthText = "";
            cldDate.SelectionChanged += CldDate_SelectionChanged;
            cldDate.DayRender += CldDate_DayRender;
            cldDate.VisibleMonthChanged += CldDate_VisibleMonthChanged;
            this.Controls.Add(txtDate);
            this.Controls.Add(imgDate);
            this.Controls.Add(cldDate);
        }
        #endregion

        #region RESUABLE METHOD
        private bool IsSecondSat(DateTime dt)
        {
            if (dt.Day < 8 || dt.Day > 14)
                return false;
            else
                return dt.DayOfWeek == DayOfWeek.Saturday;
        }
        #endregion

        #region EVENT HANDLERS
        private void CldDate_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            if (e.NewDate.Month == DateTime.Now.Month&&e.NewDate.Year==DateTime.Now.Year)
                cldDate.PrevMonthText = "";
            else
                cldDate.PrevMonthText = "&lt;";
        }

        private void CldDate_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date.DayOfYear < DateTime.Now.DayOfYear && e.Day.Date.Year == DateTime.Now.Year || e.Day.Date.DayOfWeek==DayOfWeek.Sunday ||IsSecondSat(e.Day.Date))
            {
                e.Cell.ForeColor = System.Drawing.Color.Red;
                e.Day.IsSelectable = false;
            }
        }

        private void CldDate_SelectionChanged(object sender, EventArgs e)
        {
            txtDate.Text = cldDate.SelectedDate.ToLongDateString();
            cldDate.Visible = false;
        }

        private void ImgDate_Click(object sender, ImageClickEventArgs e)
        {
            if (cldDate.Visible)
                cldDate.Visible = false;
            else
                cldDate.Visible = true;
        }
        #endregion

        #region PROPERTIES
        public DateTime SelectedDate
        {
            get
            {
                EnsureChildControls();
                return cldDate.SelectedDate;
            }
            set
            {
                EnsureChildControls();
                cldDate.SelectedDate = value;
            }
        }

        public string ImageUrl
        {
            get
            {
                EnsureChildControls();
                return imgDate.ImageUrl;
            }
            set
            {
                EnsureChildControls();
                imgDate.ImageUrl = value;
            }
        }
        #endregion

        #region RENDERING

        public override void RenderControl(HtmlTextWriter writer)
        {
            txtDate.RenderControl(writer);
            imgDate.RenderControl(writer);
            cldDate.RenderControl(writer);
        }
        #endregion
    }
}
