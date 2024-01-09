Imports System.Windows.Threading
Imports DevExpress.Mvvm.Native
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Core.Native
Imports MaterialDesignThemes.Wpf
Imports Microsoft.Identity.Client
Imports UI.BaseFuncs
Imports UI.CategoryHome
Imports VBProject.Entity

Partial Public Class MainWindow
    Inherits Window
    Public Shared selectedItemId As Integer
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub CloseButton_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub MinimizeButton_Click(sender As Object, e As RoutedEventArgs)
        WindowState = WindowState.Minimized
    End Sub

    Private Sub Border_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        DragMove()
    End Sub
    Private Sub MaxMinButton_Click(sender As Object, e As RoutedEventArgs)
        If Me.WindowState = WindowState.Normal Then
            Me.WindowState = WindowState.Maximized
        ElseIf Me.WindowState = WindowState.Maximized Then
            Me.WindowState = WindowState.Normal
        End If
    End Sub
    Public Async Sub Logout()
        logOutBtn.IsEnabled = False
        Application.Current.Properties.Clear()
        Close()
        Dim loginWindow As New LoginWindow()
        loginWindow.Show()
    End Sub






    Public Sub BarButtonItem_Click_Add(sender As Object, e As RoutedEventArgs) ' başka sayfaya gönderme
        Dim clickedButton As BarButtonItem = DirectCast(sender, BarButtonItem)
        Select Case clickedButton.Name
            Case "AddCategory"
                Dim categoryWindow As CategoryHome = InstanceCategory
                Dim openWindow As Grid = TryCast(categoryWindow.FindName("openWindow"), Grid)
                categoryWindow.Button_Click_Open(sender, e)
            Case "AddProduct"
                Dim productWindow As ProductHome = ProductHome.InstanceProduct
                Dim openWindow As Grid = TryCast(productWindow.FindName("openWindow"), Grid)
                productWindow.Button_Click_Open(sender, e)
            Case Else
                ' Bu durum gerçekleşirse, varsayılan olarak bir şey yapabilirsiniz.
                Return
        End Select

    End Sub
    Public Sub BarButtonItem_Click_Update(sender As Object, e As RoutedEventArgs) ' başka sayfaya gönderme
        Dim clickedButton As BarButtonItem = DirectCast(sender, BarButtonItem)

        Select Case clickedButton.Name
            Case "UpdateCategory"
                Dim categoryWindow As CategoryHome = InstanceCategory
                Dim openWindow As Grid = TryCast(categoryWindow.FindName("openWindow"), Grid)
                categoryWindow.Button_Click_Open(sender, e)
                categoryWindow.categorylist_MouseDown(sender, e)
            Case "UpdateProduct"
                Dim productWindow As ProductHome = ProductHome.InstanceProduct
                Dim openWindow As Grid = TryCast(productWindow.FindName("openWindow"), Grid)
                productWindow.Button_Click_Open(sender, e)
                productWindow.productlist_MouseDown(sender, e)
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
                Dim categoryWindow As CategoryHome = InstanceCategory
                categoryWindow.GetAll()
                categoryWindow.Close_Window(sender, e)
            Case "DeleteProduct"
                Dim Id As Integer = selectedItemId
                Dim response = Delete($"RemoveProduct/{Id}")
                Dim productWindow As ProductHome = ProductHome.InstanceProduct
                productWindow.GetAll()
                productWindow.Close_Window(sender, e)
            Case "DeleteBrand"
                Dim Id As Integer = selectedItemId
                Dim response = Delete($"RemoveBrand/{Id}")
            Case Else
                MessageBox.Show("bi sıkıntı var kontrol et")
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
                    .Foreground = New SolidColorBrush(Colors.White),
                    .Text = clickedButton.Content.ToString(),
                    .VerticalAlignment = VerticalAlignment.Center,
                    .HorizontalAlignment = HorizontalAlignment.Left
                }

                    ' Kapatma butonu
                    Dim closeButton As New Button With {
                .Foreground = New SolidColorBrush(Colors.White),
                .Background = New SolidColorBrush(Colors.Red),
                .HorizontalAlignment = HorizontalAlignment.Center,
                .VerticalAlignment = VerticalAlignment.Top,
                .Margin = New Thickness(25, 0, -7, 0),
                .Width = 9,
                .Height = 9,
                .BorderThickness = New Thickness(0, 0, 0, 0),
                .Content = "X",
                .FontSize = 2,
                .VerticalContentAlignment = VerticalContentAlignment.Center
                }
                    AddHandler closeButton.Click, AddressOf CloseTabButtonClick

                    ' StackPanel'a başlık ve kapatma butonunu ekle
                    headerStackPanel.Children.Add(headerTextBlock)
                    headerStackPanel.Children.Add(closeButton)

                    ' StackPanel'ı yeni sekmenin başlığı olarak ayarla
                    newTabItem.Header = headerStackPanel

                    ' İlgili sayfa yüklenmeli veya içeriği ayarlanmalı
                    ' Örneğin:
                    ' newTabItem.Content = New YourPage()
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
                    tabControl.Items.Add(newTabItem)
                    tabControl.SelectedItem = newTabItem
                End If
            End If
        End If
    End Sub
    Private Function FindTabItemByTag(tag As Object) As TabItem
        For Each item As TabItem In tabControl.Items
            If Object.Equals(item.Tag, tag) Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public Sub TabControl_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim selectedTabItem As TabItem = TryCast(tabControl.SelectedItem, TabItem)

        For Each tabItem As TabItem In tabControl.Items
            ' Başlık StackPanel
            Dim headerStackPanel As StackPanel = TryCast(tabItem.Header, StackPanel)

            If headerStackPanel IsNot Nothing Then
                ' Başlık TextBlock
                Dim headerTextBlock As TextBlock = TryCast(headerStackPanel.Children.OfType(Of TextBlock)().FirstOrDefault(), TextBlock)

                If headerTextBlock IsNot Nothing Then
                    ' Rengi siyah olan TabItem'ın başlık rengini beyaza dönüştür
                    If tabItem.IsSelected Then
                        headerTextBlock.Foreground = New SolidColorBrush(Colors.Black)
                    Else
                        headerTextBlock.Foreground = New SolidColorBrush(Colors.White)
                        tabItem.Background = New SolidColorBrush(Colors.DarkBlue)
                    End If
                End If
            End If
        Next
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

End Class