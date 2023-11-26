Public Interface IServicesDepartment
    Sub AddDepartment(name As String)
    Sub UpdateDepartment(id As Integer, newName As String)
    Sub DeleteDepartment(id As Integer)
    Function GetDepartments() As List(Of Department)
End Interface
