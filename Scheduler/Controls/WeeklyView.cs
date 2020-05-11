using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduler.Controls
{
    public partial class WeeklyView : UserControl
    {
        public WeeklyView()
        {
            InitializeComponent();
            PopulateDates();
        }

        public void PopulateDates()
        {
            int diff = (7 + (DateTime.Now.DayOfWeek - DayOfWeek.Sunday)) % 7;
            var beginningOfWeek = DateTime.Now.AddDays(-1 * diff).Date;

            lblDate1.Text = beginningOfWeek.ToString("dd");
            lblDate2.Text = beginningOfWeek.AddDays(1).ToString("dd");
            lblDate3.Text = beginningOfWeek.AddDays(2).ToString("dd");
            lblDate4.Text = beginningOfWeek.AddDays(3).ToString("dd");
            lblDate5.Text = beginningOfWeek.AddDays(4).ToString("dd");
            lblDate6.Text = beginningOfWeek.AddDays(5).ToString("dd");
            lblDate7.Text = beginningOfWeek.AddDays(6).ToString("dd");
        }
    }
}
