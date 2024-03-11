Imports AutoMapper
Imports Microsoft.AspNetCore.Authorization
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class BrandController
    Inherits Controller
    Private ReadOnly _brandService As IBrandService
    Private ReadOnly _mapper As IMapper
    Public Sub New(mapper As IMapper, brandService As IBrandService)
        _mapper = mapper
        _brandService = brandService
    End Sub
    <HttpGet("/GetBrands")>
    Public Async Function GetBrands() As Task(Of IActionResult)
        Dim brands = Await _brandService.GetAllAsync(Function(x) True)

        Dim brandDTOResponseList As New List(Of BrandDTOResponse)()
        For Each brand In brands
            If brand.IsActive = True Then
                brandDTOResponseList.Add(_mapper.Map(Of BrandDTOResponse)(brand))
            End If
        Next

        Return Ok(brandDTOResponseList)
        'Return Ok(Sonuc(Of List(Of BrandDTOResponse)).SuccessWithData(brandDTOResponseList))
    End Function
    <HttpGet("/GetBrand/{brandId}")>
    Public Async Function GetBrand(brandId As Integer) As Task(Of IActionResult)
        Dim brand As Brand = Await _brandService.GetAsync(Function(x) x.Id = brandId)

        If brand Is Nothing Then
            Return NotFound()
        End If

        Dim brandDTOResponse As BrandDTOResponse = _mapper.Map(Of BrandDTOResponse)(brand)

        Return Ok(brandDTOResponse)
    End Function

    <HttpPost("/AddBrand")>
    Public Async Function AddBrand(brandDTORequest As BrandDTORequest) As Task(Of IActionResult)
        Dim brand As Brand = _mapper.Map(Of Brand)(brandDTORequest)

        Dim existingBrand = Await _brandService.GetAsync(Function(x) x.Name = brand.Name)

        If existingBrand IsNot Nothing Then
            Return BadRequest("Bu marka zaten var")
        End If

        Await _brandService.AddAsync(brand)

        Dim brandDTOResponse As BrandDTOResponse = _mapper.Map(Of BrandDTOResponse)(brand)

        Return Ok(brandDTOResponse)
    End Function
    <HttpPost("/UpdateBrand")>
    Public Async Function UpdateBrand(brandDTORequest As BrandDTORequest) As Task(Of IActionResult)
        Dim brand As Brand = Await _brandService.GetAsync(Function(x) x.Id = brandDTORequest.Id)

        If brand Is Nothing Then
            Return NotFound()
        End If

        brand = _mapper.Map(brandDTORequest, brand)

        Dim existingBrand = Await _brandService.GetAsync(Function(x) x.Name = brand.Name)

        If existingBrand IsNot Nothing Then
            Return BadRequest("Bu marka zaten var")
        End If

        Await _brandService.UpdateAsync(brand)

        Dim brandDTOResponse As BrandDTOResponse = _mapper.Map(Of BrandDTOResponse)(brand)

        Return Ok(brandDTOResponse)
    End Function

    <HttpDelete("/RemoveBrand/{brandId}")>
    Public Async Function RemoveBrand(brandId As Integer) As Task(Of IActionResult)
        Dim brand As Brand = Await _brandService.GetAsync(Function(x) x.Id = brandId)

        If brand Is Nothing Then
            Return NotFound()
        End If

        Await _brandService.RemoveAsync(brand)

        Return Ok()
    End Function


End Class

