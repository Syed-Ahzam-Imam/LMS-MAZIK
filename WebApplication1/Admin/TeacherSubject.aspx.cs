using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static WebApplication1.Models.CommonFn;

namespace WebApplication1.Admin
{
    public partial class TeacherSubject : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClass();
                GetTeacher();
                GetTeacherSubject();
            }
        }

        private void GetClass()
        {
            DataTable dt = fn.Fetch("Select * from Class");
            ddlclass.DataSource = dt;
            ddlclass.DataTextField = "ClassName";
            ddlclass.DataValueField = "ClassId";
            ddlclass.DataBind();
            ddlclass.Items.Insert(0, "Select Class");
        }

        private void GetTeacher()
        {
            DataTable dt = fn.Fetch("Select * from Teacher");
            ddlTeacher.DataSource = dt;
            ddlTeacher.DataTextField = "Name";
            ddlTeacher.DataValueField = "TeacherId";
            ddlTeacher.DataBind();
            ddlTeacher.Items.Insert(0, "Select Teacher");
        }

        private void GetTeacherSubject()
        {
            DataTable dt = fn.Fetch(@"SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS [Sr.No],
                                    ts.Id, ts.ClassId, c.ClassName, ts.SubjectId, s.SubjectName, ts.TeacherId, t.Name
                                    FROM TeacherSubject ts INNER JOIN Class c ON ts.ClassId = c.ClassId INNER JOIN Subject s ON ts.SubjectId = s.SubjectId
                                    INNER JOIN Teacher t ON ts.TeacherId = t.TeacherId; ");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void ddlclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            string classId = ddlclass.SelectedValue;
            DataTable dt = fn.Fetch("Select * from Subject where ClassId = '" + classId + "' ");
            ddlSubject.DataSource = dt;
            ddlSubject.DataTextField = "SubjectName";
            ddlSubject.DataValueField = "SubjectId";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, "Select Subject");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string classId = ddlclass.SelectedItem.Text;
                string subjectId = ddlSubject.SelectedItem.Text;
                string teacherId = ddlTeacher.SelectedItem.Text;
                DataTable dt = fn.Fetch("Select * from TeacherSubject where ClassId = '" + classId +
                                        "' and SubjectName = '" + subjectId + "' and TeacherId = '" + teacherId + "' ");
                if (dt.Rows.Count == 0)
                {
                    string query = "Insert into TeacherSubject values('" + classId + "', '" + subjectId + "', '" + teacherId + "')";
                    fn.Query(query);
                    LabelMsg.Text = "Inserted Successfully!";
                    LabelMsg.CssClass = "alert alert-success";
                    ddlclass.SelectedIndex = 0;
                    ddlSubject.SelectedIndex = 0;
                    ddlTeacher.SelectedIndex = 0;
                    GetTeacherSubject();
                }
                else
                {
                    LabelMsg.Text = "Entered <b>Teacher</b> already exists";
                    LabelMsg.CssClass = "alert alert-danger";
                }

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
    }
}