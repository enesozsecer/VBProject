﻿Imports Newtonsoft.Json.Linq
Imports UI.MainWindow

Public Class BrandHome
    Public Sub New()
        InitializeComponent()
        GetAll()
    End Sub
    Public Async Sub GetAll()
        Dim values As JArray = UI.BaseFuncs.GetAll(Of BrandModel)("GetBrands")
        Dim convertedValues = values.Select(Function(u) u.ToObject(Of BrandModel)()).ToList()
        Dim filteredValues = convertedValues.Select(Function(u) New BrandModel With {.Name = u.Name, .Id = u.Id}).ToList()
        brandlist.ItemsSource = filteredValues

    End Sub
    Private Sub brandlist_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Dim item = brandlist.SelectedItem
        selectedItemId = item.Id
    End Sub
End Class
