namespace BmoKids.Models;

public class ChildData
{
    public string Name { get; set; } = "";
    public int Sessions { get; set; }
    public int Correct { get; set; }
    public int Wrong { get; set; }
    public int Interactions { get; set; }
    public List<LearningEvent> Events { get; set; } = new();
    public ChildSettings Settings { get; set; } = new();
}

public class ChildSettings
{
    public int Age { get; set; } = 6;
    public string LanguagePreference { get; set; } = "ar-EG";
}

public class LearningEvent
{
    public string Type { get; set; } = "";
    public string? Value { get; set; }
    public string? Item { get; set; }
    public bool? Correct { get; set; }
    public double? Confidence { get; set; }
    public int? AttemptIndex { get; set; }
    public long Timestamp { get; set; }
}

public class RecognitionResult
{
    public string Transcript { get; set; } = "";
    public double Confidence { get; set; }
    public List<RecognitionAlternative> Alternatives { get; set; } = new();
}

public class RecognitionAlternative
{
    public string Transcript { get; set; } = "";
    public double Confidence { get; set; }
}

public enum MouthState
{
    Neutral,
    Closed,
    Half,
    Open,
    Wide,
    Sad
}

public enum LearningSection
{
    None,
    English,
    Arabic,
    Sports,
    Chat
}

public class LessonItem
{
    public string Value { get; set; } = "";
    public string DisplayText { get; set; } = "";
    public List<string> AcceptedPronunciations { get; set; } = new();
    public string TeachingText { get; set; } = "";
    public string Language { get; set; } = "ar-EG";
}