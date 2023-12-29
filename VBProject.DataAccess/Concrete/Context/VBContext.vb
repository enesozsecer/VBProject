
Imports System.IO
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
    Public Overridable Property Brand As DbSet(Of Brand)
    Public Overridable Property Company As DbSet(Of Company)
    Public Overridable Property Department As DbSet(Of Department)
    Public Overridable Property Invoice As DbSet(Of Invoice)
    Public Overridable Property InvoiceDetail As DbSet(Of InvoiceDetail)
    Public Overridable Property Offer As DbSet(Of Offer)
    Public Overridable Property Request As DbSet(Of Request)
    Public Overridable Property Role As DbSet(Of Role)
    Public Overridable Property Stock As DbSet(Of Stock)
    Public Overridable Property StockDetail As DbSet(Of StockDetail)
    Public Overridable Property User As DbSet(Of User)
    Public Overridable Property UserRole As DbSet(Of UserRole)

    Protected Overrides Sub OnConfiguring(ByVal optionsBuilder As DbContextOptionsBuilder)
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-R04PVQ3\SQLEXPRESS; Initial Catalog=ErpDB2; Integrated Security=true; TrustServerCertificate=True")
    End Sub





End Class

'dotnet ef dbcontext scaffold "Data Source=DESKTOP-R04PVQ3\SQLEXPRESS; Initial Catalog=ErpDB2; Integrated Security=true; TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -o Models --project "C:\Users\Enes Özseçer\source\repos\VBProject-master\VBProject-master\VBProject.DataAccess\DataAccess.vbproj"aq

