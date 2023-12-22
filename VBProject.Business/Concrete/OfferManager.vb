Imports System.Linq.Expressions
Imports System.Threading.Tasks
Imports DataAccess
Imports Entities
Imports VBProject.DataAccess.ERPProject.DataAccess.Abstract.DataManagement
Imports VBProject.Entity

Public Class OfferManager
    Implements IOfferService

    Private ReadOnly _uow As IUnitOfWork

    Public Sub New(uow As IUnitOfWork)
        _uow = uow
    End Sub

    Public Async Function AddAsync(Entity As Offer) As Task(Of Offer) Implements IGenericService(Of Offer).AddAsync
        Await _uow.OfferRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function

    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of Offer, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of Offer)) Implements IGenericService(Of Offer).GetAllAsync
        Return Await _uow.OfferRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Async Function GetAsync(Filter As Expression(Of Func(Of Offer, Boolean)), ParamArray IncludeProperties As String()) As Task(Of Offer) Implements IGenericService(Of Offer).GetAsync
        Return Await _uow.OfferRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function RemoveAsync(Entity As Offer) As Task Implements IGenericService(Of Offer).RemoveAsync
        Entity.IsActive = False
        Await _uow.OfferRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAsync(Entity As Offer) As Task Implements IGenericService(Of Offer).UpdateAsync
        Await _uow.OfferRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function UpdateAllAsync(Entity As Offer) As Task(Of ICollection(Of Offer)) Implements IOfferService.UpdateAllAsync
        Dim updateofferList = Await _uow.OfferRepository.GetAllAsync(Function(e) e.RequestId = Entity.RequestId)
        Entity.Status = 4
        Entity.IsActive = True
        Await _uow.OfferRepository.UpdateAsync(Entity)

        If updateofferList.ToList().Count = 1 Then
            Await _uow.SaveChangeAsync()
            Return updateofferList.ToList()
        End If

        For Each value In updateofferList
            If value.Id <> Entity.Id Then
                value.Status = 3
                Await _uow.OfferRepository.UpdateAsync(value)
            End If
        Next

        Await _uow.SaveChangeAsync()
        Return updateofferList.ToList()
        ' onay bekliyor 1
        ' kabul edildi 2
        ' reddedildi 3
        ' onaylandı 4
    End Function
End Class
