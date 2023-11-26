Imports System.Data.SqlClient
Imports Emploeey.DataAccessLayer

Public Class ServicesEmployee
    Implements IServicesEmployee
    Private databaseConnection As New Connection()

    Public Sub AddEmployee(id As Integer, name As String, salary As Double, departmentId As Integer) Implements IServicesEmployee.AddEmployee
        Try
            Dim query As String = "INSERT INTO Employee (Id,Name,Salary,DepartmentId) VALUES (@Id,@Name,@Salary,@DepartmentId)"

            Using command As New SqlCommand(query, databaseConnection.GetConnection())

                command.Parameters.AddWithValue("@Id", id)
                command.Parameters.AddWithValue("@Name", name)
                command.Parameters.AddWithValue("@Salary", salary)
                command.Parameters.AddWithValue("@DepartmentId", departmentId)

                databaseConnection.Open()
                command.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error adding employee: {ex.Message}")
        Finally

            databaseConnection.Close()
        End Try
    End Sub

    Public Sub UpdateEmployee(id As Integer, name As String, salary As Double, departmentId As Integer) Implements IServicesEmployee.UpdateEmployee
        Try
            Using connection As SqlConnection = databaseConnection.GetConnection()
                connection.Open()

                Dim query As String = "UPDATE Employee SET Name = @Name, Salary = @Salary, DepartmentId = @DepartmentId WHERE Id = @Id"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@Id", id)
                    command.Parameters.AddWithValue("@Name", name)
                    command.Parameters.AddWithValue("@Salary", salary)
                    command.Parameters.AddWithValue("@DepartmentId", departmentId)

                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error updating employee: {ex.Message}")
        End Try
    End Sub

    Public Sub DeleteEmployee(id As Integer) Implements IServicesEmployee.DeleteEmployee
        Try
            Dim query As String = "DELETE FROM Employee WHERE Id = @Id"


            Using command As New SqlCommand(query, databaseConnection.GetConnection())
                command.Parameters.AddWithValue("@Id", id)


                databaseConnection.Open()
                command.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error deleting employee: {ex.Message}")
        Finally

            databaseConnection.Close()
        End Try
    End Sub

    Public Function GetMaxId() As Integer
        Dim maxId As Integer = 0

        Try
            Using connection As SqlConnection = databaseConnection.GetConnection()
                connection.Open()

                Dim query As String = "SELECT ISNULL(MAX(Id) + 1, 1) AS MaxId FROM [dbo].[Employee]"

                Using command As New SqlCommand(query, connection)
                    Dim result As Object = command.ExecuteScalar()

                    If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                        maxId = Convert.ToInt32(result)
                    End If
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error getting max Id: {ex.Message}")
        End Try

        Return maxId
    End Function


    Public Function GetEmployees() As List(Of Employe) Implements IServicesEmployee.GetEmployees
        Dim employees As New List(Of Employe)

        Try
            Using connection As SqlConnection = databaseConnection.GetConnection()
                connection.Open()

                Dim query As String = "SELECT E.Id, E.Name, E.Salary, E.DepartmentId, D.Name AS DepartmentName " &
                                      "FROM Employee E " &
                                      "INNER JOIN Departments D ON E.DepartmentId = D.Id"

                Using command As New SqlCommand(query, connection)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            Dim employee As New Employe With {
                                .Id = CInt(reader("Id")),
                                .Name = CStr(reader("Name")),
                                .Salary = CDbl(reader("Salary")),
                                .DepartmentId = CInt(reader("DepartmentId")),
                                .DepartmentName = CStr(reader("DepartmentName"))
                            }

                            employees.Add(employee)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error getting employees: {ex.Message}")
        End Try

        Return employees


    End Function

    Public Function GetDepartments() As List(Of Department)
        Dim departments As New List(Of Department)

        Try
            Using connection As SqlConnection = databaseConnection.GetConnection()
                connection.Open()

                Dim query As String = "SELECT Id, Name FROM Departments"
                Using command As New SqlCommand(query, connection)
                    Using reader As SqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            Dim department As New Department With {
                                .Id = CInt(reader("Id")),
                                .Name = CStr(reader("Name"))
                            }
                            departments.Add(department)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error getting departments: {ex.ToString()}")
        End Try

        Return departments
    End Function




End Class
