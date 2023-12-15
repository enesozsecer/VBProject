Imports System.Linq.Expressions


Public Interface IGenericService(Of T)
    Function GetAsync(ByVal Filter As Expression(Of Func(Of T, Boolean)), ParamArray IncludeProperties As String()) As Task(Of T)
    Function GetAllAsync(ByVal Optional Filter As Expression(Of Func(Of T, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of T))

    Function AddAsync(ByVal Entity As T) As Task(Of T)

    Function UpdateAsync(ByVal Entity As T) As Task

    Function RemoveAsync(ByVal Entity As T) As Task

End Interface

