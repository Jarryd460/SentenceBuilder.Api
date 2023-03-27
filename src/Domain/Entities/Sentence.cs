using Domain.Common;

namespace Domain.Entities;

public class Sentence : BaseAuditableEntity
{
    public virtual IList<SentenceWord> SentencesWords { get; set; }
}
