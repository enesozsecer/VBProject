Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class StockController
    Inherits Controller
    Private ReadOnly _mapper As IMapper
    Private ReadOnly _stockService As IStockService

    Public Sub New(mapper As IMapper, stockService As IStockService)
        _mapper = mapper
        _stockService = stockService
    End Sub

    <HttpGet("/Stocks")>
    Public Async Function GetStocks() As Task(Of IActionResult)
        Dim stocks = Await _stockService.GetAllAsync(Function(x) x.IsActive = True, {"Product", "Company"})
        If stocks Is Nothing Then
            Return NotFound(Sonuc(Of StockDTOResponse).SuccessNoDataFound())
        End If

        Dim stockDTOResponses As New List(Of StockDTOResponse)()
        For Each stock In stocks
            stockDTOResponses.Add(_mapper.Map(Of StockDTOResponse)(stock))
        Next

        'Log.Information("Stocks => {@stockDTOResponse} => { Stoklar Getirildi. }", stockDTOResponses)
        Return Ok(Sonuc(Of List(Of StockDTOResponse)).SuccessWithData(stockDTOResponses))
    End Function

    <HttpGet("/Stock/{stockId}")>
    Public Async Function GetStock(stockId As Integer) As Task(Of IActionResult)
        Dim stock = Await _stockService.GetAsync(Function(e) e.Id = stockId AndAlso e.IsActive = True, "Product", "Company")
        If stock Is Nothing Then
            Return NotFound(Sonuc(Of StockDTOResponse).SuccessNoDataFound())
        End If

        Dim stockDTOResponse As StockDTOResponse = _mapper.Map(Of StockDTOResponse)(stock)

        'Log.Information("Stocks => {@stockDTOResponse} => { Stok Getirildi. }", stockDTOResponse)
        Return Ok(Sonuc(Of StockDTOResponse).SuccessWithData(stockDTOResponse))
    End Function

    <HttpPost("/AddStock")>
    Public Async Function AddStock(stockDTORequest As StockDTORequest) As Task(Of IActionResult)
        Dim innerstock = Await _stockService.GetAsync(Function(x) x.CompanyId = stockDTORequest.CompanyId AndAlso x.ProductId = stockDTORequest.ProductId)

        If innerstock IsNot Nothing Then
            innerstock.Quantity += stockDTORequest.Quantity
            Await _stockService.UpdateAsync(innerstock)
            Dim innerStockDTOResponse As StockDTOResponse = _mapper.Map(Of StockDTOResponse)(innerstock)
            'Log.Information("Stocks => {@stockDTOResponse} => { Stok Eklendi. }", innerStockDTOResponse)
            Return Ok(Sonuc(Of StockDTOResponse).SuccessWithData(innerStockDTOResponse))
        End If

        Dim stock = _mapper.Map(Of Stock)(stockDTORequest)
        Await _stockService.AddAsync(stock)
        Dim stockDTOResponse As StockDTOResponse = _mapper.Map(Of StockDTOResponse)(stock)

        'Log.Information("Stocks => {@stockDTOResponse} => { Stok Eklendi. }", stockDTOResponse)

        Return Ok(Sonuc(Of StockDTOResponse).SuccessWithData(stockDTOResponse))
    End Function

    <HttpPost("/UpdateStock")>
    Public Async Function UpdateStock(stockDTORequest As StockDTORequest) As Task(Of IActionResult)
        Dim stock = Await _stockService.GetAsync(Function(e) e.Id = stockDTORequest.Id)
        If stock Is Nothing Then
            Return NotFound(Sonuc(Of StockDTOResponse).SuccessNoDataFound())
        End If

        _mapper.Map(stockDTORequest, stock)
        Await _stockService.UpdateAsync(stock)

        Dim stockDTOResponse As StockDTOResponse = _mapper.Map(Of StockDTOResponse)(stock)

        'Log.Information("Stocks => {@stockDTOResponse} => { Stok Güncellendi. }", stockDTOResponse)

        Return Ok(Sonuc(Of StockDTOResponse).SuccessWithData(stockDTOResponse))
    End Function

    <HttpDelete("/RemoveStock/{stockId}")>
    Public Async Function RemoveStock(stockId As Integer) As Task(Of IActionResult)
        Dim stock = Await _stockService.GetAsync(Function(e) e.Id = stockId)
        If stock Is Nothing Then
            Return NotFound(Sonuc(Of StockDTOResponse).SuccessNoDataFound())
        End If
        Await _stockService.RemoveAsync(stock)

        'Log.Information("Stocks => {@stock} => { Stok Silindi. }", stock)

        Return Ok(Sonuc(Of StockDTOResponse).SuccessWithoutData())
    End Function

    <HttpGet("/StocksByCompany/{companyId}")>
    Public Async Function GetStocksByCompany(companyId As Integer) As Task(Of IActionResult)
        Dim stocks = Await _stockService.GetAllAsync(Function(x) x.CompanyId = companyId, {"Company", "Product"})
        If stocks Is Nothing Then
            Return NotFound(Sonuc(Of StockDTOResponse).SuccessNoDataFound())
        End If

        Dim stockDTOResponses As New List(Of StockDTOResponse)()
        For Each stock In stocks
            stockDTOResponses.Add(_mapper.Map(Of StockDTOResponse)(stock))
        Next

        'Log.Information("Stocks => {@stockDTOResponse} => { Stoklar Getirildi. }", stockDTOResponses)
        Return Ok(Sonuc(Of List(Of StockDTOResponse)).SuccessWithData(stockDTOResponses))
    End Function

End Class