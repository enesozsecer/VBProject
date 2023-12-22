Imports Entities
Imports VBProject.Entity

Public Interface IOfferService
    Inherits IGenericService(Of Offer)

    Function UpdateAllAsync(Entity As Offer) As Task(Of ICollection(Of Offer))
End Interface
