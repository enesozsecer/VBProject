Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class RoleController
    Inherits Controller
    Private ReadOnly _roleService As IRoleService
    Private ReadOnly _mapper As IMapper
    Public Sub New(mapper As IMapper, roleService As IRoleService)
        _mapper = mapper
        _roleService = roleService
    End Sub
    <HttpGet("/GetRoles")>
    Public Async Function GetRoles() As Task(Of IActionResult)
        Dim roles = Await _roleService.GetAllAsync(Function(x) True)

        Dim roleDTOResponseList As New List(Of RoleDTOResponse)()
        For Each role In roles
            roleDTOResponseList.Add(_mapper.Map(Of RoleDTOResponse)(role))
        Next

        Return Ok(roleDTOResponseList)
    End Function
End Class

