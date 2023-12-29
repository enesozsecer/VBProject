Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class CategoryController
    Inherits Controller
    Private ReadOnly _categoryService As ICategoryService
    Private ReadOnly _mapper As IMapper

    Public Sub New(mapper As IMapper, categoryService As ICategoryService)
        _mapper = mapper
        _categoryService = categoryService
    End Sub

    <HttpGet("/GetCategories")>
    Public Async Function GetCategories() As Task(Of IActionResult)
        Dim categories = Await _categoryService.GetAllAsync(Function(x) True)

        Dim categoryDTOResponseList As New List(Of CategoryDTOResponse)()
        For Each category In categories
            If category.IsActive = True Then
                categoryDTOResponseList.Add(_mapper.Map(Of CategoryDTOResponse)(category))
            End If
        Next

        Return Ok(categoryDTOResponseList)
    End Function

    <HttpGet("/GetCategory/{categoryId}")>
    Public Async Function GetCategory(categoryId As Integer) As Task(Of IActionResult)
        Dim category As Category = Await _categoryService.GetAsync(Function(x) x.Id = categoryId)

        If category Is Nothing Then
            Return NotFound()
        End If

        Dim categoryDTOResponse As CategoryDTOResponse = _mapper.Map(Of CategoryDTOResponse)(category)

        Return Ok(categoryDTOResponse)
    End Function

    <HttpPost("/AddCategory")>
    Public Async Function AddCategory(categoryDTORequest As CategoryDTORequest) As Task(Of IActionResult)
        Dim category As Category = _mapper.Map(Of Category)(categoryDTORequest)

        Dim existingCategory = Await _categoryService.GetAsync(Function(x) x.Name = category.Name)

        If existingCategory IsNot Nothing Then
            Return BadRequest("Bu kategori zaten var")
        End If

        Await _categoryService.AddAsync(category)

        Dim categoryDTOResponse As CategoryDTOResponse = _mapper.Map(Of CategoryDTOResponse)(category)

        Return Ok(categoryDTOResponse)
    End Function
    <HttpPost("/UpdateCategory")>
    Public Async Function UpdateCategory(categoryDTORequest As CategoryDTORequest) As Task(Of IActionResult)
        Dim category As Category = Await _categoryService.GetAsync(Function(x) x.Id = categoryDTORequest.Id)

        If category Is Nothing Then
            Return NotFound()
        End If

        category = _mapper.Map(categoryDTORequest, category)

        Dim existingCategory = Await _categoryService.GetAsync(Function(x) x.Name = category.Name)

        If existingCategory IsNot Nothing Then
            Return BadRequest("Bu kategori zaten var")
        End If

        Await _categoryService.UpdateAsync(category)

        Dim categoryDTOResponse As CategoryDTOResponse = _mapper.Map(Of CategoryDTOResponse)(category)

        Return Ok(categoryDTOResponse)
    End Function

    <HttpDelete("/RemoveCategory/{categoryId}")>
    Public Async Function RemoveCategory(categoryId As Integer) As Task(Of IActionResult)
        Dim category As Category = Await _categoryService.GetAsync(Function(x) x.Id = categoryId)

        If category Is Nothing Then
            Return NotFound()
        End If

        Await _categoryService.RemoveAsync(category)

        Return Ok()
    End Function


End Class
