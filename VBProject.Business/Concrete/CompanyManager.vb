Imports System.Linq.Expressions
Imports System.Threading.Tasks
Imports DataAccess
Imports Entities
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports VBProject.Entity

Public Class CompanyManager
    Implements ICompanyService

    Private ReadOnly _uow As IUnitOfWork

    Public Sub New(uow As IUnitOfWork)
        _uow = uow
    End Sub

    Public Async Function AddAsync(Entity As Company) As Task(Of Company) Implements IGenericService(Of Company).AddAsync
        Await _uow.CompanyRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function

    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of Company, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of Company)) Implements IGenericService(Of Company).GetAllAsync
        Return Await _uow.CompanyRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Async Function GetAsync(Filter As Expression(Of Func(Of Company, Boolean)), ParamArray IncludeProperties As String()) As Task(Of Company) Implements IGenericService(Of Company).GetAsync
        Return Await _uow.CompanyRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function RemoveAsync(Entity As Company) As Task Implements IGenericService(Of Company).RemoveAsync
        Entity.IsActive = False
        Await _uow.CompanyRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAsync(Entity As Company) As Task Implements IGenericService(Of Company).UpdateAsync
        Await _uow.CompanyRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function
End Class
