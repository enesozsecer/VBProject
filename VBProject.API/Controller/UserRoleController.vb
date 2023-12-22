Imports System.ServiceModel.Channels
Imports AutoMapper
Imports Microsoft.AspNetCore.Authorization
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class UserRoleController
    Inherits Controller
    Private ReadOnly _userService As IUserService
    Private ReadOnly _userRoleService As IUserRoleService
    Private ReadOnly _mapper As IMapper
    Public Sub New(userService As IUserService, userRoleService As IUserRoleService, mapper As IMapper)
        _userService = userService
        _userRoleService = userRoleService
        _mapper = mapper
    End Sub
    <HttpPost("/AddUserRole")>
    Public Async Function AddUserRole(userRoleDTORequest As UserRoleDTORequest) As Task(Of IActionResult)
        Dim user As User = Await _userService.GetAsync(Function(x) x.Id = userRoleDTORequest.UserId)
        Dim userRoles = Await _userRoleService.GetAsync(Function(x) x.UserId = user.Id AndAlso x.RoleId = userRoleDTORequest.RoleId)
        If userRoles IsNot Nothing Then
            Return BadRequest(Sonuc(Of UserRoleDTOResponse).AlreadyExistError())
        End If

        Dim userRole = _mapper.Map(Of UserRole)(userRoleDTORequest)
        Await _userRoleService.AddAsync(userRole)

        Dim userRoleDTOResponse As UserRoleDTOResponse = _mapper.Map(Of UserRoleDTOResponse)(userRole)

        Return Ok(Sonuc(Of UserRoleDTOResponse).SuccessWithData(userRoleDTOResponse))
    End Function

    <HttpPost("/RemoveUserRole")>
    Public Async Function DeleteUserRole(userRoleDTORequest As UserRoleDTORequest) As Task(Of IActionResult)
        Dim userRole = Await _userRoleService.GetAsync(Function(e) e.UserId = userRoleDTORequest.UserId AndAlso e.RoleId = userRoleDTORequest.RoleId)

        Await _userRoleService.RemoveAsync(userRole)

        'Log.Information("UserRole => {@userRole} => { Kullanıcıya Ait Rol Silindi. }", userRole)

        Return Ok(Sonuc(Of UserRoleDTOResponse).SuccessWithoutData())
    End Function

    <HttpGet("/GetUserRole/{userId}")>
    Public Async Function GetUserRoleByUserId(userRoleDTORequest As UserRoleDTORequest) As Task(Of IActionResult)
        Dim userRoles = Await _userRoleService.GetAllAsync(Function(e) e.UserId = userRoleDTORequest.UserId)

        Dim userRoleDTOResponses As New List(Of UserRoleDTOResponse)()

        For Each item In userRoles
            userRoleDTOResponses.Add(_mapper.Map(Of UserRoleDTOResponse)(item))
        Next

        Return Ok(Sonuc(Of List(Of UserRoleDTOResponse)).SuccessWithData(userRoleDTOResponses))
    End Function

    <HttpGet("/GetUserRoles")>
    Public Async Function GetUserRoles() As Task(Of IActionResult)
        Dim userRoles = Await _userRoleService.GetAllAsync()

        Dim userRoleDTOResponses As New List(Of UserRoleDTOResponse)()

        For Each item In userRoles
            userRoleDTOResponses.Add(_mapper.Map(Of UserRoleDTOResponse)(item))
        Next

        Return Ok(Sonuc(Of List(Of UserRoleDTOResponse)).SuccessWithData(userRoleDTOResponses))
    End Function

    <HttpGet("/GetUserRolesJs/{id}")>
    Public Async Function GetUserRolesJs(id As Integer) As Task(Of IActionResult)
        Dim userRoles = Await _userRoleService.GetAllAsync(Function(x) x.UserId = id, {"Role"})

        Dim userRoleDTOResponses As New List(Of UserRoleDTOResponse)()

        For Each item In userRoles
            userRoleDTOResponses.Add(_mapper.Map(Of UserRoleDTOResponse)(item))
        Next

        Return Ok(userRoleDTOResponses)
    End Function

End Class