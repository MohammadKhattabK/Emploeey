Imports System.Data.SqlClient

Public Class Connection
    Private Const ConnectionString As String = "Data Source=\;Initial Catalog=DemoEmpDB;Integrated Security=True; Timeout=30"

    Private connection As SqlConnection

    Public Sub New()
        connection = New SqlConnection(ConnectionString)
    End Sub

    Public Sub Open()
        Try
            If connection.State = ConnectionState.Closed Then
                connection.Open()
            End If
        Catch ex As Exception
            Console.WriteLine($"Error opening connection: {ex.Message}")
        End Try
    End Sub

    Public Sub Close()
        Try
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
        Catch ex As Exception
            Console.WriteLine($"Error closing connection: {ex.Message}")
        End Try
    End Sub

    Public Function GetConnection() As SqlConnection
        Return connection
    End Function

    Public Function ExecuteQuery(query As String) As DataTable
        Dim dataTable As New DataTable()

        Try
            Open()

            Using command As New SqlCommand(query, connection)
                Using adapter As New SqlDataAdapter(command)
                    adapter.Fill(dataTable)
                End Using
            End Using
        Catch ex As Exception
            Console.WriteLine($"Error executing query: {ex.Message}")
        Finally
            Close()
        End Try

        Return dataTable
    End Function
End Class
