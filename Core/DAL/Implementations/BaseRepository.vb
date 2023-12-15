Imports System.Linq.Expressions
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.ChangeTracking

Public Class BaseRepository(Of T As BaseEntity)
    Implements IBaseRepository(Of T)
    Private ReadOnly _context As DbContext
    Private ReadOnly _dbSet As DbSet(Of T)
    Public Sub New(ByVal context As DbContext) ' Constructor Injection
        _context = context
        _dbSet = _context.Set(Of T)()
    End Sub
    Public Async Function GetAsync(Filter As Expression(Of Func(Of T, Boolean)), ParamArray IncludeProperties() As String) As Task(Of T) Implements IBaseRepository(Of T).GetAsync
        Dim query As IQueryable(Of T) = _dbSet

        If IncludeProperties IsNot Nothing Then
            For Each includeProperty As String In IncludeProperties ' "User.Orders.OrderDetails.Product.Category"
                query = query.Include(includeProperty)
            Next
        End If

        ' select * from User where TCNO=12345678901
        Return Await query.SingleOrDefaultAsync(Filter)
    End Function

    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of T, Boolean)) = Nothing, Optional IncludeProperties() As String = Nothing) As Task(Of IEnumerable(Of T)) Implements IBaseRepository(Of T).GetAllAsync
        Dim query As IQueryable(Of T) = _dbSet ' select * from user

        If Filter IsNot Nothing Then
            query = query.Where(Filter) ' select * from User Where id > 5
        End If

        If IncludeProperties IsNot Nothing Then
            For Each includeProperty As String In IncludeProperties ' "User.Orders.OrderDetails.Product.Category"
                query = query.Include(includeProperty)
            Next
        End If

        Return Await Task.Run(Function() query)
    End Function

    Public Async Function AddAsync(Entity As T) As Task(Of EntityEntry(Of T)) Implements IBaseRepository(Of T).AddAsync
        Return Await _dbSet.AddAsync(Entity)
    End Function
    Public Async Function UpdateAsync(Entity As T) As Task Implements IBaseRepository(Of T).UpdateAsync
        Await Task.Run(Sub() _dbSet.Update(Entity))
    End Function

    Public Async Function RemoveAsync(Entity As T) As Task Implements IBaseRepository(Of T).RemoveAsync
        Await Task.Run(Sub() _dbSet.Remove(Entity))
    End Function
End Class
