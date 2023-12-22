Imports System.Net.Mail
Imports System.ServiceModel.Channels
Imports System.Text
Imports AutoMapper
Imports Microsoft.AspNetCore.Authorization
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class RequestController
    Inherits Controller
    Private ReadOnly _mapper As IMapper
    Private ReadOnly _requestService As IRequestService
    Private ReadOnly _userService As IUserService
    Private ReadOnly _companyService As ICompanyService
    Private ReadOnly _departmentService As IDepartmentService
    Public Sub New(mapper As IMapper, requestService As IRequestService, userService As IUserService, companyService As ICompanyService, departmentService As IDepartmentService)
        _mapper = mapper
        _requestService = requestService
        _userService = userService
        _companyService = companyService
        _departmentService = departmentService
    End Sub

    <HttpGet("/Requests")>
    Public Async Function GetRequests() As Task(Of IActionResult)
        Dim requests = Await _requestService.GetAllAsync(Function(x) x.IsActive = True, {"User", "Product", "AcceptedUser"})

        If requests Is Nothing Then
            Return NotFound(Sonuc(Of List(Of RequestDTOResponse)).SuccessNoDataFound())
        End If

        Dim requestDTOResponseList As New List(Of RequestDTOResponse)()

        For Each requestval In requests
            requestDTOResponseList.Add(_mapper.Map(Of RequestDTOResponse)(requestval))
        Next

        'Log.Information("Requests => {@requestDTOResponse} => { İstekler Getirildi. }", requestDTOResponseList)

        Return Ok(Sonuc(Of List(Of RequestDTOResponse)).SuccessWithData(requestDTOResponseList))
    End Function

    <HttpPost("/UpdateRequest")>
    Public Async Function UpdateRequest(requestDTORequest As RequestDTORequest) As Task(Of IActionResult)
        Dim request = Await _requestService.GetAsync(Function(e) e.Id = requestDTORequest.Id, "User", "Product")
        If request Is Nothing Then
            Return NotFound(Sonuc(Of RequestDTOResponse).SuccessNoDataFound())
        End If

        _mapper.Map(requestDTORequest, request)
        Await _requestService.UpdateAsync(request)

        Dim acceptRequestMessage = $"{request.User.Name} {request.User.LastName} adlı personelimiz {request.Title} başlıklı isteğiniz tamamlanmıştır."
        Dim refuseRequestMessage = $"{request.User.Name} {request.User.LastName} adlı personelimiz {request.Title} başlıklı isteğiniz reddedilmiştir."

        If request.RequestStatus = 2 Then
            SendMail(request.User.Email, acceptRequestMessage)
        End If

        If request.RequestStatus = 3 Then
            SendMail(request.User.Email, refuseRequestMessage)
        End If

        Dim requestDTOResponse = _mapper.Map(Of RequestDTOResponse)(request)

        'Log.Information("Requests => {@requestDTOResponse} => { İstek Güncellendi. }", requestDTOResponse)

        Return Ok(Sonuc(Of RequestDTOResponse).SuccessWithData(requestDTOResponse))
    End Function

    <HttpDelete("/RemoveRequest/{requestId}")>
    Public Async Function RemoveRequest(requestId As Integer) As Task(Of IActionResult)
        Dim request = Await _requestService.GetAsync(Function(e) e.Id = requestId)
        If request Is Nothing Then
            Return NotFound(Sonuc(Of RequestDTOResponse).SuccessNoDataFound())
        End If

        Await _requestService.RemoveAsync(request)

        'Log.Information("Requests => {@request} => { İstek Silindi. }", request)

        Return Ok(Sonuc(Of RequestDTOResponse).SuccessWithoutData())
    End Function

    <HttpPost("/AddRequest")>
    Public Async Function AddRequest(requestDTORequest As RequestDTORequest) As Task(Of IActionResult)
        Dim request = _mapper.Map(Of Request)(requestDTORequest)

        Await _requestService.AddAsync(request)
        Dim requestDTOResponse = _mapper.Map(Of RequestDTOResponse)(request)

        'Log.Information("Requests => {@requestDTOResponse} => { İstek Eklendi. }", requestDTOResponse)
        Return Ok(Sonuc(Of RequestDTOResponse).SuccessWithData(requestDTOResponse))
    End Function

    <HttpGet("/Requests/{userId}")>
    Public Async Function GetRequests(userId As Long) As Task(Of IActionResult)
        Dim requests = Await _requestService.GetAllAsync(Function(e) e.UserId = userId AndAlso e.IsActive = True, {"User", "Product"})
        If requests Is Nothing Then
            Return NotFound(Sonuc(Of List(Of RequestDTOResponse)).SuccessNoDataFound())
        End If

        Dim requestDTOResponseList As New List(Of RequestDTOResponse)()

        For Each requestval In requests
            requestDTOResponseList.Add(_mapper.Map(Of RequestDTOResponse)(requestval))
        Next

        'Log.Information("Requests => {@requestDTOResponse} => { Kullanıcıya Göre İstek Getirildi. }", requestDTOResponseList)
        Return Ok(Sonuc(Of List(Of RequestDTOResponse)).SuccessWithData(requestDTOResponseList))
    End Function

    <HttpGet("/Request/{requestId}")>
    Public Async Function GetRequest(requestId As Integer) As Task(Of IActionResult)
        Dim request = Await _requestService.GetAsync(Function(e) e.Id = requestId)
        If request Is Nothing Then
            Return NotFound(Sonuc(Of RequestDTOResponse).SuccessNoDataFound())
        End If

        Dim requestDTOResponse = _mapper.Map(Of RequestDTOResponse)(request)

        'Log.Information("Requests => {@requestDTOResponse} => { İstek Getirildi. } ", requestDTOResponse)

        Return Ok(Sonuc(Of RequestDTOResponse).SuccessWithData(requestDTOResponse))
    End Function

    <HttpGet("/RequestsByCompany/{userId}")>
    Public Async Function GetRequestsByCompany(userId As Long) As Task(Of IActionResult)
        Dim user = Await _userService.GetAsync(Function(x) x.Id = userId)
        Dim department = Await _departmentService.GetAsync(Function(x) x.Id = user.DepartmentId)
        Dim company = Await _companyService.GetAsync(Function(x) x.Id = department.CompanyId)

        Dim requests = Await _requestService.GetAllAsync(Function(x) x.IsActive = True AndAlso x.User.Department.CompanyId = company.Id, {"User", "Product"})
        If requests Is Nothing Then
            Return NotFound(Sonuc(Of List(Of RequestDTOResponse)).SuccessNoDataFound())
        End If

        Dim requestDTOResponseList As New List(Of RequestDTOResponse)()

        For Each requestval In requests
            requestDTOResponseList.Add(_mapper.Map(Of RequestDTOResponse)(requestval))
        Next

        'Log.Information("Requests => {@requestDTOResponse} => { İstekler Getirildi. }", requestDTOResponseList)

        Return Ok(Sonuc(Of List(Of RequestDTOResponse)).SuccessWithData(requestDTOResponseList))
    End Function

    <HttpGet("/RequestsByDepartment/{userId}")>
    Public Async Function GetRequestsByDepartment(userId As Long) As Task(Of IActionResult)
        Dim user = Await _userService.GetAsync(Function(x) x.Id = userId)
        Dim department = Await _departmentService.GetAsync(Function(x) x.Id = user.DepartmentId)

        Dim requests = Await _requestService.GetAllAsync(Function(x) x.IsActive = True AndAlso x.User.DepartmentId = department.Id, {"User", "Product"})

        If requests Is Nothing Then
            Return NotFound(Sonuc(Of List(Of RequestDTOResponse)).SuccessNoDataFound())
        End If

        Dim requestDTOResponseList As New List(Of RequestDTOResponse)()

        For Each requestval In requests
            requestDTOResponseList.Add(_mapper.Map(Of RequestDTOResponse)(requestval))
        Next

        'Log.Information("Requests => {@requestDTOResponse} => { İstekler Getirildi. }", requestDTOResponseList)

        Return Ok(Sonuc(Of List(Of RequestDTOResponse)).SuccessWithData(requestDTOResponseList))
    End Function

    Private Sub SendMail(mail As String, body As String)
        Dim mesaj As New MailMessage()
        mesaj.From = New MailAddress("teklifbilgilendirme@hotmail.com")
        mesaj.To.Add(mail)
        mesaj.Subject = "İstek Sonuçlandı"
        mesaj.Body = body
        mesaj.IsBodyHtml = True
        mesaj.BodyEncoding = Encoding.UTF8
        Dim a As New SmtpClient()
        a.Credentials = New System.Net.NetworkCredential("teklifbilgilendirme@hotmail.com", "Bilal123")
        a.Port = 587
        a.Host = "smtp.office365.com"
        a.EnableSsl = True
        Dim userState As Object = mesaj
        a.Send(mesaj)
    End Sub

    <HttpGet("/RequestsByUser/{userId}")>
    Public Async Function GetRequestsByUser(userId As Long) As Task(Of IActionResult)
        Dim user = Await _userService.GetAsync(Function(x) x.Id = userId)

        Dim requests = Await _requestService.GetAllAsync(Function(x) x.IsActive = True AndAlso x.User.Id = userId, {"User", "Product"})

        If requests Is Nothing Then
            Return NotFound(Sonuc(Of List(Of RequestDTOResponse)).SuccessNoDataFound())
        End If

        Dim requestDTOResponseList As New List(Of RequestDTOResponse)()

        For Each requestval In requests
            requestDTOResponseList.Add(_mapper.Map(Of RequestDTOResponse)(requestval))
        Next

        'Log.Information("Requests => {@requestDTOResponse} => { İstekler Kullanıcıya Göre Getirildi. }", requestDTOResponseList)

        Return Ok(Sonuc(Of List(Of RequestDTOResponse)).SuccessWithData(requestDTOResponseList))
    End Function


End Class