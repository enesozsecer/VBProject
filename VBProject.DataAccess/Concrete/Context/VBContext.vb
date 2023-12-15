
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity
Partial Public Class ErpDB2Context
    Inherits DbContext
    Public Sub New()
    End Sub

    Public Sub New(ByVal options As DbContextOptions(Of ErpDB2Context))
        MyBase.New(options)
    End Sub

    Public Overridable Property Product As DbSet(Of Product)

    Public Overridable Property Category As DbSet(Of Category)



    Protected Overrides Sub OnConfiguring(ByVal optionsBuilder As DbContextOptionsBuilder)
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-R04PVQ3\SQLEXPRESS; Initial Catalog=ErpDB2; Integrated Security=true; TrustServerCertificate=True")
    End Sub
End Class

