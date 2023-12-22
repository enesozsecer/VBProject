Imports System.Linq.Expressions
Imports System.Threading.Tasks
Imports DataAccess
Imports Entities
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports VBProject.Entity

Public Class RequestManager
    Implements IRequestService

    Private ReadOnly _uow As IUnitOfWork

    Public Sub New(uow As IUnitOfWork)
        _uow = uow
    End Sub

    Public Async Function AddAsync(Entity As Request) As Task(Of Request) Implements IGenericService(Of Request).AddAsync
        Await _uow.RequestRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function

    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of Request, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of Request)) Implements IGenericService(Of Request).GetAllAsync
        Return Await _uow.RequestRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Async Function GetAsync(Filter As Expression(Of Func(Of Request, Boolean)), ParamArray IncludeProperties As String()) As Task(Of Request) Implements IGenericService(Of Request).GetAsync
        Return Await _uow.RequestRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function RemoveAsync(Entity As Request) As Task Implements IGenericService(Of Request).RemoveAsync
        Entity.IsActive = False
        Await _uow.RequestRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAsync(Entity As Request) As Task Implements IGenericService(Of Request).UpdateAsync
        Await _uow.RequestRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function
End Class
