Imports System.Linq.Expressions
Imports System.Threading.Tasks
Imports DataAccess
Imports Entities
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports VBProject.Entity

Public Class StockDetailManager
    Implements IStockDetailService

    Private ReadOnly _uow As IUnitOfWork

    Public Sub New(uow As IUnitOfWork)
        _uow = uow
    End Sub

    Public Async Function AddAsync(Entity As StockDetail) As Task(Of StockDetail) Implements IGenericService(Of StockDetail).AddAsync
        Await _uow.StockDetailRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function

    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of StockDetail, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of StockDetail)) Implements IGenericService(Of StockDetail).GetAllAsync
        Return Await _uow.StockDetailRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Async Function GetAsync(Filter As Expression(Of Func(Of StockDetail, Boolean)), ParamArray IncludeProperties As String()) As Task(Of StockDetail) Implements IGenericService(Of StockDetail).GetAsync
        Return Await _uow.StockDetailRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function RemoveAsync(Entity As StockDetail) As Task Implements IGenericService(Of StockDetail).RemoveAsync
        Entity.IsActive = False
        Await _uow.StockDetailRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAsync(Entity As StockDetail) As Task Implements IGenericService(Of StockDetail).UpdateAsync
        Await _uow.StockDetailRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function
End Class
