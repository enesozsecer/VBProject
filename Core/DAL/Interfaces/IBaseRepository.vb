Imports System.Linq.Expressions
Imports System.Security.Principal
Imports System.Threading.Tasks
Imports Microsoft.EntityFrameworkCore.ChangeTracking

Public Interface IBaseRepository(Of T As BaseEntity)
    Function GetAsync(ByVal Filter As Expression(Of Func(Of T, Boolean)), ByVal ParamArray IncludeProperties As String()) As Task(Of T)
    Function GetAllAsync(ByVal Optional Filter As Expression(Of Func(Of T, Boolean)) = Nothing, ByVal Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of T))
    Function AddAsync(ByVal Entity As T) As Task(Of EntityEntry(Of T))
    Function UpdateAsync(ByVal Entity As T) As Task
    Function RemoveAsync(ByVal Entity As T) As Task
End Interface