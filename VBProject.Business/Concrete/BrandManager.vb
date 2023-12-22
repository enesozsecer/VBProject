Imports System.Linq.Expressions
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports VBProject.Entity

Public Class BrandManager
    Implements IBrandService

    Private ReadOnly _uow As IUnitOfWork

    Public Sub New(uow As IUnitOfWork)
        _uow = uow
    End Sub

    Public Async Function AddAsync(Entity As Brand) As Task(Of Brand) Implements IGenericService(Of Brand).AddAsync
        Await _uow.BrandRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function

    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of Brand, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of Brand)) Implements IGenericService(Of Brand).GetAllAsync
        Return Await _uow.BrandRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Async Function GetAsync(Filter As Expression(Of Func(Of Brand, Boolean)), ParamArray IncludeProperties As String()) As Task(Of Brand) Implements IGenericService(Of Brand).GetAsync
        Return Await _uow.BrandRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function RemoveAsync(Entity As Brand) As Task Implements IGenericService(Of Brand).RemoveAsync
        Entity.IsActive = False
        Await _uow.BrandRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAsync(Entity As Brand) As Task Implements IGenericService(Of Brand).UpdateAsync
        Await _uow.BrandRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function
End Class
