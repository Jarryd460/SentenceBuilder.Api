using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlite())
            {
                await _context.Database.MigrateAsync().ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole("Administrator");

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole).ConfigureAwait(false);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "JarrydDeane", Email = "jarryd.deane@gmail.com" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!").ConfigureAwait(false);
            await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name }).ConfigureAwait(false);
        }

        if (!_context.WordTypes.Any())
        {
            await SeedWordTypes().ConfigureAwait(false);

            await SeedNounWords().ConfigureAwait(false);
            await SeedVerbWords().ConfigureAwait(false);
            await SeedAdjectiveWords().ConfigureAwait(false);
            await SeedAdverbWords().ConfigureAwait(false);
            await SeedPronounWords().ConfigureAwait(false);
            await SeedPrepositionWords().ConfigureAwait(false);
            await SeedConjunctionWords().ConfigureAwait(false);
            await SeedDeterminerWords().ConfigureAwait(false);
            await SeedExclamationWords().ConfigureAwait(false);

            await SeedSentences().ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }

    private async Task SeedSentences()
    {
        await _context.Sentences.AddRangeAsync(
            new Sentence()
            {
                SentencesWords = new List<SentenceWord>()
                {
                    new SentenceWord()
                    {
                        Order = 1,
                        SentenceId = 1,
                        WordId = 164
                    },
                    new SentenceWord()
                    {
                        Order = 2,
                        SentenceId = 1,
                        WordId = 107
                    },
                    new SentenceWord()
                    {
                        Order = 3,
                        SentenceId = 1,
                        WordId = 48
                    },
                    new SentenceWord()
                    {
                        Order = 4,
                        SentenceId = 1,
                        WordId = 179
                    },
                    new SentenceWord()
                    {
                        Order = 5,
                        SentenceId = 1,
                        WordId = 49
                    },
                    new SentenceWord()
                    {
                        Order = 6,
                        SentenceId = 1,
                        WordId = 50
                    },
                    new SentenceWord()
                    {
                        Order = 7,
                        SentenceId = 1,
                        WordId = 79
                    },
                    new SentenceWord()
                    {
                        Order = 8,
                        SentenceId = 1,
                        WordId = 129
                    },
                    new SentenceWord()
                    {
                        Order = 9,
                        SentenceId = 1,
                        WordId = 51
                    },
                    new SentenceWord()
                    {
                        Order = 10,
                        SentenceId = 1,
                        WordId = 113
                    },
                    new SentenceWord()
                    {
                        Order = 11,
                        SentenceId = 1,
                        WordId = 52
                    }
                }
            },
            new Sentence()
            {
                SentencesWords = new List<SentenceWord>()
                {
                    new SentenceWord()
                    {
                        Order = 1,
                        SentenceId = 2,
                        WordId = 109
                    },
                    new SentenceWord()
                    {
                        Order = 2,
                        SentenceId = 2,
                        WordId = 91
                    },
                    new SentenceWord()
                    {
                        Order = 3,
                        SentenceId = 2,
                        WordId = 55
                    },
                    new SentenceWord()
                    {
                        Order = 4,
                        SentenceId = 2,
                        WordId = 168
                    },
                    new SentenceWord()
                    {
                        Order = 5,
                        SentenceId = 2,
                        WordId = 22
                    },
                    new SentenceWord()
                    {
                        Order = 6,
                        SentenceId = 2,
                        WordId = 127
                    },
                    new SentenceWord()
                    {
                        Order = 7,
                        SentenceId = 2,
                        WordId = 168
                    },
                    new SentenceWord()
                    {
                        Order = 8,
                        SentenceId = 2,
                        WordId = 13
                    }
                }
            },
            new Sentence()
            {
                SentencesWords = new List<SentenceWord>()
                {
                    new SentenceWord()
                    {
                        Order = 1,
                        SentenceId = 3,
                        WordId = 167
                    },
                    new SentenceWord()
                    {
                        Order = 2,
                        SentenceId = 3,
                        WordId = 168
                    },
                    new SentenceWord()
                    {
                        Order = 3,
                        SentenceId = 3,
                        WordId = 80
                    },
                    new SentenceWord()
                    {
                        Order = 4,
                        SentenceId = 3,
                        WordId = 146
                    },
                    new SentenceWord()
                    {
                        Order = 5,
                        SentenceId = 3,
                        WordId = 81
                    },
                    new SentenceWord()
                    {
                        Order = 6,
                        SentenceId = 3,
                        WordId = 23
                    },
                    new SentenceWord()
                    {
                        Order = 7,
                        SentenceId = 3,
                        WordId = 112
                    },
                    new SentenceWord()
                    {
                        Order = 8,
                        SentenceId = 3,
                        WordId = 105
                    },
                    new SentenceWord()
                    {
                        Order = 9,
                        SentenceId = 3,
                        WordId = 57
                    },
                    new SentenceWord()
                    {
                        Order = 10,
                        SentenceId = 3,
                        WordId = 128
                    },
                    new SentenceWord()
                    {
                        Order = 11,
                        SentenceId = 3,
                        WordId = 184
                    },
                    new SentenceWord()
                    {
                        Order = 12,
                        SentenceId = 3,
                        WordId = 24
                    },
                    new SentenceWord()
                    {
                        Order = 13,
                        SentenceId = 3,
                        WordId = 82
                    },
                    new SentenceWord()
                    {
                        Order = 14,
                        SentenceId = 3,
                        WordId = 147
                    },
                    new SentenceWord()
                    {
                        Order = 15,
                        SentenceId = 3,
                        WordId = 83
                    }
                }
            },
            new Sentence()
            {
                SentencesWords = new List<SentenceWord>()
                {
                    new SentenceWord()
                    {
                        Order = 1,
                        SentenceId = 4,
                        WordId = 168
                    },
                    new SentenceWord()
                    {
                        Order = 2,
                        SentenceId = 4,
                        WordId = 164
                    },
                    new SentenceWord()
                    {
                        Order = 3,
                        SentenceId = 4,
                        WordId = 84
                    },
                    new SentenceWord()
                    {
                        Order = 4,
                        SentenceId = 4,
                        WordId = 25
                    },
                    new SentenceWord()
                    {
                        Order = 5,
                        SentenceId = 4,
                        WordId = 134
                    },
                    new SentenceWord()
                    {
                        Order = 6,
                        SentenceId = 4,
                        WordId = 168
                    },
                    new SentenceWord()
                    {
                        Order = 7,
                        SentenceId = 4,
                        WordId = 26
                    },
                    new SentenceWord()
                    {
                        Order = 8,
                        SentenceId = 4,
                        WordId = 58
                    },
                    new SentenceWord()
                    {
                        Order = 9,
                        SentenceId = 4,
                        WordId = 126
                    },
                    new SentenceWord()
                    {
                        Order = 10,
                        SentenceId = 4,
                        WordId = 168
                    },
                    new SentenceWord()
                    {
                        Order = 11,
                        SentenceId = 4,
                        WordId = 27
                    }
                }
            }
        ).ConfigureAwait(false);
    }

    private async Task SeedWordTypes()
    {
        await _context.WordTypes.AddRangeAsync(
            new WordType()
            {
                Value = "Noun"
            },
            new WordType()
            {
                Value = "Verb"
            },
            new WordType()
            {
                Value = "Adjective"
            },
            new WordType()
            {
                Value = "Adverb"
            },
            new WordType()
            {
                Value = "Pronoun"
            },
            new WordType()
            {
                Value = "Preposition"
            },
            new WordType()
            {
                Value = "Conjunction"
            },
            new WordType()
            {
                Value = "Determiner"
            },
            new WordType()
            {
                Value = "Exclamation"
            }
        ).ConfigureAwait(false);
    }

    private async Task SeedNounWords()
    {
        await _context.Words.AddRangeAsync(
            new Word()
            {
                Value = "Book",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Car",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Dog",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Computer",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Tree",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "House",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Chair",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Cat",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Phone",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Sun",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Food",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Bird",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Table",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Music",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Water",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Door",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Mountain",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Shoe",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Bike",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Camera",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Deadline",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Vase",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Journey",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Destination",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Leaves",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Trees",
                WordTypeId = (int)WordTypeEnum.Noun
            },
            new Word()
            {
                Value = "Wind",
                WordTypeId = (int)WordTypeEnum.Noun
            }
        ).ConfigureAwait(false);
    }

    private async Task SeedVerbWords()
    {
        await _context.Words.AddRangeAsync(
            new Word()
            {
                Value = "Run",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Jump",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Sing",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Dance",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Write",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Read",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Eat",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Sleep",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Laugh",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Cry",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Play",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Swim",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Talk",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Drive",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Fly",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Cook",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Think",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Walk",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Climb",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Study",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Finished",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Work",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Be",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Let",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Know",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Forget",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Miss",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Placed",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Hoping",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Arrived",
                WordTypeId = (int)WordTypeEnum.Verb
            },
            new Word()
            {
                Value = "Danced",
                WordTypeId = (int)WordTypeEnum.Verb
            }
        ).ConfigureAwait(false);
    }

    private async Task SeedAdjectiveWords()
    {
        await _context.Words.AddRangeAsync(
            new Word()
            {
                Value = "Happy",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Sad",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Beautiful",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Ugly",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Tall",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Short",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Smart",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Dumb",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Brave",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Cowardly",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Bright",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Dark",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Loud",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Quiet",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Fast",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Slow",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Cold",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Hot",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Kind",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Mean",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Sure",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Long",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Difficult",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Tired",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Relieved",
                WordTypeId = (int)WordTypeEnum.Adjective
            },
            new Word()
            {
                Value = "Colorful",
                WordTypeId = (int)WordTypeEnum.Adjective
            }
        ).ConfigureAwait(false);
    }

    private async Task SeedAdverbWords()
    {
        await _context.Words.AddRangeAsync(
            new Word()
            {
                Value = "Quickly",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Slowly",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Happily",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Sadly",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Quietly",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Loudly",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Carefully",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Carelessly",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Beautifully",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Deliberately",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Gracefully",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Awkwardly",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Briskly",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Calmly",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Steadily",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Crazily",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Eagerly",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Lazily",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Recently",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Yesterday",
                WordTypeId = (int)WordTypeEnum.Adverb
            },
            new Word()
            {
                Value = "Finally",
                WordTypeId = (int)WordTypeEnum.Adverb
            }
        );
    }

    private async Task SeedPronounWords()
    {
        await _context.Words.AddRangeAsync(
            new Word()
            {
                Value = "I",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "You",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "He",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "She",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "It",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "We",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "They",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "Me",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "Him",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "Her",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "Us",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "Them",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "Myself",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "Yourself",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "Himself",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "Herself",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "Itself",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "Ourselves",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "Themselves",
                WordTypeId = (int)WordTypeEnum.Pronoun
            },
            new Word()
            {
                Value = "Who",
                WordTypeId = (int)WordTypeEnum.Pronoun
            }
        ).ConfigureAwait(false);
    }

    private async Task SeedPrepositionWords()
    {
        await _context.Words.AddRangeAsync(
            new Word()
            {
                Value = "In",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "On",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "At",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "To",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "From",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "With",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "Without",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "By",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "Of",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "About",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "Above",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "Below",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "Under",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "Over",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "Across",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "Through",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "Among",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "Between",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "Around",
                WordTypeId = (int)WordTypeEnum.Preposition
            },
            new Word()
            {
                Value = "Against",
                WordTypeId = (int)WordTypeEnum.Preposition
            }
        ).ConfigureAwait(false);
    }

    private async Task SeedConjunctionWords()
    {
        await _context.Words.AddRangeAsync(
            new Word()
            {
                Value = "And",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "But",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Or",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Nor",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Yet",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "So",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "For",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Because",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Although",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Though",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "While",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Whereas",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Since",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Unless",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "If",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Even If",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Even Though",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Whether",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Once",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "As If",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "Lest",
                WordTypeId = (int)WordTypeEnum.Conjunction
            },
            new Word()
            {
                Value = "After",
                WordTypeId = (int)WordTypeEnum.Conjunction
            }
        ).ConfigureAwait(false);
    }

    private async Task SeedDeterminerWords()
    {
        await _context.Words.AddRangeAsync(
            new Word()
            {
                Value = "The",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "A",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "An",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "Some",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "Any",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "No",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "This",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "That",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "These",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "Those",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "My",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "Your",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "His",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "Most",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "Its",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "Our",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "Their",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "Many",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "Few",
                WordTypeId = (int)WordTypeEnum.Determiner
            },
            new Word()
            {
                Value = "Several",
                WordTypeId = (int)WordTypeEnum.Determiner
            }
        ).ConfigureAwait(false);
    }

    private async Task SeedExclamationWords()
    {
        await _context.Words.AddRangeAsync(
            new Word()
            {
                Value = "Wow!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Oh!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Hey!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Hooray!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Yay!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Alas!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Hurruh!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Oops!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Ah!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Bravo!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Phew!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Eek!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Ouch!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Aha!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Bah!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Bingo!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Cheers!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Dang!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Gosh!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            },
            new Word()
            {
                Value = "Hallelujah!",
                WordTypeId = (int)WordTypeEnum.Exclamation
            }
        ).ConfigureAwait(false);
    }
}
