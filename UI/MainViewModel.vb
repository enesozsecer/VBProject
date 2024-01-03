Imports System.ComponentModel

Public Class MainViewModel
    Implements INotifyPropertyChanged

    Private _categoryNameText As String
    Public Property CategoryNameText As String
        Get
            Return _categoryNameText
        End Get
        Set(value As String)
            If _categoryNameText <> value Then
                _categoryNameText = value
                OnPropertyChanged("CategoryNameText")
            End If
        End Set
    End Property

    Private _categoryIdText As String
    Public Property CategoryIdText As String
        Get
            Return _categoryIdText
        End Get
        Set(value As String)
            If _categoryIdText <> value Then
                _categoryIdText = value
                OnPropertyChanged("CategoryIdText")
            End If
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
End Class
