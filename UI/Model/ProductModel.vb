Imports System.Collections.ObjectModel
Imports VBProject.Entity

Public Class ProductModel
    Public Property Id As Integer
    Public Property Name As String
    Public Property Description As String = Nothing
    Public Property CategoryId As Integer
    Public Property BrandId As Integer
    'Public Shared Property Categories As ObservableCollection(Of CategoryDTOResponse) = New ObservableCollection(Of CategoryDTOResponse)()
    'Public Shared Property Products As ObservableCollection(Of ProductDTOResponse) = New ObservableCollection(Of ProductDTOResponse)()
    'Public Shared Property Brands As ObservableCollection(Of BrandDTOResponse) = New ObservableCollection(Of BrandDTOResponse)()
End Class
