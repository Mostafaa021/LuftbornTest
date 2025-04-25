namespace Luftborn.Core.Abstraction.Domain;

public interface IAuditData
{
    string CreatorId { get; set; }
    string CreatorName { get; set; }
    DateTime? CreationDate { get; set; }
    string ModifierId { get; set; }
    string ModifierName { get; set; }
    DateTime? ModificationDate { get; set; }
}