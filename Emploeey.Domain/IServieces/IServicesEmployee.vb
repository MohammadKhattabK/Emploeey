Public Interface IServicesEmployee
    Sub AddEmployee(id As Integer, name As String, salary As Double, departmentId As Integer)
    Sub UpdateEmployee(id As Integer, name As String, salary As Double, departmentId As Integer)
    Sub DeleteEmployee(id As Integer)
    Function GetEmployees() As List(Of Employe)
End Interface
