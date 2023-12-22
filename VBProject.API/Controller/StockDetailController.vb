Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class StockDetailController
    Inherits Controller
    Private ReadOnly _stockDetailService As IStockDetailService
    Private ReadOnly _stockService As IStockService
    Private ReadOnly _mapper As IMapper

    Public Sub New(stockDetailService As IStockDetailService, stockService As IStockService, mapper As IMapper)
        _stockDetailService = stockDetailService
        _stockService = stockService
        _mapper = mapper
    End Sub

    <HttpPost("/AddStockDetail")>
    Public Async Function AddStockDetail(stockDetailDTORequest As StockDetailDTORequest) As Task(Of IActionResult)
        Dim stock = Await _stockService.GetAsync(Function(x) x.Id = stockDetailDTORequest.StockId)

        If (stock.Quantity + stockDetailDTORequest.Quantity) >= 0 Then
            Dim stockDetail As StockDetail = _mapper.Map(Of StockDetail)(stockDetailDTORequest)
            Await _stockDetailService.AddAsync(stockDetail)
            stock.Quantity += stockDetailDTORequest.Quantity
            Await _stockService.UpdateAsync(stock)
            Dim stockDetailDTOResponse As StockDetailDTOResponse = _mapper.Map(Of StockDetailDTOResponse)(stockDetail)
            'Log.Information("StockDetails => {@stockDetailDTOResponse} => { Stok Detayı Eklendi }", stockDetailDTOResponse)
            Return Ok(Sonuc(Of StockDetailDTOResponse).SuccessWithData(stockDetailDTOResponse))
        Else
            Return BadRequest(Sonuc(Of StockDetailDTOResponse).SuccessWithoutData())
        End If
    End Function

    <HttpDelete("/RemoveStockDetail/{stockDetailId}")>
    Public Async Function RemoveStockDetail(stockDetailId As Long) As Task(Of IActionResult)
        Dim stockDetail = Await _stockDetailService.GetAsync(Function(x) x.Id = stockDetailId)
        If stockDetail Is Nothing Then
            Return NotFound(Sonuc(Of StockDetailDTOResponse).SuccessNoDataFound())
        End If

        Dim stockDetailDTOResponse As StockDetailDTOResponse = _mapper.Map(Of StockDetailDTOResponse)(stockDetail)

        'Log.Information("StockDetails => {@stockDetailDTOResponse} => { Stok Detayı Silindi. }", stockDetailDTOResponse)

        Return Ok(Sonuc(Of StockDetailDTOResponse).SuccessWithData(stockDetailDTOResponse))
    End Function

    <HttpPost("/UpdateStockDetail")>
    Public Async Function UpdateStockDetail(stockDetailDTORequest As StockDetailDTORequest) As Task(Of IActionResult)
        Dim stockDetail = Await _stockDetailService.GetAsync(Function(x) x.Id = stockDetailDTORequest.Id)
        If stockDetail Is Nothing Then
            Return NotFound(Sonuc(Of StockDetailDTOResponse).SuccessNoDataFound())
        End If

        stockDetail = _mapper.Map(stockDetailDTORequest, stockDetail)
        Await _stockDetailService.UpdateAsync(stockDetail)

        Dim stockDetailDTOResponse As StockDetailDTOResponse = _mapper.Map(Of StockDetailDTOResponse)(stockDetail)

        'Log.Information("StockDetails => {@stockDetailDTOResponse} => { Stok Detayı Güncellendi. }", stockDetailDTOResponse)

        Return Ok(Sonuc(Of StockDetailDTOResponse).SuccessWithData(stockDetailDTOResponse))
    End Function

    <HttpGet("/StockDetail/{stockDetailId}")>
    Public Async Function GetStockDetail(stockDetailId As Long) As Task(Of IActionResult)
        Dim stockDetail = Await _stockDetailService.GetAsync(Function(x) x.Id = stockDetailId, "Stock.Product", "User")
        If stockDetail Is Nothing Then
            Return NotFound(Sonuc(Of StockDetailDTOResponse).SuccessNoDataFound())
        End If

        Dim stockDetailDTOResponse As StockDetailDTOResponse = _mapper.Map(Of StockDetailDTOResponse)(stockDetail)

        'Log.Information("StockDetails => {@stockDetailDTOResponse} => { Stok Detayı Getirildi. }", stockDetailDTOResponse)

        Return Ok(Sonuc(Of StockDetailDTOResponse).SuccessWithData(stockDetailDTOResponse))
    End Function

    <HttpGet("/StockDetails")>
    Public Async Function GetStockDetails() As Task(Of IActionResult)
        Dim stockDetails = Await _stockDetailService.GetAllAsync(Function(x) x.IsActive = True, {"Stock.Product", "User"})
        If stockDetails Is Nothing Then
            Return NotFound(Sonuc(Of StockDetailDTOResponse).SuccessNoDataFound())
        End If

        Dim stockDetailDTOResponseList As New List(Of StockDetailDTOResponse)()
        For Each stockdetail In stockDetails
            stockDetailDTOResponseList.Add(_mapper.Map(Of StockDetailDTOResponse)(stockdetail))
        Next

        'Log.Information("StockDetails => {@stockDetailDTOResponse} => { Stok Detayları Getirildi. }", stockDetailDTOResponseList)

        Return Ok(Sonuc(Of List(Of StockDetailDTOResponse)).SuccessWithData(stockDetailDTOResponseList))
    End Function

    <HttpGet("/StockDetailsByStock/{stockId}")>
    Public Async Function GetStockDetailsByStock(stockId As Long) As Task(Of IActionResult)
        Dim stockDetails = Await _stockDetailService.GetAllAsync(Function(x) x.IsActive = True AndAlso x.StockId = stockId, {"Stock.Product", "User"})
        If stockDetails Is Nothing Then
            Return NotFound(Sonuc(Of StockDetailDTOResponse).SuccessNoDataFound())
        End If

        Dim stockDetailDTOResponseList As New List(Of StockDetailDTOResponse)()
        For Each stockdetail In stockDetails
            stockDetailDTOResponseList.Add(_mapper.Map(Of StockDetailDTOResponse)(stockdetail))
        Next

        'Log.Information("StockDetails => {@stockDetailDTOResponse} => { Stoğa Göre Stok Detayları Getirildi. }", stockDetailDTOResponseList)

        Return Ok(Sonuc(Of List(Of StockDetailDTOResponse)).SuccessWithData(stockDetailDTOResponseList))
    End Function

    <HttpGet("/StockDetailsByUser/{userId}")>
    Public Async Function GetStockDetailsByUser(userId As Long) As Task(Of IActionResult)
        Dim stockDetails = Await _stockDetailService.GetAllAsync(Function(x) x.IsActive = True AndAlso x.RecieverId = userId, {"Stock.Product", "User"})
        If stockDetails Is Nothing Then
            Return NotFound(Sonuc(Of StockDetailDTOResponse).SuccessNoDataFound())
        End If

        Dim stockDetailDTOResponseList As New List(Of StockDetailDTOResponse)()
        For Each stockdetail In stockDetails
            stockDetailDTOResponseList.Add(_mapper.Map(Of StockDetailDTOResponse)(stockdetail))
        Next

        'Log.Information("StockDetails => {@stockDetailDTOResponse} => { Stoğa Göre Stok Detayları Getirildi. }", stockDetailDTOResponseList)

        Return Ok(Sonuc(Of List(Of StockDetailDTOResponse)).SuccessWithData(stockDetailDTOResponseList))
    End Function

End Class