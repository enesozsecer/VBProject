﻿Imports System.Net.Http
Imports System.Text
Imports Microsoft.AspNetCore.Http
Imports Microsoft.AspNetCore.Mvc
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class BaseFuncs

    Public Shared Function GetOne(Of T)(link As String) As T
        Dim apiUrl As String = $"https://localhost:62437/{link}"

        Using client As New HttpClient()
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Application.Current.Properties("Token"))

            Dim response = client.GetAsync(apiUrl).Result
            If response.IsSuccessStatusCode Then
                Dim jsonData = response.Content.ReadAsStringAsync().Result
                Dim val = JsonConvert.DeserializeObject(Of T)(jsonData)
                client.DefaultRequestHeaders.Remove("Authorization")
                Return val
            Else
                ' Başarısız durumda uygun bir şey yapın, örneğin loglama
                Return Nothing
            End If
        End Using
    End Function
    Public Shared Function GetAll(Of T)(link As String) As JArray
        Dim apiUrl As String = $"https://localhost:62437/{link}"
        Using client As New HttpClient()
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Application.Current.Properties("Token"))
            Dim response = client.GetAsync(apiUrl).Result
            If response.IsSuccessStatusCode Then
                Dim jsonString = response.Content.ReadAsStringAsync().Result
                Dim values As JArray = JArray.Parse(jsonString)
                client.DefaultRequestHeaders.Remove("Authorization")
                Return values
            End If
        End Using
    End Function
    Public Shared Function Update(Of T)(p As T, link As String) As Boolean
        Dim client As New HttpClient()
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Application.Current.Properties("Token"))
        Dim jsonData As String = JsonConvert.SerializeObject(p)
        Dim stringContent As New StringContent(jsonData, Encoding.UTF8, "application/json")
        Dim response = client.PostAsync($"https://localhost:62437/{link}", stringContent).Result
        client.DefaultRequestHeaders.Remove("Authorization")
        Return response.StatusCode
    End Function
    Public Shared Function Delete(link As String) As Boolean
        Dim client As New HttpClient()
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Application.Current.Properties("Token"))
        Dim response = client.DeleteAsync($"https://localhost:62437/{link}").Result
        client.DefaultRequestHeaders.Remove("Authorization")
        Return response.StatusCode
    End Function
    Public Shared Function Add(Of T)(link As String, p As T) As Boolean
        Using client As New HttpClient()
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " & Application.Current.Properties("Token"))
            Dim jsonData As String = JsonConvert.SerializeObject(p)
            Dim content As New StringContent(jsonData, Encoding.UTF8, "application/json")
            Dim result = client.PostAsync($"https://localhost:62437/{link}", content).Result
            client.DefaultRequestHeaders.Remove("Authorization")
            Return result.StatusCode
        End Using
    End Function
End Class
