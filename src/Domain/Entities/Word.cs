using Domain.Common;

namespace Domain.Entities;

public class Word : BaseAuditableEntity
{
    public required string Value { get; set; }
    public required int WordTypeId { get; set; }
    public virtual WordType WordType { get; set; }
    public virtual IList<SentenceWord> SentencesWords { get; set; }
}
