Imports Core
Imports Entities
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity

Public Class OfferRepository
    Inherits BaseRepository(Of Offer)
    Implements IOfferRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class
