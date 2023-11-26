Imports Emploeey.Domain


Public Class _Default
    Inherits System.Web.UI.Page

    Dim departmentService As New ServicesDepartment()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindData()
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim departmentName As String = txtName.Text.Trim()

        departmentService.AddDepartment(departmentName)
        ClearEditControls()
        BindData()
    End Sub

    Protected Sub BindData()

        Dim departmentsList As List(Of Department) = departmentService.GetDepartments()

        Dim departmentsTable As New DataTable()
        departmentsTable.Columns.Add("ID", GetType(Integer))
        departmentsTable.Columns.Add("Name", GetType(String))

        For Each department As Department In departmentsList
            Dim row As DataRow = departmentsTable.NewRow()
            row("ID") = department.Id
            row("Name") = department.Name

            departmentsTable.Rows.Add(row)
        Next

        dgvDepartment.DataSource = departmentsTable
        dgvDepartment.DataBind()
    End Sub

    Protected Sub dgvDepartment_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvDepartment.RowCommand
        If e.CommandName = "DeleteDepartment" Then

            Dim departmentId As Integer = Convert.ToInt32(e.CommandArgument)

            departmentService.DeleteDepartment(departmentId)

            BindData()
        End If
    End Sub
    Private Sub ClearEditControls()
        txtId.Text = ""
        txtName.Text = ""
    End Sub
    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            Dim id As Integer = Integer.Parse(txtId.Text)
            Dim newName As String = txtName.Text.Trim()

            departmentService.UpdateDepartment(id, newName)
            ClearEditControls()
            BindData()
        Catch ex As Exception
            Console.WriteLine($"Error updating department: {ex.Message}")
        End Try
    End Sub
End Class