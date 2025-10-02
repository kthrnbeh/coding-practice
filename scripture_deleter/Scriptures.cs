using System.Collections.Generic;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string fullText)
    {
        _reference = reference;
        _words = new List<Word>();

        string[] pieces = fullText.Split(' ');
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].Length > 0)
                _words.Add(new Word(pieces[i]));
        }
    }

    public string GetDisplay()
    {
        var parts = new List<string>();
        foreach (var w in _words)
            parts.Add(w.Present());

        return _reference.Bookinfo() + "\n" + string.Join(" ", parts);
    }

    public void HideRandomWords(int count, System.Random rng)
    {
        if (_words.Count == 0) return;

        for (int i = 0; i < count; i++)
        {
            int idx = rng.Next(0, _words.Count);
            _words[idx].Hide();
        }
    }

    public bool IsFullyHidden()
    {
        for (int i = 0; i < _words.Count; i++)
        {
            if (! _words[i].IsHidden())
                return false;
        }
        return true;
    }
}
