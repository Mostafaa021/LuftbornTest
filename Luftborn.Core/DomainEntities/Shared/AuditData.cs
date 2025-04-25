using System.Text.Json.Serialization;
using Luftborn.Core.Abstraction.Domain;

namespace Luftborn.Core.DomainEntities.Shared;

public class AuditData : IAuditData // can be used as a base class for other entities for auditing purposes
{
    [JsonIgnore]
    public string CreatorId { get; set; }
    [JsonIgnore]
    public string CreatorName { get; set; }
    [JsonIgnore]
    public DateTime? CreationDate { get; set; }
    [JsonIgnore]
    public string ModifierId { get; set; }
    [JsonIgnore]
    public string ModifierName { get; set; }
    [JsonIgnore]
    public DateTime? ModificationDate { get; set; }
}