Imports Emploeey.Domain

Public Class Employee
    Inherits System.Web.UI.Page
    Dim employeeService As New ServicesEmployee()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                ' Load departments
                LoadDepartments()
                Console.WriteLine("LoadDepartments executed")

                ' Load other data or perform other actions
                BindData()
                Console.WriteLine("BindData executed")

                txtId.Text = Convert.ToString(employeeService.GetMaxId())

                Console.WriteLine("GetMaxId executed")
            Catch ex As Exception
                Console.WriteLine($"Error in Page_Load: {ex.Message}")
            End Try
        End If


    End Sub


    Protected Sub LoadDepartments()
        Dim departments As List(Of Department) = employeeService.GetDepartments()

        If departments IsNot Nothing AndAlso departments.Count > 0 Then
            ddlDepartment.DataSource = departments
            ddlDepartment.DataTextField = "Name"
            ddlDepartment.DataValueField = "Id"
            ddlDepartment.DataBind()

            ddlDepartment.Items.Insert(0, New ListItem("-- Select Department --", ""))
        End If
    End Sub

    Protected Sub BindData()
        Dim employeesList As List(Of Employe) = employeeService.GetEmployees()
        dgvEmployee.DataSource = employeesList
        dgvEmployee.DataBind()
    End Sub

    Private Sub ClearEditControls()
        'txtId.Text = Convert.ToString(employeeService.GetMaxId())
        txtName.Text = ""
        txtSalary.Text = ""
        ddlDepartment.SelectedIndex = 0
        ddlDepartment.Items.Insert(0, New ListItem("-- Select Department --", ""))
    End Sub
    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim empId As String = txtId.Text.Trim()
        Dim empName As String = txtName.Text.Trim()
        Dim empSalary As Double = CDbl(txtSalary.Text)
        Dim empDepartment As Integer = CInt(ddlDepartment.SelectedValue)

        employeeService.AddEmployee(empId, empName, empSalary, empDepartment)
        ClearEditControls()
        BindData()
        txtId.Text = Convert.ToString(employeeService.GetMaxId())
    End Sub

    Protected Sub dgvDepartment_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgvEmployee.RowCommand
        If e.CommandName = "DeleteEmployee" Then
            Dim employeeId As Integer = Convert.ToInt32(e.CommandArgument)
            employeeService.DeleteEmployee(employeeId)
            BindData()
        End If
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Dim empId As String = txtId.Text.Trim()
        Dim empName As String = txtName.Text.Trim()
        Dim empSalary As Double = CDbl(txtSalary.Text)
        Dim empDepartment As Integer = CInt(ddlDepartment.SelectedValue)

        employeeService.UpdateEmployee(empId, empName, empSalary, empDepartment)
        ClearEditControls()
        BindData()
        txtId.Text = Convert.ToString(employeeService.GetMaxId())
    End Sub
End Class