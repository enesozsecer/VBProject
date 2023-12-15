Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Public Class MainViewModel1
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Function SetProperty(Of T)(ByRef field As T, newValue As T, <CallerMemberName> Optional propertyName As String = Nothing) As Boolean
        If Not (Object.Equals(field, newValue)) Then
            field = newValue
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
            Return True
        End If

        Return False
    End Function
End Class
