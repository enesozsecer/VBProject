Imports System.Linq.Expressions
Imports System.Threading.Tasks
Imports AutoMapper
Imports DataAccess
Imports Entities
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports VBProject.Entity

Public Class UserManager
    Implements IUserService

    Private ReadOnly _uow As IUnitOfWork
    Private ReadOnly _mapper As IMapper

    Public Sub New(uow As IUnitOfWork, mapper As IMapper)
        _uow = uow
        _mapper = mapper
    End Sub

    Public Async Function AddAsync(Entity As User) As Task(Of User) Implements IGenericService(Of User).AddAsync
        Await _uow.UserRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function

    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of User, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of User)) Implements IGenericService(Of User).GetAllAsync
        Return Await _uow.UserRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Async Function GetAsync(Filter As Expression(Of Func(Of User, Boolean)), ParamArray IncludeProperties As String()) As Task(Of User) Implements IGenericService(Of User).GetAsync
        Return Await _uow.UserRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function RemoveAsync(Entity As User) As Task Implements IGenericService(Of User).RemoveAsync
        Entity.IsActive = False
        Await _uow.UserRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAsync(Entity As User) As Task Implements IGenericService(Of User).UpdateAsync
        Await _uow.UserRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function
End Class
