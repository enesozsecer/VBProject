Imports System.Threading.Tasks

Namespace ERPProject.DataAccess.Abstract.DataManagement
    Public Interface IUnitOfWork
        ReadOnly Property CategoryRepository As ICategoryRepository
        ReadOnly Property ProductRepository As IProductRepository
        Function SaveChangeAsync() As Task(Of Integer)
    End Interface
End Namespace
