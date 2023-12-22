Imports System.ServiceModel.Channels
Imports AutoMapper
Imports Microsoft.AspNetCore.Authorization
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class CompanyController
    Inherits Controller

    Private ReadOnly _companyService As ICompanyService
    Private ReadOnly _departmentService As IDepartmentService
    Private ReadOnly _userService As IUserService
    Private ReadOnly _mapper As IMapper

    Public Sub New(mapper As IMapper, userService As IUserService, departmentService As IDepartmentService, companyService As ICompanyService)
        _mapper = mapper
        _userService = userService
        _departmentService = departmentService
        _companyService = companyService
    End Sub


    <HttpPost("/AddCompany")>
    Public Async Function AddCompany(companyDTORequest As CompanyDTORequest) As Task(Of ActionResult)
        Dim company As Company = _mapper.Map(Of Company)(companyDTORequest)

        Dim existingCompany = Await _companyService.GetAsync(Function(x) x.Name = company.Name)

        If existingCompany IsNot Nothing Then
            Return BadRequest(Sonuc(Of CompanyDTOResponse).ExistingError("Bu şirket zaten var"))
        End If

        Await _companyService.AddAsync(company)

        Dim companyDTOResponse As CompanyDTOResponse = _mapper.Map(Of CompanyDTOResponse)(company)

        'Log.Information("Companies => {@companyDTOResponse} => { Şirket Eklendi. }", companyDTOResponse)

        Return Ok(Sonuc(Of CompanyDTOResponse).SuccessWithData(companyDTOResponse))
    End Function

    <HttpDelete("/RemoveCompany/{id}")>
    Public Async Function RemoveCompany(id As Integer) As Task(Of IActionResult)
        Dim company As Company = Await _companyService.GetAsync(Function(x) x.Id = id)

        If company Is Nothing Then
            Return NotFound(Sonuc(Of CompanyDTOResponse).SuccessNoDataFound())
        End If

        Await _companyService.RemoveAsync(company)

        'Log.Information("Companies => {@company} => { Şirket Silindi. }", company)

        Return Ok(Sonuc(Of CompanyDTOResponse).SuccessWithoutData())
    End Function

    <HttpPost("/UpdateCompany")>
    Public Async Function UpdateCompany(companyDTORequest As CompanyDTORequest) As Task(Of IActionResult)
        Dim company As Company = Await _companyService.GetAsync(Function(x) x.Id = companyDTORequest.Id)
        Dim companylist = Await _companyService.GetAllAsync()

        If company Is Nothing Then
            Return NotFound(Sonuc(Of CompanyDTORequest).SuccessNoDataFound())
        ElseIf company.Name = companyDTORequest.Name Then
            company = _mapper.Map(companyDTORequest, company)
            Await _companyService.UpdateAsync(company)

            Dim companyDTOResponse2 As CompanyDTOResponse = _mapper.Map(Of CompanyDTOResponse)(company)

            'Log.Information("Companies => {@companyDTOResponse} => { Kullanıcı Güncellendi. }", companyDTOResponse2)

            Return Ok(Sonuc(Of CompanyDTOResponse).SuccessWithData(companyDTOResponse2))
        ElseIf companylist.Any(Function(x) x.Name = companyDTORequest.Name) Then
            Return BadRequest(Sonuc(Of CompanyDTOResponse).ExistingError("Bu şirket zaten var"))
        End If

        company = _mapper.Map(companyDTORequest, company)
        Await _companyService.UpdateAsync(company)

        Dim companyDTOResponse As CompanyDTOResponse = _mapper.Map(Of CompanyDTOResponse)(company)

        'og.Information("Companies => {@companyDTOResponse} => { Şirket Güncellendi. }", companyDTOResponse)

        Return Ok(Sonuc(Of CompanyDTOResponse).SuccessWithData(companyDTOResponse))
    End Function

    <HttpGet("/GetCompany/{id}")>
    Public Async Function GetCompany(id As Integer) As Task(Of IActionResult)
        Dim company As Company = Await _companyService.GetAsync(Function(x) x.Id = id)

        If company Is Nothing Then
            Return NotFound(Sonuc(Of CompanyDTOResponse).SuccessNoDataFound())
        End If

        Dim companyDTOResponse As CompanyDTOResponse = _mapper.Map(Of CompanyDTOResponse)(company)

        'Log.Information("Companies => {@companyDTOResponse} => { Şirket Getirildi. }", companyDTOResponse)

        Return Ok(Sonuc(Of CompanyDTOResponse).SuccessWithData(companyDTOResponse))
    End Function

    <HttpGet("/GetCompanies")>
    Public Async Function GetCompanies() As Task(Of IActionResult)
        Dim companies = Await _companyService.GetAllAsync(Function(x) x.IsActive = True)

        If companies Is Nothing Then
            Return Ok(Sonuc(Of CompanyDTOResponse).SuccessWithoutData())
        End If

        Dim companyDTOResponseList As New List(Of CompanyDTOResponse)()

        For Each company In companies
            companyDTOResponseList.Add(_mapper.Map(Of CompanyDTOResponse)(company))
        Next

        'Log.Information("Companies => {@companyDTOResponse} => { Şirketler Getirildi. } ", companyDTOResponseList)

        Return Ok(Sonuc(Of List(Of CompanyDTOResponse)).SuccessWithData(companyDTOResponseList))
    End Function

    <HttpGet("/GetCompaniesByUser/{userId}")>
    Public Async Function GetCompaniesByUser(userId As Long) As Task(Of IActionResult)
        Dim user As User = Await _userService.GetAsync(Function(x) x.Id = userId)
        Dim department As Department = Await _departmentService.GetAsync(Function(x) x.Id = user.DepartmentId)
        Dim company As Company = Await _companyService.GetAsync(Function(x) x.Id = department.CompanyId)

        Dim companies = Await _companyService.GetAllAsync(Function(x) x.IsActive = True AndAlso x.Id = company.Id)

        If companies Is Nothing Then
            Return Ok(Sonuc(Of CompanyDTOResponse).SuccessWithoutData())
        End If

        Dim companyDTOResponseList As New List(Of CompanyDTOResponse)()

        For Each item In companies
            companyDTOResponseList.Add(_mapper.Map(Of CompanyDTOResponse)(item))
        Next

        'Log.Information("Companies => {@companyDTOResponse} => { Şirketler Getirildi. } ", companyDTOResponseList)

        Return Ok(Sonuc(Of List(Of CompanyDTOResponse)).SuccessWithData(companyDTOResponseList))
    End Function

End Class