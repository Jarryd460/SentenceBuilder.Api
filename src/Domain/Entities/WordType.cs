using Domain.Common;

namespace Domain.Entities;

public class WordType : BaseAuditableEntity
{
    public required string Value { get; set; }
    public virtual ICollection<Word> Words { get; set; }
}
