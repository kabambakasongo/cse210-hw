using System.Text;

class Program
{
    // Creativity: choose a random scripture from a built-in library, then hide only
    // words that are still visible so each round makes progress toward memorization.
    private const int WordsToHidePerRound = 3;

    private static readonly List<Scripture> Scriptures =
    [
        new Scripture(
            new Reference("John", 3, 16),
            "For God so loved the world, that he gave his only begotten Son, " +
            "that whosoever believeth in him should not perish, but have everlasting life."),
        new Scripture(
            new Reference("Proverbs", 3, 5, 6),
            "Trust in the Lord with all thine heart; and lean not unto thine own understanding. " +
            "In all thy ways acknowledge him, and he shall direct thy paths."),
        new Scripture(
            new Reference("Psalm", 23, 1),
            "The Lord is my shepherd; I shall not want.")
    ];

    static void Main(string[] args)
    {
        Scripture scripture = Scriptures[Random.Shared.Next(Scriptures.Count)];

        while (true)
        {
            ClearConsole();
            Console.WriteLine(scripture.GetDisplayText());

            if (scripture.IsCompletelyHidden)
            {
                break;
            }

            Console.WriteLine();
            Console.Write("Press enter to hide words, or type quit to exit: ");
            string response = Console.ReadLine() ?? string.Empty;

            if (response.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            scripture.HideRandomWords(WordsToHidePerRound);
        }
    }

    private static void ClearConsole()
    {
        if (!Console.IsOutputRedirected)
        {
            Console.Clear();
        }
    }
}

class Scripture
{
    private readonly Reference _reference;
    private readonly List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference ?? throw new ArgumentNullException(nameof(reference));
        ArgumentException.ThrowIfNullOrWhiteSpace(text);
        _words = text
            .Split((char[])null, StringSplitOptions.RemoveEmptyEntries)
            .Select(word => new Word(word))
            .ToList();
    }

    public bool IsCompletelyHidden => _words.All(word => word.IsHidden);

    public string GetDisplayText()
    {
        return $"{_reference} {string.Join(" ", _words)}";
    }

    public void HideRandomWords(int count)
    {
        List<Word> visibleWords = _words.Where(word => !word.IsHidden).ToList();
        int wordsToHide = Math.Min(count, visibleWords.Count);

        for (int i = 0; i < wordsToHide; i++)
        {
            int randomIndex = Random.Shared.Next(visibleWords.Count);
            visibleWords[randomIndex].Hide();
            visibleWords.RemoveAt(randomIndex);
        }
    }
}

class Reference
{
    private readonly string _book;
    private readonly int _chapter;
    private readonly int _startVerse;
    private readonly int? _endVerse;

    public Reference(string book, int chapter, int verse)
        : this(book, chapter, verse, null)
    {
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
        : this(book, chapter, startVerse, (int?)endVerse)
    {
    }

    private Reference(string book, int chapter, int startVerse, int? endVerse)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(book);

        if (chapter < 1 || startVerse < 1 || (endVerse.HasValue && endVerse.Value < startVerse))
        {
            throw new ArgumentOutOfRangeException(nameof(startVerse), "Chapter and verses must be positive, and the end verse cannot precede the start verse.");
        }

        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    public override string ToString()
    {
        string verseRange = _endVerse.HasValue
            ? $"{_startVerse}-{_endVerse.Value}"
            : _startVerse.ToString();

        return $"{_book} {_chapter}:{verseRange}";
    }
}

class Word
{
    private readonly string _text;

    public Word(string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);
        _text = text;
    }

    public bool IsHidden { get; private set; }

    public void Hide()
    {
        IsHidden = true;
    }

    public override string ToString()
    {
        if (!IsHidden)
        {
            return _text;
        }

        StringBuilder hiddenText = new(_text.Length);
        foreach (char character in _text)
        {
            hiddenText.Append(char.IsLetter(character) ? '_' : character);
        }

        return hiddenText.ToString();
    }
}
