Imports Core
Imports Microsoft.AspNetCore.Http
Imports Microsoft.EntityFrameworkCore
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement


Public Class UnitOfWork
    Implements IUnitOfWork

    Private ReadOnly _context As ErpDB2Context
    Private ReadOnly _contextAccessor As IHttpContextAccessor

    Public Sub New(ByVal contextAccessor As IHttpContextAccessor, ByVal context As ErpDB2Context)
        _contextAccessor = contextAccessor
        _context = context

        CategoryRepository = New CategoryRepository(_context)
        ProductRepository = New ProductRepository(_context)

    End Sub


    Public ReadOnly Property CategoryRepository As ICategoryRepository Implements IUnitOfWork.CategoryRepository


    Public ReadOnly Property ProductRepository As IProductRepository Implements IUnitOfWork.ProductRepository


    Public Function SaveChangeAsync() As Task(Of Integer) Implements IUnitOfWork.SaveChangeAsync
        For Each item In _context.ChangeTracker.Entries(Of BaseEntity)()
            If item.State = EntityState.Added Then
                item.Entity.Id = item.Entity.Id
                item.Entity.AddedTime = DateTime.Now
                item.Entity.UpdatedTime = DateTime.Now
                item.Entity.AddedUser = item.Entity.AddedUser
                item.Entity.UpdatedUser = item.Entity.UpdatedUser
                item.Entity.AddedIP4VAdress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                item.Entity.UpdatedIP4VAdress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()

                If item.Entity.IsActive Is Nothing Then
                    item.Entity.IsActive = True
                End If
            ElseIf item.State = EntityState.Modified Then
                item.Entity.UpdatedTime = DateTime.Now
                item.Entity.UpdatedUser = item.Entity.UpdatedUser
                item.Entity.UpdatedIP4VAdress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                item.Entity.IsActive = True
            End If

            'ElseIf item.State = EntityState.Deleted Then
            '    item.Entity.IsActive = False
            '    item.State = EntityState.Modified
            'End If
        Next

        Return _context.SaveChangesAsync()
    End Function
End Class
