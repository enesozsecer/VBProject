Imports System.ServiceModel.Channels
Imports AutoMapper
Imports Microsoft.AspNetCore.Authorization
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class UserController
    Inherits Controller

    Private ReadOnly _departmentService As IDepartmentService
    Private ReadOnly _companyService As ICompanyService
    Private ReadOnly _userRoleService As IUserRoleService
    Private ReadOnly _userService As IUserService
    Private ReadOnly _mapper As IMapper
    Public Sub New(mapper As IMapper, userService As IUserService, departmentService As IDepartmentService, companyService As ICompanyService, userRoleService As IUserRoleService)
        _mapper = mapper
        _userService = userService
        _departmentService = departmentService
        _companyService = companyService
        _userRoleService = userRoleService
    End Sub

    <HttpGet("/GetUsers")>
    Public Async Function GetUsers() As Task(Of IActionResult)
        Dim users = Await _userService.GetAllAsync(Function(x) x.IsActive = True, {"Department.Company", "UserRoles.Role"})

        Dim userDTOResponseList As New List(Of UserDTOResponse)()
        For Each userval In users
            userDTOResponseList.Add(_mapper.Map(Of UserDTOResponse)(userval))
        Next

        Return Ok(userDTOResponseList)
    End Function
    <HttpGet("/GetUser/{userId}")>
    Public Async Function GetUser(userId As Integer) As Task(Of IActionResult)
        Dim user As User = Await _userService.GetAsync(Function(x) x.Id = userId, {"Department.Company", "UserRoles.Role"})

        If user Is Nothing Then
            Return NotFound()
        End If

        Dim userDTOResponse As UserDTOResponse = _mapper.Map(Of UserDTOResponse)(user)

        Return Ok(userDTOResponse)
    End Function

    <HttpPost("/AddUser")>
    Public Async Function AddUser(userDTORequest As UserDTORequest) As Task(Of IActionResult)
        Dim user As User = _mapper.Map(Of User)(userDTORequest)

        Dim existingUser = Await _userService.GetAsync(Function(x) x.Name = user.Name)

        If existingUser IsNot Nothing Then
            Return BadRequest("Bu kategori zaten var")
        End If

        Await _userService.AddAsync(user)

        Dim userDTOResponse As UserDTOResponse = _mapper.Map(Of UserDTOResponse)(user)

        Return Ok(userDTOResponse)
    End Function
    <HttpPost("/UpdateUser")>
    Public Async Function UpdateUser(userDTORequest As UserDTORequest) As Task(Of IActionResult)
        Dim user As User = Await _userService.GetAsync(Function(x) x.Id = userDTORequest.Id)

        If user Is Nothing Then
            Return NotFound()
        End If

        user = _mapper.Map(userDTORequest, user)

        Dim existingUser = Await _userService.GetAsync(Function(x) x.Name = user.Name)

        If existingUser IsNot Nothing Then
            Return BadRequest("Bu kategori zaten var")
        End If

        Await _userService.UpdateAsync(user)

        Dim userDTOResponse As UserDTOResponse = _mapper.Map(Of UserDTOResponse)(user)

        Return Ok(userDTOResponse)
    End Function

    <HttpDelete("/RemoveUser/{userId}")>
    Public Async Function RemoveUser(userId As Integer) As Task(Of IActionResult)
        Dim user As User = Await _userService.GetAsync(Function(x) x.Id = userId)

        If user Is Nothing Then
            Return NotFound()
        End If

        Await _userService.RemoveAsync(user)

        Return Ok()
    End Function
    <HttpGet("/GetUsersByDepartment/{userId}")>
    Public Async Function GetUsersByDepartment(userId As Integer) As Task(Of IActionResult)
        Dim user As User = Await _userService.GetAsync(Function(x) x.Id = userId, {"Department"})
        Dim department As Department = Await _departmentService.GetAsync(Function(x) x.Id = user.DepartmentId)

        Dim users = Await _userService.GetAllAsync(Function(x) x.IsActive = True And x.DepartmentId = department.Id, {"Department.Company", "UserRoles.Role"})
        If users Is Nothing Then
            Return NotFound(Sonuc(Of UserDTOResponse).SuccessNoDataFound())
        End If

        Dim userDTOResponseList As New List(Of UserDTOResponse)()
        For Each item In users
            userDTOResponseList.Add(_mapper.Map(Of UserDTOResponse)(item))
        Next

        'Log.Information("Users => {@userDTOResponse} => { Departmana Göre Kullanıcılar Getirildi. }", userDTOResponseList)

        Return Ok(Sonuc(Of List(Of UserDTOResponse)).SuccessWithData(userDTOResponseList))
    End Function

    <HttpGet("/GetUsersByRole/{roleId}")>
    Public Async Function GetUsersByRole(roleId As Integer) As Task(Of IActionResult)
        Dim users = Await _userService.GetAllAsync(Function(x) x.IsActive = True AndAlso x.DepartmentId = roleId, {"Department.Company", "UserRoles.Role"})
        If users Is Nothing Then
            Return NotFound(Sonuc(Of UserDTOResponse).SuccessNoDataFound())
        End If

        Dim userDTOResponseList As New List(Of UserDTOResponse)()
        For Each userval In users
            userDTOResponseList.Add(_mapper.Map(Of UserDTOResponse)(userval))
        Next

        'Log.Information("Users => {@userDTOResponse} => { Rollere Göre Kullanıcılar Getirildi. }", userDTOResponseList)

        Return Ok(Sonuc(Of List(Of UserDTOResponse)).SuccessWithData(userDTOResponseList))
    End Function

    <HttpGet("/GetUsersByCompany/{userId}")>
    Public Async Function GetUserGetUsersByCompany(userId As Integer) As Task(Of IActionResult)
        Dim user As User = Await _userService.GetAsync(Function(x) x.Id = userId)
        Dim department As Department = Await _departmentService.GetAsync(Function(x) x.Id = user.DepartmentId)
        Dim company As Company = Await _companyService.GetAsync(Function(x) x.Id = department.CompanyId)

        Dim users = Await _userService.GetAllAsync(Function(x) x.IsActive = True AndAlso x.Department.CompanyId = company.Id, {"Department.Company", "UserRoles.Role"})
        If users Is Nothing Then
            Return NotFound(Sonuc(Of UserDTOResponse).SuccessNoDataFound())
        End If

        Dim userDTOResponseList As New List(Of UserDTOResponse)()
        For Each item In users
            userDTOResponseList.Add(_mapper.Map(Of UserDTOResponse)(item))
        Next

        For Each item In userDTOResponseList
            item.CompanyName = company.Name
        Next

        'Log.Information("Users => {@userDTOResponse} => { Şirkete Göre Kullanıcılar Getirildi. }", userDTOResponseList)

        Return Ok(Sonuc(Of List(Of UserDTOResponse)).SuccessWithData(userDTOResponseList))
    End Function

    <HttpGet("/GetUserByMail/{mail}")>
    Public Async Function GetUserByMail(mail As String) As Task(Of IActionResult)
        Dim user As User = Await _userService.GetAsync(Function(x) x.Email = mail AndAlso x.IsActive = True, {"UserRoles.Role", "Department", "Department.Company"})
        If user Is Nothing Then
            Return NotFound(Sonuc(Of UserDTOResponse).SuccessNoDataFound())
        End If

        Dim userDTOResponse As UserDTOResponse = _mapper.Map(Of UserDTOResponse)(user)

        'Log.Information("Users => {@userDTOResponse} => { Kullanıcı Getirildi. }", userDTOResponse)

        Return Ok(Sonuc(Of UserDTOResponse).SuccessWithData(userDTOResponse))
    End Function

End Class

