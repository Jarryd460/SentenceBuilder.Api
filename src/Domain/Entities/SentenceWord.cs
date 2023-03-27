using Domain.Common;

namespace Domain.Entities;

public class SentenceWord : BaseAuditableEntity
{
    public int Id { get; set; }
    public required int Order { get; set; }
    public int SentenceId { get; set; }
    public required int WordId { get; set; }
    public virtual Sentence Sentence { get; set; }
    public virtual Word Word { get; set; }
}
