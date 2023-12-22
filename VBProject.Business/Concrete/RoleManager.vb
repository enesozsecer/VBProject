Imports System.Linq.Expressions
Imports System.Threading.Tasks
Imports DataAccess
Imports Entities
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports VBProject.Entity

Public Class RoleManager
    Implements IRoleService

    Private ReadOnly _uow As IUnitOfWork

    Public Sub New(uow As IUnitOfWork)
        _uow = uow
    End Sub

    Public Async Function AddAsync(Entity As Role) As Task(Of Role) Implements IGenericService(Of Role).AddAsync
        Await _uow.RoleRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function

    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of Role, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of Role)) Implements IGenericService(Of Role).GetAllAsync
        Return Await _uow.RoleRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Async Function GetAsync(Filter As Expression(Of Func(Of Role, Boolean)), ParamArray IncludeProperties As String()) As Task(Of Role) Implements IGenericService(Of Role).GetAsync
        Return Await _uow.RoleRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function RemoveAsync(Entity As Role) As Task Implements IGenericService(Of Role).RemoveAsync
        Entity.IsActive = False
        Await _uow.RoleRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAsync(Entity As Role) As Task Implements IGenericService(Of Role).UpdateAsync
        Await _uow.RoleRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function
End Class
