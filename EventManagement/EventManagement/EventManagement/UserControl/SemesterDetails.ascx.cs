using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventManagement.UserControl
{
    public partial class SemesterDetails : System.Web.UI.UserControl
    {
        void Page_Load(object sender, EventArgs e)
        {
            String semester, year, courseName;

            //Getting the current month to display the semester
            Int32 month = DateTime.Now.Month;
            if (month <= 5) semester = "Spring";
            else if (month <= 7) semester = "Summer";
            else semester = "Fall";

            Semesterlbl.Text = semester;

            //Getting the current year
            Int32 Year = DateTime.Now.Year;
            year = Year.ToString();

            Yearlbl.Text = year;

            //Displaying the course name 
            courseName = "ASU CSE 445- Distributed Software Computing";

            CourseNamelbl.Text = courseName;


        }
    }
}