using BmoKids.Models;

namespace BmoKids.Services;

public class AssessmentService
{
    public List<LessonItem> GetEnglishLetters()
    {
        var letters = new List<LessonItem>();
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        foreach (var letter in alphabet)
        {
            letters.Add(new LessonItem
            {
                Value = letter.ToString(),
                DisplayText = letter.ToString(),
                AcceptedPronunciations = GetEnglishPronunciations(letter),
                TeachingText = $"This is the letter {letter}. Say {letter}.",
                Language = "en-US"
            });
        }

        return letters;
    }

    public List<LessonItem> GetArabicLetters()
    {
        var letters = new List<LessonItem>();
        string[] arabicLetters = { "أ", "ب", "ت", "ث", "ج", "ح", "خ", "د", "ذ", "ر", "ز",
                                  "س", "ش", "ص", "ض", "ط", "ظ", "ع", "غ", "ف", "ق",
                                  "ك", "ل", "م", "ن", "هـ", "و", "ي" };

        string[] arabicNames = { "ألف", "باء", "تاء", "ثاء", "جيم", "حاء", "خاء", "دال",
                                "ذال", "راء", "زاي", "سين", "شين", "صاد", "ضاد", "طاء",
                                "ظاء", "عين", "غين", "فاء", "قاف", "كاف", "لام", "ميم",
                                "نون", "هاء", "واو", "ياء" };

        for (int i = 0; i < arabicLetters.Length; i++)
        {
            letters.Add(new LessonItem
            {
                Value = arabicLetters[i],
                DisplayText = arabicLetters[i],
                AcceptedPronunciations = new List<string> { arabicLetters[i], arabicNames[i] },
                TeachingText = $"هذا الحرف {arabicLetters[i]}. قول معايا: {arabicNames[i]}",
                Language = "ar-EG"
            });
        }

        return letters;
    }

    public List<LessonItem> GetNumbers()
    {
        var numbers = new List<LessonItem>();

        for (int i = 1; i <= 20; i++)
        {
            numbers.Add(new LessonItem
            {
                Value = i.ToString(),
                DisplayText = i.ToString(),
                AcceptedPronunciations = GetNumberPronunciations(i),
                TeachingText = $"This is the number {i}. Say {i}.",
                Language = "en-US"
            });
        }

        return numbers;
    }

    private List<string> GetEnglishPronunciations(char letter)
    {
        return letter switch
        {
            'A' => new List<string> { "a", "ay", "eh", "ei", "ا", "إيه" },
            'B' => new List<string> { "b", "bee", "bi", "بي" },
            'C' => new List<string> { "c", "see", "si", "سي" },
            'D' => new List<string> { "d", "dee", "di", "دي" },
            'E' => new List<string> { "e", "ee", "i", "إي" },
            'F' => new List<string> { "f", "ef", "إف" },
            'G' => new List<string> { "g", "jee", "جي" },
            'H' => new List<string> { "h", "aych", "إتش" },
            'I' => new List<string> { "i", "eye", "ay", "آي" },
            'J' => new List<string> { "j", "jay", "جيه" },
            'K' => new List<string> { "k", "kay", "كيه" },
            'L' => new List<string> { "l", "el", "إل" },
            'M' => new List<string> { "m", "em", "إم" },
            'N' => new List<string> { "n", "en", "إن" },
            'O' => new List<string> { "o", "oh", "أو" },
            'P' => new List<string> { "p", "pee", "بي" },
            'Q' => new List<string> { "q", "cue", "كيو" },
            'R' => new List<string> { "r", "ar", "آر" },
            'S' => new List<string> { "s", "es", "إس" },
            'T' => new List<string> { "t", "tee", "تي" },
            'U' => new List<string> { "u", "you", "يو" },
            'V' => new List<string> { "v", "vee", "في" },
            'W' => new List<string> { "w", "double you", "دبليو" },
            'X' => new List<string> { "x", "ex", "إكس" },
            'Y' => new List<string> { "y", "why", "واي" },
            'Z' => new List<string> { "z", "zee", "zed", "زد" },
            _ => new List<string> { letter.ToString().ToLower() }
        };
    }

    private List<string> GetNumberPronunciations(int number)
    {
        var pronunciations = new List<string> { number.ToString() };

        string[] englishWords = { "zero", "one", "two", "three", "four", "five", "six", "seven",
                                 "eight", "nine", "ten", "eleven", "twelve", "thirteen",
                                 "fourteen", "fifteen", "sixteen", "seventeen", "eighteen",
                                 "nineteen", "twenty" };

        if (number >= 0 && number < englishWords.Length)
        {
            pronunciations.Add(englishWords[number]);
        }

        return pronunciations;
    }
}