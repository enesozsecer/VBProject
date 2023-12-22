Imports Core
Imports Entities
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class CompanyRepository
    Inherits BaseRepository(Of Company)
    Implements ICompanyRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class
