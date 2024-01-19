Imports System.ServiceModel.Channels
Imports AutoMapper
Imports Microsoft.AspNetCore.Authorization
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class DepartmentController
    Inherits Controller
    Private ReadOnly _departmentService As IDepartmentService
    Private ReadOnly _mapper As IMapper
    Public Sub New(mapper As IMapper, departmentService As IDepartmentService)
        _mapper = mapper
        _departmentService = departmentService
    End Sub

    <HttpPost("/AddDepartment")>
    Public Async Function AddDepartment(departmentDTORequest As DepartmentDTORequest) As Task(Of IActionResult)
        Dim department As Department = _mapper.Map(Of Department)(departmentDTORequest)
        Await _departmentService.AddAsync(department)

        Dim departmentDTOResponse As DepartmentDTOResponse = _mapper.Map(Of DepartmentDTOResponse)(department)

        'Log.Information("Departments => {@departmentDTOResponse} => { Departman Eklendi. }", departmentDTOResponse)

        Return Ok(Sonuc(Of DepartmentDTOResponse).SuccessWithData(departmentDTOResponse))
    End Function

    <HttpDelete("/RemoveDepartment/{departmentId}")>
    Public Async Function RemoveDepartment(departmentId As Integer) As Task(Of IActionResult)
        Dim department As Department = Await _departmentService.GetAsync(Function(x) x.Id = departmentId)

        If department Is Nothing Then
            Return NotFound(Sonuc(Of DepartmentDTOResponse).SuccessNoDataFound())
        End If

        Await _departmentService.RemoveAsync(department)

        'Log.Information("Departments => {@department} => { Departman Silindi. }", department)

        Return Ok(Sonuc(Of DepartmentDTOResponse).SuccessWithoutData())
    End Function

    <HttpPost("/UpdateDepartment")>
    Public Async Function UpdateDepartment(departmentDTORequest As DepartmentDTORequest) As Task(Of IActionResult)
        Dim department As Department = Await _departmentService.GetAsync(Function(x) x.Id = departmentDTORequest.Id)

        If department Is Nothing Then
            Return NotFound(Sonuc(Of DepartmentDTOResponse).SuccessNoDataFound())
        End If

        _mapper.Map(departmentDTORequest, department)

        Dim existingDepartment = Await _departmentService.GetAsync(Function(x) x.Name = department.Name)

        If existingDepartment IsNot Nothing Then
            Return BadRequest(Sonuc(Of UserDTOResponse).ExistingError("Bu departman zaten var"))
        End If

        Await _departmentService.UpdateAsync(department)

        Dim departmentDTOResponse As DepartmentDTOResponse = _mapper.Map(Of DepartmentDTOResponse)(department)

        'Log.Information("Departments => {@departmentDTOResponse} => { Departman Güncellendi. }", departmentDTOResponse)

        Return Ok(Sonuc(Of DepartmentDTOResponse).SuccessWithData(departmentDTOResponse))
    End Function

    <HttpGet("/GetDepartment/{departmentId}")>
    Public Async Function GetDepartment(departmentId As Integer) As Task(Of IActionResult)
        Dim department As Department = Await _departmentService.GetAsync(Function(x) x.Id = departmentId, "Company")

        If department Is Nothing Then
            Return NotFound(Sonuc(Of DepartmentDTOResponse).SuccessNoDataFound())
        End If

        Dim departmentDTOResponse As DepartmentDTOResponse = _mapper.Map(Of DepartmentDTOResponse)(department)

        'Log.Information("Departments => {@departmentDTOResponse} => { Department Getirildi. }", departmentDTOResponse)

        Return Ok(Sonuc(Of DepartmentDTOResponse).SuccessWithData(departmentDTOResponse))
    End Function

    <HttpGet("/GetDepartments")>
    Public Async Function GetDepartments() As Task(Of IActionResult)
        Dim departments = Await _departmentService.GetAllAsync(Function(x) x.IsActive = True, {"Company"})

        If departments Is Nothing Then
            Return NotFound(Sonuc(Of DepartmentDTOResponse).SuccessNoDataFound())
        End If

        Dim departmentDTOResponseList As New List(Of DepartmentDTOResponse)()

        For Each department In departments
            departmentDTOResponseList.Add(_mapper.Map(Of DepartmentDTOResponse)(department))
        Next

        'Log.Information("Departments => {@departmentDTOResponse} => { Departmanlar Getirildi. }", departmentDTOResponseList)
        Return Ok(departmentDTOResponseList)
    End Function

    <HttpGet("/GetDepartmentsByCompany/{companyId}")>
    Public Async Function GetDepartmentsByCompany(companyId As Integer) As Task(Of IActionResult)
        Dim departments = Await _departmentService.GetAllAsync(Function(x) x.IsActive = True AndAlso x.CompanyId = companyId, {"Company"})

        If departments Is Nothing Then
            Return NotFound(Sonuc(Of DepartmentDTOResponse).SuccessNoDataFound())
        End If

        Dim departmentDTOResponseList As New List(Of DepartmentDTOResponse)()

        For Each department In departments
            departmentDTOResponseList.Add(_mapper.Map(Of DepartmentDTOResponse)(department))
        Next

        'Log.Information("Departments => {@departmentDTOResponse} => { Şirketlere Göre Departmanlar Getirildi. }", departmentDTOResponseList)

        Return Ok(departmentDTOResponseList)
    End Function
End Class