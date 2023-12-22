Imports System.Linq.Expressions
Imports System.Threading.Tasks
Imports DataAccess
Imports Entities
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports VBProject.Entity

Public Class StockManager
    Implements IStockService

    Private ReadOnly _uow As IUnitOfWork

    Public Sub New(uow As IUnitOfWork)
        _uow = uow
    End Sub

    Public Async Function AddAsync(Entity As Stock) As Task(Of Stock) Implements IGenericService(Of Stock).AddAsync
        Await _uow.StockRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function

    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of Stock, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of Stock)) Implements IGenericService(Of Stock).GetAllAsync
        Return Await _uow.StockRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Async Function GetAsync(Filter As Expression(Of Func(Of Stock, Boolean)), ParamArray IncludeProperties As String()) As Task(Of Stock) Implements IGenericService(Of Stock).GetAsync
        Return Await _uow.StockRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function RemoveAsync(Entity As Stock) As Task Implements IGenericService(Of Stock).RemoveAsync
        Entity.IsActive = False
        Await _uow.StockRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAsync(Entity As Stock) As Task Implements IGenericService(Of Stock).UpdateAsync
        Await _uow.StockRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function
End Class
