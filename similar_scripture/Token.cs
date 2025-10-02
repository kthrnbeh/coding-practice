using System;

// A single word that can be shown or hidden.
public class Token
{
    private string _text;      // the real word
    private bool _masked;      // whether it's hidden or not

    public Token(string text)
    {
        _text = text;
        _masked = false;       // start visible
    }

    // Behavior: change state
    public void Hide()  { _masked = true;  }
    public void Show()  { _masked = false; }

    // Read-only query: is it hidden?
    public bool IsHidden() { return _masked; }

    // How to display: either underscores or the word.
    public string Display()
    {
        if (_masked) return "_____";
        return _text;
    }

    // (Optional) Getter if you want the raw text for testing.
    public string GetText() { return _text; }
}
