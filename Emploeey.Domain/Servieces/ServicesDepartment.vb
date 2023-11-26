Imports System.Data.SqlClient
Imports Emploeey.DataAccessLayer

Public Class ServicesDepartment
    Implements IServicesDepartment

    Private databaseConnection As New Connection()

    Public Sub AddDepartment(name As String) Implements IServicesDepartment.AddDepartment
        Try
            Dim query As String = "INSERT INTO Departments (Name) VALUES (@Name)"

            Using command As New SqlCommand(query, databaseConnection.GetConnection())

                command.Parameters.AddWithValue("@Name", name)

                databaseConnection.Open()
                command.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error deleting department: {ex.Message}")
        Finally

            databaseConnection.Close()
        End Try
    End Sub

    Public Sub UpdateDepartment(id As Integer, newName As String) Implements IServicesDepartment.UpdateDepartment
        Try


            Dim query As String = "UPDATE Departments SET Name = @NewName WHERE Id = @Id"
            Using command As New SqlCommand(query, databaseConnection.GetConnection())


                command.Parameters.AddWithValue("@NewName", newName)
                command.Parameters.AddWithValue("@Id", id)
                databaseConnection.Open()
                command.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error deleting department: {ex.Message}")
        Finally

            databaseConnection.Close()
        End Try

    End Sub

    Public Sub DeleteDepartment(id As Integer) Implements IServicesDepartment.DeleteDepartment
        Try
            Dim query As String = "DELETE FROM Departments WHERE Id = @Id"


            Using command As New SqlCommand(query, databaseConnection.GetConnection())
                command.Parameters.AddWithValue("@Id", id)


                databaseConnection.Open()
                command.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error deleting department: {ex.Message}")
        Finally

            databaseConnection.Close()
        End Try
    End Sub

    Public Function GetDepartments() As List(Of Department) Implements IServicesDepartment.GetDepartments
        Dim departments As New List(Of Department)

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

        Return departments
    End Function

End Class
