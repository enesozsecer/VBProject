Imports System.Net.Mail
Imports System.ServiceModel.Channels
Imports AutoMapper
Imports Microsoft.AspNetCore.Authorization
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class OfferController
    Inherits Controller

    Private ReadOnly _offerService As IOfferService
    Private ReadOnly _mapper As IMapper
    Private ReadOnly _requestService As IRequestService
    Public Sub New(offerService As IOfferService, mapper As IMapper, requestService As IRequestService)
        _offerService = offerService
        _mapper = mapper
        _requestService = requestService
    End Sub
    <HttpPost("/AddOffer")>
    Public Async Function AddOffer(offerDTORequest As OfferDTORequest) As Task(Of IActionResult)
        Dim offer As Offer = _mapper.Map(Of Offer)(offerDTORequest)
        Await _offerService.AddAsync(offer)

        Dim offerDTOResponse As OfferDTOResponse = _mapper.Map(Of OfferDTOResponse)(offer)

        'Log.Information("Offers => {@offerDTOResponse} => { Teklif Eklendi. }", offerDTOResponse)

        Return Ok(Sonuc(Of OfferDTOResponse).SuccessWithData(offerDTOResponse))
    End Function

    <HttpDelete("/RemoveOffer/{offerId}")>
    Public Async Function RemoveOffer(offerId As Integer) As Task(Of IActionResult)
        Dim offer As Offer = Await _offerService.GetAsync(Function(x) x.Id = offerId)
        If offer Is Nothing Then
            Return NotFound(Sonuc(Of OfferDTOResponse).SuccessNoDataFound())
        End If

        Await _offerService.RemoveAsync(offer)

        'Log.Information("Offers => {@offer} => { Teklif Silindi. }", offer)

        Return Ok(Sonuc(Of OfferDTOResponse).SuccessWithoutData())
    End Function

    <HttpPost("/UpdateOffer")>
    Public Async Function UpdateOffer(offerDTORequest As OfferDTORequest) As Task(Of IActionResult)
        Dim offer As Offer = Await _offerService.GetAsync(Function(x) x.Id = offerDTORequest.Id)
        If offer Is Nothing Then
            Return NotFound(Sonuc(Of OfferDTOResponse).SuccessNoDataFound())
        End If
        offer = _mapper.Map(offerDTORequest, offer)
        Await _offerService.UpdateAsync(offer)

        Dim offerDTOResponse As OfferDTOResponse = _mapper.Map(Of OfferDTOResponse)(offer)

        'Log.Information("Offers => {@offerDTOResponse} => { Teklif Güncellendi. }", offerDTOResponse)

        Return Ok(Sonuc(Of OfferDTOResponse).SuccessWithData(offerDTOResponse))
    End Function

    <HttpGet("/GetOffer/{offerId}")>
    Public Async Function GetOffer(offerId As Integer) As Task(Of IActionResult)
        Dim offer As Offer = Await _offerService.GetAsync(Function(x) x.Id = offerId, "User")
        If offer Is Nothing Then
            Return NotFound(Sonuc(Of OfferDTOResponse).SuccessNoDataFound())
        End If

        Dim offerDTOResponse As OfferDTOResponse = _mapper.Map(Of OfferDTOResponse)(offer)

        'Log.Information("Offers => {@offerDTOResponse} => { Teklif Getirildi. }", offerDTOResponse)

        Return Ok(Sonuc(Of OfferDTOResponse).SuccessWithData(offerDTOResponse))
    End Function

    <HttpGet("/GetOffers")>
    Public Async Function GetOffers() As Task(Of IActionResult)
        Dim offers = Await _offerService.GetAllAsync(Function(x) x.IsActive = True, {"User"})
        If offers Is Nothing Then
            Return NotFound(Sonuc(Of OfferDTOResponse).SuccessNoDataFound())
        End If
        Dim offerDTOResponseList As New List(Of OfferDTOResponse)()
        For Each offer In offers
            offerDTOResponseList.Add(_mapper.Map(Of OfferDTOResponse)(offer))
        Next

        'Log.Information("Offers => {@offerDTOResponse} => { Teklifleri Getir. }", offerDTOResponseList)
        Return Ok(Sonuc(Of List(Of OfferDTOResponse)).SuccessWithData(offerDTOResponseList))
    End Function

    <HttpPost("/UpdateAllOffer")>
    Public Async Function UpdateAll(offerDTORequest As OfferDTORequest) As Task(Of IActionResult)
        Dim request = Await _requestService.GetAsync(Function(e) e.Id = offerDTORequest.RequestId, "User", "Product")
        request.RequestStatus = 2
        Await _requestService.UpdateAsync(request)
        Dim offer = _mapper.Map(Of Offer)(offerDTORequest)
        Dim response = Await _offerService.UpdateAllAsync(offer)
        Dim offerDTOResponse1 As OfferDTOResponse = _mapper.Map(Of OfferDTOResponse)(offer)

        Dim AcceptRequestMessage As String = $"{request.User.Name} {request.User.LastName} adlı personelimiz {request.Title} başlıklı isteğiniz tamamlanmıştır."
        Dim RefuseRequestMessage As String = $"{request.User.Name} {request.User.LastName} adlı personelimiz {request.Title} başlıklı isteğiniz reddedildi"
        If request.RequestStatus = 2 Then
            SendMail(request.User.Email, AcceptRequestMessage)
        End If
        If request.RequestStatus = 3 Then
            SendMail(request.User.Email, RefuseRequestMessage)
        End If

        'Log.Information("Offers => {@offerDTOResponse} => { Teklifler Toplu Güncellendi }", offerDTOResponse1)
        Return Ok(Sonuc(Of OfferDTOResponse).SuccessWithData(offerDTOResponse1))
    End Function

    <HttpGet("/GetOffersByRequest/{requestId}")>
    Public Async Function GetOffersByRequest(requestId As Long) As Task(Of IActionResult)
        Dim offers = Await _offerService.GetAllAsync(Function(x) x.IsActive = True AndAlso x.RequestId = requestId, {"User", "Request"})
        If offers Is Nothing Then
            Return NotFound(Sonuc(Of OfferDTOResponse).SuccessNoDataFound())
        End If
        Dim offerDTOResponseList As New List(Of OfferDTOResponse)()
        For Each offer In offers
            offerDTOResponseList.Add(_mapper.Map(Of OfferDTOResponse)(offer))
        Next

        'Log.Information("Offers => {@offerDTOResponse} => { Teklifleri İsteklere Göre Getir. }", offerDTOResponseList)
        Return Ok(Sonuc(Of List(Of OfferDTOResponse)).SuccessWithData(offerDTOResponseList))
    End Function

    <HttpGet("/GetOfferByjs/{requestId}")>
    Public Async Function GetOfferByjs(requestId As Long) As Task(Of IActionResult)
        Dim offerlist = Await _offerService.GetAllAsync(Function(x) x.RequestId = requestId AndAlso x.IsActive = True, {"User", "Request"})
        If offerlist Is Nothing Then
            Return NotFound(Sonuc(Of OfferDTOResponse).SuccessNoDataFound())
        End If

        Dim offerDTOResponseList As New List(Of OfferDTOResponse)()

        For Each offer In offerlist
            offerDTOResponseList.Add(_mapper.Map(Of OfferDTOResponse)(offer))
        Next

        'Log.Information("Offers => {@offerDTOResponse} => { Teklif Getirildi. }", offerDTOResponseList)

        Return Ok(offerDTOResponseList)
    End Function

    Private Sub SendMail(mail As String, body As String)
        Dim mesaj As New MailMessage()
        mesaj.From = New MailAddress("teklifbilgilendirme@hotmail.com")
        mesaj.To.Add(mail)
        mesaj.Subject = "İstek Sonuçlandı"
        mesaj.Body = body

        Dim a As New SmtpClient()
        a.Credentials = New System.Net.NetworkCredential("teklifbilgilendirme@hotmail.com", "Hakanceylan123")
        a.Port = 587
        a.Host = "smtp.office365.com"
        a.EnableSsl = True
        Dim userState As Object = mesaj

        a.Send(mesaj)
    End Sub


End Class