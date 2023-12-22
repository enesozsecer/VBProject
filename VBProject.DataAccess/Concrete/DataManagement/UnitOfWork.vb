Imports Core
Imports Microsoft.AspNetCore.Http
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata.Internal
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
        BrandRepository = New BrandRepository(_context)
        CompanyRepository = New CompanyRepository(_context)
        DepartmentRepository = New DepartmentRepository(_context)
        RequestRepository = New RequestRepository(_context)
        InvoiceRepository = New InvoiceRepository(_context)
        OfferRepository = New OfferRepository(_context)
        RoleRepository = New RoleRepository(_context)
        StockRepository = New StockRepository(_context)
        StockDetailRepository = New StockDetailRepository(_context)
        UserRepository = New UserRepository(_context)
        InvoiceDetailRepository = New InvoiceDetailRepository(_context)
        UserRoleRepository = New UserRoleRepository(_context)

    End Sub


    Public ReadOnly Property CategoryRepository As ICategoryRepository Implements IUnitOfWork.CategoryRepository
    Public ReadOnly Property ProductRepository As IProductRepository Implements IUnitOfWork.ProductRepository


    Public ReadOnly Property BrandRepository As IBrandRepository Implements IUnitOfWork.BrandRepository
    Public ReadOnly Property CompanyRepository As ICompanyRepository Implements IUnitOfWork.CompanyRepository
    Public ReadOnly Property DepartmentRepository As IDepartmentRepository Implements IUnitOfWork.DepartmentRepository
    Public ReadOnly Property RequestRepository As IRequestRepository Implements IUnitOfWork.RequestRepository
    Public ReadOnly Property InvoiceRepository As IInvoiceRepository Implements IUnitOfWork.InvoiceRepository
    Public ReadOnly Property OfferRepository As IOfferRepository Implements IUnitOfWork.OfferRepository
    Public ReadOnly Property RoleRepository As IRoleRepository Implements IUnitOfWork.RoleRepository
    Public ReadOnly Property StockRepository As IStockRepository Implements IUnitOfWork.StockRepository
    Public ReadOnly Property StockDetailRepository As IStockDetailRepository Implements IUnitOfWork.StockDetailRepository
    Public ReadOnly Property UserRepository As IUserRepository Implements IUnitOfWork.UserRepository
    Public ReadOnly Property InvoiceDetailRepository As IInvoiceDetailRepository Implements IUnitOfWork.InvoiceDetailRepository
    Public ReadOnly Property UserRoleRepository As IUserRoleRepository Implements IUnitOfWork.UserRoleRepository


    Public Function SaveChangeAsync() As Task(Of Integer) Implements IUnitOfWork.SaveChangeAsync
        For Each item In _context.ChangeTracker.Entries(Of BaseEntity)()
            If item.State = EntityState.Added Then

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

            Else
                item.Entity.UpdatedTime = DateTime.Now
                item.Entity.UpdatedUser = item.Entity.UpdatedUser
                item.Entity.UpdatedIP4VAdress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                item.Entity.IsActive = False
            End If


        Next

        Return _context.SaveChangesAsync()
    End Function
End Class
