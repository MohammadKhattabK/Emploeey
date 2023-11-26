<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Employee.aspx.vb" Inherits="Emploeey.Web.Employee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <title></title>
</head>
<body>

    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <a class="navbar-brand" href="Default.aspx">Home</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav mr-auto">
                <li class="nav-item ">
                    <a class="nav-link" href="Default.aspx">Department</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="Employee.aspx">Employee</a>
                </li>

            </ul>
        </div>
    </nav>

    <div class="container mt-3">


        <form id="form1" runat="server">
            <div class="row">
                <div class="col-6">
                    <div class="form-group">
                        <label>Employee ID</label>
                        <asp:TextBox ID="txtId" CssClass="form-control" placeholder="Employee ID" runat="server" ReadOnly="False"></asp:TextBox>
                        <label>Employee Name</label>
                        <asp:TextBox ID="txtName" CssClass="form-control" placeholder="Employee Name" runat="server"></asp:TextBox>
                        <label>Salary </label>
                        <asp:TextBox ID="txtSalary" CssClass="form-control" placeholder="Salary" runat="server"></asp:TextBox>
                        <label>Department</label>
                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>


            <asp:Button CssClass="btn btn-primary mt-3" ID="btnSave" runat="server" Text="Add" />


            <asp:Button CssClass="btn btn-primary mt-3" ID="btnUpdate" runat="server" Text="Update" />

            <asp:GridView ID="dgvEmployee" runat="server" CssClass="table mt-3" AutoGenerateColumns="False" EnableModelValidation="True">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Salary" HeaderText="Salary" SortExpression="Salary" />
                    <asp:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="DepartmentName" />
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <a class="btn btn-primary" href="javascript:void(0);" onclick='EditRecord(<%# Eval("Id") %>, "<%# Eval("Name") %>")'>Edit</a>
                            <asp:LinkButton runat="server" CommandName="DeleteEmployee" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-danger">Delete</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </form>





    </div>




    <script src="Scripts/bootstrap.min.js"></script>

    <script>
        function EditRecord(id, name,salary,departmentId) {

            document.getElementById('<%= txtId.ClientID %>').value = id;
            document.getElementById('txtName').value = name;
            document.getElementById('txtSalary').value = salary;
            document.getElementById('ddlDepartment').value = departmentId;

        }

    </script>

</body>
</html>
