Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class ProductController
    Inherits Controller
    Private ReadOnly _productService As IProductService
    Private ReadOnly _mapper As IMapper

    Public Sub New(mapper As IMapper, productService As IProductService)
        _mapper = mapper
        _productService = productService
    End Sub

    <HttpGet("/GetProducts")>
    Public Async Function GetProducts() As Task(Of IActionResult)
        Dim products = Await _productService.GetAllAsync(Function(x) x.IsActive = True, {"Category", "Brand"})

        Dim productDTOResponseList As New List(Of ProductDTOResponse)()
        For Each product In products
            If product.IsActive = True Then
                productDTOResponseList.Add(_mapper.Map(Of ProductDTOResponse)(product))
            End If
        Next

        Return Ok(productDTOResponseList)
    End Function
    <HttpGet("/GetProduct/{ProductId}")>
    Public Async Function GetProduct(ProductId As Integer) As Task(Of IActionResult)
        Dim product As Product = Await _productService.GetAsync(Function(x) x.Id = ProductId, {"Category", "Brand"})

        If product Is Nothing Then
            Return NotFound()
        End If

        Dim productDTOResponse As ProductDTOResponse = _mapper.Map(Of ProductDTOResponse)(product)

        Return Ok(productDTOResponse)
    End Function

    <HttpPost("/AddProduct")>
    Public Async Function AddProduct(productDTORequest As ProductDTORequest) As Task(Of IActionResult)
        Dim product As Product = _mapper.Map(Of Product)(productDTORequest)

        Dim existingProduct = Await _productService.GetAsync(Function(x) x.Name = product.Name, {"Category", "Brand"})

        If existingProduct IsNot Nothing Then
            Return BadRequest("Bu kategori zaten var")
        End If

        Await _productService.AddAsync(Product)

        Dim productDTOResponse As ProductDTOResponse = _mapper.Map(Of ProductDTOResponse)(product)

        Return Ok(productDTOResponse)
    End Function
    <HttpPost("/UpdateProduct")>
    Public Async Function UpdateProduct(productDTORequest As ProductDTORequest) As Task(Of IActionResult)
        Dim product As Product = Await _productService.GetAsync(Function(x) x.Id = productDTORequest.Id, {"Category", "Brand"})

        If product Is Nothing Then
            Return NotFound()
        End If

        product = _mapper.Map(productDTORequest, product)

        Dim existingProduct = Await _productService.GetAsync(Function(x) x.Name = product.Name)

        If existingProduct IsNot Nothing Then
            Return BadRequest("Bu kategori zaten var")
        End If

        Await _productService.UpdateAsync(product)

        Dim productDTOResponse As ProductDTOResponse = _mapper.Map(Of ProductDTOResponse)(product)

        Return Ok(productDTOResponse)
    End Function

    <HttpDelete("/RemoveProduct/{ProductId}")>
    Public Async Function RemoveProduct(ProductId As Integer) As Task(Of IActionResult)
        Dim Product As Product = Await _productService.GetAsync(Function(x) x.Id = ProductId, {"Category", "Brand"})

        If Product Is Nothing Then
            Return NotFound()
        End If

        Await _productService.RemoveAsync(Product)

        Return Ok()
    End Function

    <HttpGet("/GetProductsByCategory/{categoryId}")>
    Public Async Function GetProductsByCategory(categoryId As Integer) As Task(Of IActionResult)
        Dim products = Await _productService.GetAllAsync(Function(x) x.IsActive = True AndAlso x.CategoryId = categoryId, {"Category", "Brand"})
        If products Is Nothing Then
            Return NotFound(Sonuc(Of ProductDTOResponse).SuccessNoDataFound())
        End If

        Dim productDTOResponseList As New List(Of ProductDTOResponse)()
        For Each product In products
            productDTOResponseList.Add(_mapper.Map(Of ProductDTOResponse)(product))
        Next
        'Log.Information("Products => {@productDTOResponse} => { Kategoriye Göre Ürünler Getirildi. }", productDTOResponseList)
        Return Ok(Sonuc(Of List(Of ProductDTOResponse)).SuccessWithData(productDTOResponseList))
    End Function

    <HttpGet("/GetProductsByBrand/{brandId}")>
    Public Async Function GetProductsByBrand(brandId As Integer) As Task(Of IActionResult)
        Dim products = Await _productService.GetAllAsync(Function(x) x.IsActive = True AndAlso x.BrandId = brandId, {"Category", "Brand"})
        If products Is Nothing Then
            Return NotFound(Sonuc(Of ProductDTOResponse).SuccessNoDataFound())
        End If

        Dim productDTOResponseList As New List(Of ProductDTOResponse)()
        For Each product In products
            productDTOResponseList.Add(_mapper.Map(Of ProductDTOResponse)(product))
        Next
        'Log.Information("Products => {@productDTOResponse} => { Markaya Göre Ürünler Getirildi. }", productDTOResponseList)
        Return Ok(Sonuc(Of List(Of ProductDTOResponse)).SuccessWithData(productDTOResponseList))
    End Function

End Class
