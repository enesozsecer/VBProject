Imports System.Threading.Tasks

Namespace ERPProject.DataAccess.Abstract.DataManagement
    Public Interface IUnitOfWork
        ReadOnly Property CategoryRepository As ICategoryRepository
        ReadOnly Property ProductRepository As IProductRepository
        ReadOnly Property BrandRepository As IBrandRepository
        ReadOnly Property CompanyRepository As ICompanyRepository
        ReadOnly Property DepartmentRepository As IDepartmentRepository
        ReadOnly Property RequestRepository As IRequestRepository
        ReadOnly Property InvoiceRepository As IInvoiceRepository
        ReadOnly Property OfferRepository As IOfferRepository
        ReadOnly Property RoleRepository As IRoleRepository
        ReadOnly Property StockRepository As IStockRepository
        ReadOnly Property StockDetailRepository As IStockDetailRepository
        ReadOnly Property UserRepository As IUserRepository
        ReadOnly Property InvoiceDetailRepository As IInvoiceDetailRepository
        ReadOnly Property UserRoleRepository As IUserRoleRepository
        Function SaveChangeAsync() As Task(Of Integer)
    End Interface
End Namespace
