using System;
using System.Collections.Generic;

// A whole verse: holds a Citation and a list of Tokens (words).
public class Passage
{
    private Citation _where;        // object within object
    private List<Token> _tokens;    // list of custom type

    public Passage(Citation where, string rawText)
    {
        _where = where;
        _tokens = new List<Token>();

        // Split incoming text into words and wrap each word in a Token.
        string[] parts = rawText.Split(' ');
        foreach (string piece in parts)
        {
            if (piece.Trim().Length == 0) continue;
            _tokens.Add(new Token(piece));
        }
    }

    // Show the "Moses 1:1 — words words words"
    public void ShowLine()
    {
        Console.WriteLine(_where.Format());
        Console.WriteLine();

        // Build the visible line by asking each Token how it wants to show itself.
        foreach (Token t in _tokens)
        {
            Console.Write(t.Display() + " ");
        }
        Console.WriteLine();
    }

    // Hide a few not-yet-hidden tokens (very simple rule).
    public void MaskSome(int count)
    {
        int hiddenThisRound = 0;

        foreach (Token t in _tokens)
        {
            if (!t.IsHidden())
            {
                t.Hide();
                hiddenThisRound++;
                if (hiddenThisRound == count) break;
            }
        }
    }

    // Check if we’re done.
    public bool AllHidden()
    {
        foreach (Token t in _tokens)
        {
            if (!t.IsHidden()) return false;
        }
        return true;
    }
}
