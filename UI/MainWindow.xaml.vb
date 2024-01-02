Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports MaterialDesignThemes.Wpf
Imports UI.BaseFuncs
Imports UI.CategoryHome
Imports VBProject.Entity

'' <summary>
'' Interaction logic for MainWindow.xaml
'' </summary>
Partial Public Class MainWindow
    Inherits ThemedWindow
    'Dim usc As UserControl = Nothing

    Public Sub New()
        InitializeComponent()
    End Sub
    Public Event StatusOK As EventHandler

    Public Async Sub Logout()
        logOutBtn.IsEnabled = False
        Application.Current.Properties.Clear()
        Me.Close()
        Dim loginWindow As New LoginWindow()
        loginWindow.Show()
    End Sub

    Public Sub BarButtonItem_Click_Add(sender As Object, e As RoutedEventArgs) ' başka sayfaya gönderme

        If category.IsEnabled = True Then
            Dim hedefSayfa As New AddCategory()
            hedefSayfa.Show()
            'ElseIf product.IsEnabled = True Then
            '    Dim hedefSayfa As New AddProduct()
            '    hedefSayfa.show()
        End If

    End Sub
    Public Sub BarButtonItem_Click_Update(sender As Object, e As RoutedEventArgs) ' başka sayfaya gönderme
        Dim clickedButton As BarButtonItem = DirectCast(sender, BarButtonItem)

        Select Case clickedButton.Name
            Case "UpdateCategory"
                categorylist_MouseDown(sender, e)
                Dim hedefSayfaUpdate As New CategoryHome()
                'hedefSayfaUpdate.Close_Window(sender, e)
                'Case "product"
                '    Dim hedefSayfaUpdate As New UpdateProduct(selectedItemId)
                '    hedefSayfaUpdate.Show()
                'Case "brand"
                '    Dim hedefSayfaUpdate As New UpdateBrand(selectedItemId)
                '    hedefSayfaUpdate.Show()
            Case Else
                ' Bu durum gerçekleşirse, varsayılan olarak bir şey yapabilirsiniz.
                Return
        End Select

    End Sub

    Public Sub BarButtonItem_Click_Delete(sender As Object, e As RoutedEventArgs) ' başka sayfaya gönderme

        Dim clickedButton As BarButtonItem = DirectCast(sender, BarButtonItem)
        Select Case clickedButton.Name
            Case "DeleteCategory"
                Dim Id As Integer = selectedItemId
                Dim response = Delete($"RemoveCategory/{Id}")
            Case "DeleteProduct"
                Dim Id As Integer = selectedItemId
                Dim response = Delete($"RemoveProduct/{Id}")
            Case "DeleteBrand"
                Dim Id As Integer = selectedItemId
                Dim response = Delete($"RemoveBrand/{Id}")
            Case Else
                ' Bu durum gerçekleşirse, varsayılan olarak bir şey yapabilirsiniz.
                Return
        End Select
    End Sub

    Public Sub BarButtonItem_Click_Get(sender As Object, e As RoutedEventArgs)
        tabControl.Visibility = Visibility.Visible
        Dim clickedButton As BarButtonItem = TryCast(sender, BarButtonItem)
        If clickedButton IsNot Nothing Then
            Dim tabItemTag As Object = clickedButton.Tag
            If tabItemTag IsNot Nothing Then
                ' TabControl içindeki belirli bir sekmenin kontrolü
                Dim existingTabItem As TabItem = FindTabItemByTag(tabItemTag)

                If existingTabItem IsNot Nothing Then
                    ' Eğer sekme zaten varsa, onu seç
                    tabControl.SelectedItem = existingTabItem
                Else
                    ' Eğer sekme yoksa, yeni bir sekme oluştur ve ekledikten sonra seç
                    Dim newTabItem As New TabItem() With {
                    .Tag = tabItemTag
                }

                    ' Başlık ve kapatma butonunu içeren StackPanel
                    Dim headerStackPanel As New StackPanel With {
                    .Orientation = Orientation.Horizontal
                }

                    ' Başlık TextBlock
                    Dim headerTextBlock As New TextBlock With {
                    .Text = clickedButton.Content.ToString(),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .HorizontalAlignment = HorizontalAlignment.Left
                }

                    ' Kapatma butonu
                    Dim closeButton As New Button With {
                .Background = New SolidColorBrush(Colors.Red),
                .HorizontalAlignment = HorizontalAlignment.Right,
                .VerticalAlignment = VerticalAlignment.Top,
                .Margin = New Thickness(25, 0, -7, 0),
                .Width = 9,
                .Height = 9,
                .BorderThickness = New Thickness(0, 0, 0, 0),
                .Content = New Image With {
                    .Source = New BitmapImage(New Uri("/UI/ViewModel/Image/closebutton2.png", UriKind.Relative)),
                    .Width = 9,
                    .Height = 9
                }
                }
                    AddHandler closeButton.Click, AddressOf CloseTabButtonClick

                    ' StackPanel'a başlık ve kapatma butonunu ekle
                    headerStackPanel.Children.Add(headerTextBlock)
                    headerStackPanel.Children.Add(closeButton)

                    ' StackPanel'ı yeni sekmenin başlığı olarak ayarla
                    newTabItem.Header = headerStackPanel

                    ' İlgili sayfa yüklenmeli veya içeriği ayarlanmalı
                    ' Örneğin:
                    'usc = New CategoryHome
                    Select Case clickedButton.Name
                        Case "category"
                            newTabItem.Content = New CategoryHome
                        Case "product"
                            newTabItem.Content = New ProductHome
                        Case "brand"
                            newTabItem.Content = New BrandHome
                        Case Else
                            Return
                    End Select

                    ' newTabItem.Content = New YourPage()

                    tabControl.Items.Add(newTabItem)
                    tabControl.SelectedItem = newTabItem
                End If
            End If
        End If

        'GridMain.Children.Clear()
        'GridMain.Children.Add(usc)

    End Sub
    Private Function FindTabItemByTag(tag As Object) As TabItem
        For Each item As TabItem In tabControl.Items
            If Object.Equals(item.Tag, tag) Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Shared selectedItemId As Integer

    Private Sub TabControl_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        ' TabItem seçildiğinde burası çalışır
        Dim selectedTabItem As TabItem = TryCast(tabControl.SelectedItem, TabItem)
        If selectedTabItem IsNot Nothing AndAlso selectedTabItem.Tag IsNot Nothing Then
            ' İstenilen tabItem seçildiğinde işlemleri burada yapabilirsiniz
            ' Örneğin: selectedTabItem.Tag.ToString() ile tab adını alabilirsiniz
        End If
    End Sub

    Public Sub CloseTabButtonClick(sender As Object, e As RoutedEventArgs)
        Dim closeButton As Button = TryCast(sender, Button)
        If closeButton IsNot Nothing Then
            Dim parentTabItem As TabItem = FindParentTabItem(closeButton)
            If parentTabItem IsNot Nothing Then
                tabControl.Items.Remove(parentTabItem)
            End If
        End If
    End Sub

    Private Function FindParentTabItem(element As FrameworkElement) As TabItem
        Dim parent As DependencyObject = VisualTreeHelper.GetParent(element)

        While parent IsNot Nothing AndAlso Not TypeOf parent Is TabItem
            parent = VisualTreeHelper.GetParent(parent)
        End While

        Return TryCast(parent, TabItem)
    End Function

    Public Sub Close_Window(sender As Object, e As RoutedEventArgs)
        openWindow1.Visibility = Visibility.Collapsed
    End Sub
    Private a As New CategoryHome
    Public Sub categorylist_MouseDown(sender As Object, e As RoutedEventArgs)

        Dim item = a.categorylist.SelectedItem
        selectedItemId = item.Id
        CatId.Text = item.Id
        CategoryInput.Text = item.Name
        openWindow1.Visibility = Visibility.Visible
    End Sub
End Class