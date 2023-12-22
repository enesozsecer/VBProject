Imports System.Net.Http
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports MaterialDesignThemes.Wpf
Imports Newtonsoft.Json.Linq
Imports UI.BaseFuncs
Imports UI.CategoryHome
Imports UI.UpdateCategory
Imports VBProject.Entity

'' <summary>
'' Interaction logic for MainWindow.xaml
'' </summary>
Partial Public Class MainWindow
    Inherits ThemedWindow
    Dim usc As UserControl = Nothing
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Event StatusOK As EventHandler


    Public Sub BarButtonItem_Click_Add(sender As Object, e As RoutedEventArgs) ' başka sayfaya gönderme
        Dim hedefSayfa As New AddCategory()
        hedefSayfa.Show()
    End Sub

    Public Shared Sub BarButtonItem_Click_Update(sender As Object, e As RoutedEventArgs) ' başka sayfaya gönderme
        Dim hedefSayfaUpdate As New UpdateCategory(selectedItemId)
        hedefSayfaUpdate.ShowDialog()
    End Sub

    Public Sub BarButtonItem_Click_Delete(sender As Object, e As RoutedEventArgs) ' başka sayfaya gönderme
        Dim Id As Integer = selectedItemId
        Dim response = Delete($"RemoveCategory/{Id}")
        BarButtonItem_Click_Get(sender, e)
    End Sub

    Public Sub BarButtonItem_Click_Get(sender As Object, e As RoutedEventArgs)
        usc = New CategoryHome
        GridMain.Children.Clear()
        GridMain.Children.Add(usc)

        BarButtonItem_Click_Hadi(sender, e)
    End Sub

    Public Sub BarButtonItem_Click_Hadi(sender As Object, e As RoutedEventArgs)
        usc = New CategoryHome
        GridMain.Children.Clear()
        GridMain.Children.Add(usc)


    End Sub
    Public Shared selectedItemId As Integer

    Private Sub BarButtonItem_Click_Get_Brand(sender As Object, e As ItemClickEventArgs)
        usc = New BrandHome
        GridMain.Children.Clear()
        GridMain.Children.Add(usc)
    End Sub
    Private Sub BarButtonItem_Click_Get_Product(sender As Object, e As ItemClickEventArgs)
        usc = New ProductHome
        GridMain.Children.Clear()
        GridMain.Children.Add(usc)
    End Sub
End Class