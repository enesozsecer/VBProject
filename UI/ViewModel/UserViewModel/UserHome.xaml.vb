Imports System.Text
Imports System.Windows.Forms
Imports Newtonsoft.Json.Linq
Imports UI.BaseFuncs
Imports UI.MainWindow
Imports VBProject.Entity
Imports RestSharp
Imports Microsoft.AspNetCore.Http
Public Class UserHome

    Public Sub New()
        InitializeComponent()
        GetAllAsync()
        InstanceUser = Me
    End Sub
    Public Shared Property InstanceUser As UserHome

    Public Async Function GetAllAsync() As Task(Of List(Of String))
        Dim values = GetAll(Of UserModel)("GetUsers")
        Dim convertedValues = values.Select(Function(u) u.ToObject(Of UserModel)()).ToList()
        Dim filteredValues = convertedValues.Select(Function(u) New With {
            .CompanyId = u.CompanyId,
            .Name = u.Name,
            .Id = u.Id,
            .Email = u.Email,
            .DepartmentName = u.DepartmentName,
            .CompanyName = u.CompanyName,
            .LastName = u.LastName,
            .Phone = u.Phone,
            .Password = u.Password,
            .RoleName = String.Join(Environment.NewLine, u.RoleName)
        }).ToList()
        userlist.ItemsSource = filteredValues
    End Function
    Public Sub userlist_MouseDown(sender As Object, e As RoutedEventArgs)
        Dim item = userlist.SelectedItem
        If item Is Nothing Then
            MessageBox.Show("Seçim yapmadınız veya veri yok!!!")
            Return
        End If
        _selectedCompanyId = item.CompanyId
        selectedItemId = item.Id
        IdInput.Text = item.Id
        NameInput.Text = item.Name
        LastNameInput.Text = item.LastName
        EmailInput.Text = item.Email
        DepartmentTextInput.Text = item.DepartmentName
        CompanyTextInput.Text = item.CompanyName
        RoleTextInput.Text = item.RoleName
        PasswordInput.Text = item.Password
        PhoneInput.Text = item.Phone
        openWindow.Visibility = Visibility.Visible

    End Sub
    Public Sub Close_Window(sender As Object, e As RoutedEventArgs)
        openWindow.Visibility = Visibility.Collapsed
    End Sub
    Public Sub GetById(Id As Integer)
        Dim user As UserDTORequest = GetOne(Of UserDTORequest)("GetUser/" + Id.ToString())
        IdInput.Text = user.Id
    End Sub
    Public Async Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim clickButton As Button
        If clickButton IsNot Nothing Then
            clickButton.BackColor = System.Drawing.Color.Black
        End If
        If IdInput.Text = "" Then
            Dim p As UserDTOBase = New UserDTOBase()
            Dim q As UserDTOResponse = New UserDTOResponse()
            p.Name = NameInput.Text
            p.Email = EmailInput.Text
            p.Password = PasswordInput.Text
            p.Phone = PhoneInput.Text
            p.DepartmentId = Convert.ToInt32(DepartmentTextId.Text)
            q.CompanyId = Convert.ToInt32(CompanyTextId.Text)
            Dim response = Add(Of UserDTOBase)("AddUser", p)
            If response Then
                Await GetAllAsync()
            Else
                MessageBox.Show("Bir sorun oluştu...")
            End If
        Else
            Dim p As UserDTOBase = New UserDTOBase()
            p.Name = NameInput.Text
            p.Id = Convert.ToUInt32(IdInput.Text)
            p.Email = EmailInput.Text
            p.Password = PasswordInput.Text
            p.Phone = PhoneInput.Text
            p.DepartmentId = Convert.ToInt32(DepartmentTextId.Text)
            p.CompanyId = Convert.ToInt32(CompanyTextId.Text)
            Dim response = Update(Of UserDTOBase)(p, "UpdateUser")
            If response Then
                GetAllAsync()
            Else
                MessageBox.Show("Bir sorun oluştu...")
            End If
        End If


    End Sub
    Public Sub Button_Click_Open(sender As Object, e As RoutedEventArgs)
        _selectedCompanyId = Nothing
        IdInput.Text = ""
        NameInput.Text = ""
        EmailInput.Text = ""
        DepartmentTextInput.Text = ""
        CompanyTextInput.Text = ""
        RoleTextInput.Text = ""
        PasswordInput.Text = ""
        PhoneInput.Text = ""
        openWindow.Visibility = Visibility.Visible

    End Sub

    Private Sub PreviewKeyUpDepartment(sender As Object, e As Input.KeyEventArgs)
        Dim filterTextDepartment As String = DepartmentTextInput.Text.ToLower()
        Dim departments As JArray
        If _selectedCompanyId IsNot Nothing Then
            departments = GetAll(Of DepartmentModel)("GetDepartmentsByCompany/" + _selectedCompanyId)
        Else
            departments = GetAll(Of DepartmentModel)("GetDepartments")
        End If
        Dim filteredDepartments = departments.Where(Function(dep) dep("name").ToString().ToLower().Contains(filterTextDepartment)).ToList()
        DepartmentListBox.ItemsSource = filteredDepartments
        If departments.Any() Then
            DepartmentListBox.Visibility = Visibility.Visible
        End If
    End Sub


    Private Sub PreviewKeyUpCompany(sender As Object, e As Input.KeyEventArgs)
        Dim filterTextCompany As String = CompanyTextInput.Text.ToLower()

        Dim companies As JArray = GetAll(Of CompanyModel)("GetCompanies")
        Dim filteredCompanies = companies.Where(Function(br) br("name").ToString().ToLower().Contains(filterTextCompany)).ToList()
        CompanyListBox.ItemsSource = filteredCompanies
        If companies.Any() Then
            CompanyListBox.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub PreviewKeyUpRole(sender As Object, e As Input.KeyEventArgs)
        Dim filterTextRole As String = RoleTextInput.Text.ToLower()

        Dim roles As JArray = GetAll(Of RoleModel)("GetRoles")
        Dim filteredRoles = roles.Where(Function(rol) rol("name").ToString().ToLower().Contains(filterTextRole)).ToList()
        RoleListBox.ItemsSource = filteredRoles
        If roles.Any() Then
            RoleListBox.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub DepartmentListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim selectedDepartment As JObject = TryCast(DepartmentListBox.SelectedItem, JObject)

        If selectedDepartment IsNot Nothing Then
            Dim departmentId As String = selectedDepartment("id")
            Dim departmentName As String = selectedDepartment("name")

            If departmentName IsNot Nothing Then
                DepartmentTextId.Text = departmentId
                DepartmentTextInput.Text = departmentName
                DepartmentListBox.Visibility = Visibility.Hidden
            End If
        End If
    End Sub
    Private _selectedCompanyId As String
    Private Sub CompanyListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim selectedCompany As JObject = TryCast(CompanyListBox.SelectedItem, JObject)

        If selectedCompany IsNot Nothing Then
            _selectedCompanyId = selectedCompany("id").ToString()
            Dim companyId As String = selectedCompany("id")
            Dim companyName As String = selectedCompany("name")

            If companyName IsNot Nothing Then
                CompanyTextId.Text = companyId
                CompanyTextInput.Text = companyName
                CompanyListBox.Visibility = Visibility.Hidden
            End If
        End If
    End Sub
    Private Sub RoleListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Dim selectedRole As JObject = TryCast(RoleListBox.SelectedItem, JObject)

        If selectedRole IsNot Nothing Then
            Dim roleId As String = selectedRole("id")
            Dim roleName As String = selectedRole("name")

            If roleName IsNot Nothing Then
                RoleTextId.Text = roleId
                RoleTextInput.Text = roleName
                RoleListBox.Visibility = Visibility.Hidden
            End If
        End If
    End Sub
    Private Sub DepartmentTextInput_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        PreviewKeyUpDepartment(sender, Nothing)
    End Sub
    Private Sub CompanyTextInput_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        PreviewKeyUpCompany(sender, Nothing)
    End Sub
    Private Sub RoleTextInput_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        PreviewKeyUpRole(sender, Nothing)
    End Sub

    Private Sub Close_Click_Department(sender As Object, e As RoutedEventArgs)
        DepartmentListBox.Visibility = Visibility.Collapsed
        DepartmentTextInput.Text = ""
    End Sub
    Private Sub Close_Click_Company(sender As Object, e As RoutedEventArgs)
        CompanyListBox.Visibility = Visibility.Collapsed
        CompanyTextInput.Text = ""
    End Sub
    Private Sub Close_Click_Role(sender As Object, e As RoutedEventArgs)
        RoleListBox.Visibility = Visibility.Collapsed
        RoleTextInput.Text = ""
    End Sub
    Private Sub DepartmentTextInput_TextChanged(sender As Object, e As TextChangedEventArgs) Handles DepartmentTextInput.TextChanged
    End Sub
    Private Sub CompanyTextInput_TextChanged(sender As Object, e As TextChangedEventArgs) Handles CompanyTextInput.TextChanged
    End Sub
    Private Sub RoleTextInput_TextChanged(sender As Object, e As TextChangedEventArgs) Handles RoleTextInput.TextChanged
    End Sub

End Class
