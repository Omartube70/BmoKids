using BmoKids.Models;

namespace BmoKids.Services;

public class SpeechService
{
    // تقييم الإجابة بناءً على النطق والبدائل
    public bool EvaluateAnswer(RecognitionResult result, LessonItem lessonItem, int childAge)
    {
        // ضبط thresholds حسب العمر
        double minConfidence = childAge <= 5 ? 0.28 : (childAge <= 8 ? 0.35 : 0.45);
        double minLevenshteinRatio = 0.35;

        // فحص النتيجة الرئيسية أولاً
        if (result.Confidence >= minConfidence &&
            CheckDirectMatch(result.Transcript, lessonItem.Value, lessonItem.AcceptedPronunciations))
        {
            return true;
        }

        // فحص البدائل
        foreach (var alt in result.Alternatives.Take(5))
        {
            if (alt.Confidence >= minConfidence &&
                CheckDirectMatch(alt.Transcript, lessonItem.Value, lessonItem.AcceptedPronunciations))
            {
                return true;
            }

            // فحص Levenshtein distance
            if (alt.Confidence >= 0.35)
            {
                foreach (var pronunciation in lessonItem.AcceptedPronunciations)
                {
                    var distance = CalculateLevenshteinDistance(
                        CleanText(alt.Transcript),
                        CleanText(pronunciation)
                    );

                    var maxLen = Math.Max(alt.Transcript.Length, pronunciation.Length);
                    if (maxLen > 0 && (double)distance / maxLen <= minLevenshteinRatio)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private bool CheckDirectMatch(string transcript, string value, List<string> acceptedPronunciations)
    {
        var cleanTranscript = CleanText(transcript);

        // مقارنة مباشرة مع القيمة
        if (cleanTranscript.Equals(CleanText(value), StringComparison.OrdinalIgnoreCase))
            return true;

        // مقارنة مع النطق المقبول
        foreach (var pronunciation in acceptedPronunciations)
        {
            if (cleanTranscript.Equals(CleanText(pronunciation), StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }

    private string CleanText(string text)
    {
        // إزالة المسافات والأحرف الخاصة
        return text.Trim()
            .Replace("أنا اسمي", "")
            .Replace("اسمي", "")
            .Replace("my name is", "")
            .ToLower();
    }

    public int CalculateLevenshteinDistance(string source, string target)
    {
        if (string.IsNullOrEmpty(source))
            return target?.Length ?? 0;

        if (string.IsNullOrEmpty(target))
            return source.Length;

        int[,] distance = new int[source.Length + 1, target.Length + 1];

        for (int i = 0; i <= source.Length; i++)
            distance[i, 0] = i;

        for (int j = 0; j <= target.Length; j++)
            distance[0, j] = j;

        for (int i = 1; i <= source.Length; i++)
        {
            for (int j = 1; j <= target.Length; j++)
            {
                int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                distance[i, j] = Math.Min(
                    Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                    distance[i - 1, j - 1] + cost
                );
            }
        }

        return distance[source.Length, target.Length];
    }

    // استخراج الاسم من النص
    public string ExtractName(string transcript)
    {
        var cleaned = CleanText(transcript);
        var words = cleaned.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        // أخذ أول كلمة أو آخر كلمة بعد التنظيف
        return words.Length > 0 ? words[^1] : transcript;
    }
}