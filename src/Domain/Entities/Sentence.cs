using Domain.Common;

namespace Domain.Entities;

public class Sentence : BaseAuditableEntity
{
    public virtual ICollection<SentenceWord> SentencesWords { get; set; }
}
