Imports System.Linq.Expressions
Imports System.Threading.Tasks
Imports DataAccess
Imports Entities
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports VBProject.Entity

Public Class UserRoleManager
    Implements IUserRoleService

    Private ReadOnly _uow As IUnitOfWork

    Public Sub New(uow As IUnitOfWork)
        _uow = uow
    End Sub

    Public Async Function AddAsync(Entity As UserRole) As Task(Of UserRole) Implements IGenericService(Of UserRole).AddAsync
        Await _uow.UserRoleRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function

    Public Function GetAllAsync(Optional Filter As Expression(Of Func(Of UserRole, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of UserRole)) Implements IGenericService(Of UserRole).GetAllAsync
        Return _uow.UserRoleRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Function GetAsync(Filter As Expression(Of Func(Of UserRole, Boolean)), ParamArray IncludeProperties As String()) As Task(Of UserRole) Implements IGenericService(Of UserRole).GetAsync
        Return _uow.UserRoleRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function RemoveAsync(Entity As UserRole) As Task Implements IGenericService(Of UserRole).RemoveAsync
        Await _uow.UserRoleRepository.RemoveAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAsync(Entity As UserRole) As Task Implements IGenericService(Of UserRole).UpdateAsync
        Await _uow.UserRoleRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function
End Class
