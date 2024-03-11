Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Data.Native
Imports DevExpress.Xpf.Utils.Themes
Imports UI.BaseFuncs
Imports UI.CategoryHome

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
        If tabControl.SelectedItem Is Nothing Then
            MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
            Return
        End If
        Select Case clickedButton.Name
            Case "AddCategory"
                If tabControl.SelectedItem.Name = "category" Then
                    Dim categoryWindow As CategoryHome = InstanceCategory
                    Dim openWindow As Grid = TryCast(categoryWindow.FindName("openWindow"), Grid)
                    categoryWindow.Button_Click_Open(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case "AddProduct"
                If tabControl.SelectedItem.Name = "product" Then
                    Dim productWindow As ProductHome = ProductHome.InstanceProduct
                    Dim openWindow As Grid = TryCast(productWindow.FindName("openWindow"), Grid)
                    productWindow.Button_Click_Open(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case "AddBrand"
                If tabControl.SelectedItem.Name = "brand" Then
                    Dim brandWindow As BrandHome = BrandHome.InstanceBrand
                    Dim openWindow As Grid = TryCast(brandWindow.FindName("openWindow"), Grid)
                    brandWindow.Button_Click_Open(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case "AddUser"
                If tabControl.SelectedItem.Name = "user" Then
                    Dim userWindow As UserHome = UserHome.InstanceUser
                    Dim openWindow As Grid = TryCast(userWindow.FindName("openWindow"), Grid)
                    userWindow.Button_Click_Open(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case Else
                ' Bu durum gerçekleşirse, varsayılan olarak bir şey yapabilirsiniz.
                Return
        End Select

    End Sub
    Public Sub BarButtonItem_Click_Update(sender As Object, e As RoutedEventArgs) ' başka sayfaya gönderme
        Dim clickedButton As BarButtonItem = DirectCast(sender, BarButtonItem)
        If tabControl.SelectedItem Is Nothing Then
            MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
            Return
        End If
        Select Case clickedButton.Name
            Case "UpdateCategory"
                If tabControl.SelectedItem.Name = "category" Then
                    Dim categoryWindow As CategoryHome = InstanceCategory
                    Dim openWindow As Grid = TryCast(categoryWindow.FindName("openWindow"), Grid)
                    categoryWindow.Button_Click_Open(sender, e)
                    categoryWindow.categorylist_MouseDown(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case "UpdateProduct"
                If tabControl.SelectedItem.Name = "product" Then
                    Dim productWindow As ProductHome = ProductHome.InstanceProduct
                    Dim openWindow As Grid = TryCast(productWindow.FindName("openWindow"), Grid)
                    productWindow.Button_Click_Open(sender, e)
                    productWindow.productlist_MouseDown(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case "UpdateBrand"
                If tabControl.SelectedItem.Name = "brand" Then
                    Dim brandWindow As BrandHome = BrandHome.InstanceBrand
                    Dim openWindow As Grid = TryCast(brandWindow.FindName("openWindow"), Grid)
                    brandWindow.Button_Click_Open(sender, e)
                    brandWindow.brandlist_MouseDown(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case "UpdateUser"
                If tabControl.SelectedItem.Name = "user" Then
                    Dim userWindow As UserHome = UserHome.InstanceUser
                    Dim openWindow As Grid = TryCast(userWindow.FindName("openWindow"), Grid)
                    userWindow.Button_Click_Open(sender, e)
                    userWindow.userlist_MouseDown(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case Else
                ' Bu durum gerçekleşirse, varsayılan olarak bir şey yapabilirsiniz.
                Return
        End Select

    End Sub
    Public Async Sub BarButtonItem_Click_Delete(sender As Object, e As RoutedEventArgs) ' başka sayfaya gönderme
        Dim clickedButton As BarButtonItem = DirectCast(sender, BarButtonItem)
        If tabControl.SelectedItem Is Nothing Then
            MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
            Return
        End If
        Select Case clickedButton.Name
            Case "DeleteCategory"
                If tabControl.SelectedItem.Name = "category" Then
                    Dim Id As Integer = selectedItemId
                    Dim response = Delete($"RemoveCategory/{Id}")
                    Dim categoryWindow As CategoryHome = InstanceCategory
                    Await categoryWindow.GetAllAsync()
                    categoryWindow.Close_Window(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case "DeleteProduct"
                If tabControl.SelectedItem.Name = "product" Then
                    Dim Id As Integer = selectedItemId
                    Dim response = Delete($"RemoveProduct/{Id}")
                    Dim productWindow As ProductHome = ProductHome.InstanceProduct
                    Await productWindow.GetAllAsync()
                    productWindow.Close_Window(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case "DeleteBrand"
                If tabControl.SelectedItem.Name = "brand" Then
                    Dim Id As Integer = selectedItemId
                    Dim response = Delete($"RemoveBrand/{Id}")
                    Dim brandWindow As BrandHome = BrandHome.InstanceBrand
                    Await brandWindow.GetAllAsync()
                    brandWindow.Close_Window(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case "DeleteUser"
                If tabControl.SelectedItem.Name = "user" Then
                    Dim Id As Integer = selectedItemId
                    Dim response = Delete($"RemoveUser/{Id}")
                    Dim userWindow As UserHome = UserHome.InstanceUser
                    Await userWindow.GetAllAsync()
                    userWindow.Close_Window(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case Else
                MessageBox.Show("bi sıkıntı var kontrol et")
                Return
        End Select
    End Sub
    Public Async Sub BarButtonItem_Click_DeleteAll(sender As Object, e As RoutedEventArgs)
        Dim clickedButton As BarButtonItem = DirectCast(sender, BarButtonItem)
        If tabControl.SelectedItem Is Nothing Then
            MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
            Return
        End If
        Select Case clickedButton.Name
            Case "DeleteAllCategory"
                Dim categoryWindow As CategoryHome = InstanceCategory
                If tabControl.SelectedItem.Name = "category" Then
                    For Each item In categoryWindow.categorylist.ItemsSource
                        Dim Id As Integer = item.Id
                        Dim response = Delete($"RemoveCategory/{Id}")
                    Next
                    Await categoryWindow.GetAllAsync()
                    categoryWindow.Close_Window(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case "DeleteAllProduct"
                Dim productWindow As ProductHome = ProductHome.InstanceProduct
                If tabControl.SelectedItem.Name = "product" Then
                    For Each item In productWindow.productlist.ItemsSource
                        Dim Id As Integer = item.Id
                        Dim response = Delete($"RemoveProduct/{Id}")
                    Next
                    Await productWindow.GetAllAsync()
                    productWindow.Close_Window(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case "DeleteAllBrand"
                Dim brandWindow As BrandHome = BrandHome.InstanceBrand
                If tabControl.SelectedItem.Name = "brand" Then
                    For Each item In brandWindow.brandlist.ItemsSource
                        Dim Id As Integer = item.Id
                        Dim response = Delete($"RemoveBrand/{Id}")
                    Next
                    Await brandWindow.GetAllAsync()
                    brandWindow.Close_Window(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
            Case "DeleteAllUser"
                Dim userWindow As UserHome = UserHome.InstanceUser
                If tabControl.SelectedItem.Name = "user" Then
                    For Each item In userWindow.userlist.ItemsSource
                        Dim Id As Integer = item.Id
                        Dim response = Delete($"RemoveUser/{Id}")
                    Next
                    Await userWindow.GetAllAsync()
                    userWindow.Close_Window(sender, e)
                Else
                    MessageBox.Show("İşlem yapmak istediğiniz sayfayı açınız!!!")
                End If
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
                        Case "user"
                            newTabItem.Content = New UserHome
                        Case Else
                            Return
                    End Select
                    tabControl.Items.Add(newTabItem)
                    tabControl.SelectedItem = newTabItem
                    tabControl.SelectedItem.Name = clickedButton.Name
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