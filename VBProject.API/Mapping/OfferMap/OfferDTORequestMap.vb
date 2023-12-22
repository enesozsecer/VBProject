Imports AutoMapper
Imports VBProject.Entity

Public Class OfferDTORequestMap
    Inherits Profile

    Public Sub New()
        CreateMap(Of Offer, OfferDTORequest)()
        CreateMap(Of OfferDTORequest, Offer)()
    End Sub
End Class
