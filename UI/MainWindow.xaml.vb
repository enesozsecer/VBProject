Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports UI.CategoryHome

'' <summary>
'' Interaction logic for MainWindow.xaml
'' </summary>
Partial Public Class MainWindow
    Inherits ThemedWindow
    Dim usc As UserControl = Nothing

    Public Sub New()
        InitializeComponent()
        'Kategorileri_Getir()
    End Sub
    Public Event StatusOK As EventHandler

    Private Sub BarButtonItem_Click_Add(sender As Object, e As RoutedEventArgs) ' başka sayfaya gönderme
        Dim hedefSayfa As New AddCategory()
        hedefSayfa.Show()
    End Sub
    Private Sub category_ItemClick(sender As Object, e As ItemClickEventArgs)
        usc = New CategoryHome()
        GridMain.Children.Clear()
        GridMain.Children.Add(usc)

        ' Frame içeriğini değiştir
        'Frame.Content = usc

        '' Frame'i görünür yap
        'Frame.Visibility = Visibility.Visible
    End Sub

    'Private Sub UpdateCategory_ItemClick_1(sender As Object, e As ItemClickEventArgs)
    '    Dim hedefSayfa As New UpdateCategory(Id:=KategorilerListView.)
    '    hedefSayfa.Show()
    'End Sub
End Class