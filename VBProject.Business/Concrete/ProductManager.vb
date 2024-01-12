Imports System.Linq.Expressions
Imports Core
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.ChangeTracking
Imports VBProject.DataAccess
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports VBProject.Entity


Public Class ProductManager
    Implements IProductService
    Private ReadOnly _uow As IUnitOfWork

    Public Sub New(ByVal uow As IUnitOfWork)
        _uow = uow
    End Sub



    Public Async Function GetAllAsync(ByVal Optional Filter As Expression(Of Func(Of Product, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of Product)) Implements IGenericService(Of Product).GetAllAsync
        Return Await _uow.ProductRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Async Function GetAsync(ByVal Filter As Expression(Of Func(Of Product, Boolean)), ParamArray IncludeProperties As String()) As Task(Of Product) Implements IGenericService(Of Product).GetAsync
        Return Await _uow.ProductRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function RemoveAsync(ByVal Entity As Product) As Task Implements IGenericService(Of Product).RemoveAsync
        Entity.IsActive = False
        Await _uow.ProductRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAsync(ByVal Entity As Product) As Task Implements IGenericService(Of Product).UpdateAsync
        Entity.IsActive = True
        Await _uow.ProductRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Private Async Function AddAsync(Entity As Product) As Task(Of Product) Implements IGenericService(Of Product).AddAsync
        Await _uow.ProductRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function
End Class

