using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Sentences.Queries.GetSentences;

public class SentenceWordDto : IMapFrom<SentenceWord>
{
    /// <summary>
    /// Order position of word in sentence with 1 indicating the start
    /// </summary>
    /// <example>1</example>
    [Required]
    public int Order { get; set; }
    /// <summary>
    /// Word
    /// </summary>
    /// <example>You</example>
    [Required]
    public string Word { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SentenceWord, SentenceWordDto>()
            .ForMember(sentenceWordDto => sentenceWordDto.Word, configuration => configuration.MapFrom(sentenceWord => sentenceWord.Word.Value));
    }
}
