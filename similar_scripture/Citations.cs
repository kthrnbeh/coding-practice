using System;

// This holds WHERE the scripture lives (book/chapter/verse).
public class Citation
{
    // Keep raw data private; expose only what you choose (encapsulation).
    private string _book;
    private int _chapter;
    private int _verse;

    // Constructor: runs when you "new" up a Citation.
    public Citation(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
    }

    // Getters (read-only access). No setters yet if you don’t want changes later.
    public string GetBook()   { return _book; }
    public int GetChapter()   { return _chapter; }
    public int GetVerse()     { return _verse; }

    // A helper to show "Moses 1:1" format.
    public string Format()
    {
        return _book + " " + _chapter + ":" + _verse;
    }
}
