using System.Diagnostics.CodeAnalysis;

namespace Defra.PTS.Pet.Domain.Entities;

[ExcludeFromCodeCoverage]
public class PetDocumentEvidenceEntity
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }
    public string? EvidenceReference { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
