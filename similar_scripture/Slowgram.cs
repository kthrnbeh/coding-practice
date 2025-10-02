using System;
using System.Collections.Generic;
using System.IO;

class Slowgram
{
    // NOTE: This is a static function (belongs to the class itself, not an object).
    // Why static here? Because "starting the app" is not about a specific scripture;
    // it's a one-time "do the setup" action.
    static void Main(string[] args)
    {
        Console.WriteLine("Scripture Memorizer (learning mode)");

        // 1) Decide where your text lives (file name should match your project’s location).
        // Keep variables local here (scope: this block) because no one else needs them later.
        string vaultPath = "scriptures.txt"; 

        // 2) Make a container for many passages (list of custom types).
        List<Passage> library = new List<Passage>();

        // 3) If the file exists, read every line and build objects.
        if (File.Exists(vaultPath))
        {
            // Each line might look like: Book|Chapter|Start|End|Text
            // (Your class can ignore end if you don’t need it yet.)
            foreach (string raw in File.ReadAllLines(vaultPath))
            {
                string line = raw.Trim();
                if (line.Length == 0) continue; // empty line guard

                // Split pieces – NO advanced stuff, just Split.
                string[] bits = line.Split('|');
                // Defensive checks keep beginners safe:
                if (bits.Length < 5) continue;

                // 4) Build the "address" object first (objects within objects).
                // Citation = like Reference (book, chapter, verse).
                string book = bits[0];
                int chapter = int.Parse(bits[1]);
                int startVerse = int.Parse(bits[2]);
                // int endVerse = int.Parse(bits[3]); // optional right now

                Citation address = new Citation(book, chapter, startVerse);

                // 5) Build the passage with its text. The Passage will split into Tokens.
                string text = bits[4];
                Passage p = new Passage(address, text);

                library.Add(p);
            }
        }

        // 6) For now, just take the first item if we have any.
        if (library.Count > 0)
        {
            Passage current = library[0];

            // Show it once fully visible.
            current.ShowLine();

            // Now loop: after each Enter key, hide a few more words.
            Console.WriteLine("\nPress Enter to hide more, or type 'quit' to stop.");
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "quit") break;

                // This method lives on the Passage (method vs. function).
                current.MaskSome(3);  // hide about 3 tokens each time (simple rule)
                current.ShowLine();

                if (current.AllHidden())
                {
                    Console.WriteLine("All words are hidden. Great job!");
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine("No passages found. Check your file path and format.");
        }
    }
}
