Imports System.Linq.Expressions
Imports System.Threading.Tasks
Imports DataAccess
Imports Entities
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports VBProject.Entity

Public Class DepartmentManager
    Implements IDepartmentService

    Private ReadOnly _uow As IUnitOfWork

    Public Sub New(uow As IUnitOfWork)
        _uow = uow
    End Sub

    Public Async Function AddAsync(Entity As Department) As Task(Of Department) Implements IGenericService(Of Department).AddAsync
        Await _uow.DepartmentRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function

    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of Department, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of Department)) Implements IGenericService(Of Department).GetAllAsync
        Return Await _uow.DepartmentRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Async Function GetAsync(ByVal Filter As Expression(Of Func(Of Department, Boolean)), ParamArray IncludeProperties As String()) As Task(Of Department) Implements IGenericService(Of Department).GetAsync
        Return Await _uow.DepartmentRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function RemoveAsync(Entity As Department) As Task Implements IGenericService(Of Department).RemoveAsync
        Entity.IsActive = False
        Await _uow.DepartmentRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAsync(Entity As Department) As Task Implements IGenericService(Of Department).UpdateAsync
        Await _uow.DepartmentRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function
End Class
