Imports System.Linq.Expressions
Imports System.Threading.Tasks
Imports DataAccess
Imports Entities
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports VBProject.Entity

Public Class InvoiceManager
    Implements IInvoiceService

    Private ReadOnly _uow As IUnitOfWork

    Public Sub New(uow As IUnitOfWork)
        _uow = uow
    End Sub

    Public Async Function AddAsync(Entity As Invoice) As Task(Of Invoice) Implements IGenericService(Of Invoice).AddAsync
        Await _uow.InvoiceRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function

    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of Invoice, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of Invoice)) Implements IGenericService(Of Invoice).GetAllAsync
        Return Await _uow.InvoiceRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Async Function GetAsync(Filter As Expression(Of Func(Of Invoice, Boolean)), ParamArray IncludeProperties As String()) As Task(Of Invoice) Implements IGenericService(Of Invoice).GetAsync
        Return Await _uow.InvoiceRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function RemoveAsync(Entity As Invoice) As Task Implements IGenericService(Of Invoice).RemoveAsync
        Entity.IsActive = False
        Await _uow.InvoiceRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAsync(Entity As Invoice) As Task Implements IGenericService(Of Invoice).UpdateAsync
        Await _uow.InvoiceRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function
End Class
