Imports System.Linq.Expressions
Imports System.Threading.Tasks
Imports DataAccess
Imports Entities
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports VBProject.Entity

Public Class InvoiceDetailManager
    Implements IInvoiceDetailService

    Private ReadOnly _uow As IUnitOfWork

    Public Sub New(uow As IUnitOfWork)
        _uow = uow
    End Sub

    Public Async Function AddAsync(Entity As InvoiceDetail) As Task(Of InvoiceDetail) Implements IGenericService(Of InvoiceDetail).AddAsync
        Await _uow.InvoiceDetailRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function

    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of InvoiceDetail, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of InvoiceDetail)) Implements IGenericService(Of InvoiceDetail).GetAllAsync
        Return Await _uow.InvoiceDetailRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Async Function GetAsync(Filter As Expression(Of Func(Of InvoiceDetail, Boolean)), ParamArray IncludeProperties As String()) As Task(Of InvoiceDetail) Implements IGenericService(Of InvoiceDetail).GetAsync
        Return Await _uow.InvoiceDetailRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function RemoveAsync(Entity As InvoiceDetail) As Task Implements IGenericService(Of InvoiceDetail).RemoveAsync
        Entity.IsActive = False
        Await _uow.InvoiceDetailRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAsync(Entity As InvoiceDetail) As Task Implements IGenericService(Of InvoiceDetail).UpdateAsync
        Await _uow.InvoiceDetailRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function
End Class
