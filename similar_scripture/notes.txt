using System;
using System.Collections.Generic;
using System.IO; //reads files into an ary of strings and prints them. writting to not forget what this does.
using System.Runtime.CompilerServices; //reads files into an ary of strings and prints them. writting to not forget what this does.

//team scripture code
//looking at the team code we used to get template
/*  start minimum viable product 
Then have it hide a word and test that 
Then add the loop to hide multiple words 
Write one step at a time and test as you go. */

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Develop03 World!");
        //showed the book, chapter, verse, words
        // string book = "1 Nephi";  made the text we would store
        //int chapter = 1; made the variable
        //int verse = 1;
        //string text = "I, Nephi, having been born of goodly parents,"; storing the actual words of the scripture

        //Console.WriteLine($"{book} {chapter}:{verse}"); printing the book chapter verse
        // Console.WriteLine(text); printing the text
        /*
        //showed text in same spacing
        var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries); made an array of strings, split cut the verses into seperate pieces.
        Console.WriteLine(string.Join(" ", words)); // should match text spacing join glued all the words back together with spaces in between.
        //made words hide
        HideOneWord(words, 0);
        Console.WriteLine(string.Join(" ", words));// hides the words at index 0
        // hides words as well
        ShowVerse(book, chapter, verse, words);
        HideOneWord(words, 0); hides the words chapter verse book at index 0
        ShowVerse(book, chapter, verse, words);
        //works with random word deleter method
        var rng = new Random();
        int idx = PickVisibleIndex(words, rng);
        if (idx != -1) HideOneWord(words, idx);
        ShowVerse(book, chapter, verse, words);

        // while loop to pick random until pushed continue
        while (true) //keep looping forever until you break.
        {
            Console.Write("Press ENTER to hide a word (or type 'quit'): ");
            var input = Console.ReadLine();
            if (string.Equals(input, "quit", StringComparison.OrdinalIgnoreCase)) break;

            int idx2 = PickVisibleIndex(words, rng);
            if (idx2 == -1)
            {
                Console.WriteLine("All words hidden!");
                break;
            }
            HideOneWord(words, idx2);
            ShowVerse(book, chapter, verse, words);*/
        //uh this made them all repear not slowly disapear
        string path = "scriptures.txt"; //here the text is "scriptures.txt", which is the filename of the file we want to read.
        if (!File.Exists(path)) // path is just a variable name you called the file // if checks if the file exists
        {
            Console.WriteLine("Could not find " + path);// got a couple of these until I moved my scripture.txt to proper location
            return; // stopped the program from continuing on. 
        }

        // read the first valid scripture from file

        var rng = new Random();// rng = a random number generator. Like a dice roller.
// We'll use rng to pick random words to hide OR to pick a random verse from the file.
    
        Scripture s; //// This makes a variable named s, type Scripture (your class).
        // At this point it's empty — you will fill it later with an actual Scripture object.

        // pick ONE random verse from scriptures.txt
        try
        {
            s = LoadRandomFromFile("scriptures.txt", rng);
            // try = "try to do this code, but if something goes wrong, don't crash."
            // LoadRandomFromFile(...) = our helper function below.
            // It opens the file, loads all the lines, picks one scripture, and gives it back.
        }
        catch (Exception ex)
        {
    Console.WriteLine(ex.Message);
    // If something goes wrong (like file not found, or bad format),
    // print the error message. ex.Message is just the text of the error.
    return; // stop the program
        // interactive hide loop
        while (true)
        {
            Console.Clear();
                // Clear the console so it looks like the screen refreshes.

            Console.WriteLine(s.GetDisplay());
            // Ask your Scripture object s to show itself.
            // GetDisplay() calls Reference.Bookinfo() and Word.Present() under the hood.

                if (s.IsFullyHidden())
                {
                    Console.WriteLine("\nAll words hidden. Nice work!");
                    return;
                }
         // check if every Word inside Scripture is hidden.
         // If yes, print a "done" message and stop the program.


                Console.Write("\nPress Enter to hide 3 words, or type 'quit': ");
                string input = Console.ReadLine();
    // Ask the user for input. Waits until they hit Enter.
    // If they type nothing, input is empty. If they type quit, input = "quit".
                if (!string.IsNullOrWhiteSpace(input) &&
                input.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                     return;
                }
                // If the input is not empty AND equals "quit" (ignore spaces/capital letters),
                // then stop the program.

            
            if (!string.IsNullOrWhiteSpace(input) &&
                input.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

                s.HideRandomWords(3, rng);
                    // Otherwise, ask the Scripture to hide 3 random words.
                    // Scripture calls into its Word objects to change them to underscores.
}

        }

    }
        static Scripture LoadRandomFromFile(string path, Random rng)
        // static means this belongs to Program, not to an object.
        // It returns a Scripture object.
        // Takes 2 things: path (filename string), rng (random dice).

    {
       
        if (!File.Exists(path))
            throw new FileNotFoundException($"File not found: {path}");
        // Check if the file exists. If not, throw = stop running this function and send back an error.
        // $"File not found: {path}" makes a message like "File not found: scriptures.txt"


        var valid = new List<Scripture>();
        // Make a list to store all the good Scripture objects we find in the file.


        foreach (var raw in File.ReadAllLines(path)) //open the file and read every line into a list. foreach means go through each line. 
        {
            var line = raw.Trim(); //raw = original line, might have spaces at start or end. .Trim() = cut off extra spaces on both ends.
            if (line.Length == 0) continue; //.Length = how many characters are in the line. if there is a empty line skip it.
            //continue = jump back to the top of the foreach, don’t do the rest for this line.
            // Expect: Book|Chapter|Start|End|Text
            var parts = line.Split('|'); //.Split('|') = cut the line into chunks every time you see
            if (parts.Length != 5) continue; // checks if we got 5 parts 

            string book = parts[0].Trim(); //the first index book and trim if there is extra space
            if (!int.TryParse(parts[1], out int chapter)) continue;// chapter try to turn it into a number.” if fails skip
            if (!int.TryParse(parts[2], out int start)) continue; // verse try to print/parse a number/ or skp if its bad

            int end = start;
            int.TryParse(parts[3], out end); // if parse fails, end == start, this handles single-verse scriptures like 1:1

            string text = parts[4]; // the verse text 

            var reference = (start == end)
                ? new Reference(book, chapter, start) //this is a ternary operator (fancy if/else in one line). //still confused on this and its puropse. 
                //if start == end → build a Reference with 3 pieces (book, chapter, verse).
// else → build a Reference with 4 pieces (book, chapter, startVerse, endVerse).  this makes sure both “single verse” and “range of verses” are supported.
                : new Reference(book, chapter, start, end);

            valid.Add(new Scripture(reference, text));
            // Build a Scripture object with the reference + text.
            // Put it in the valid list.

        }

            if (valid.Count == 0)
                throw new InvalidOperationException("No valid scriptures found in file.");
            // If we never added anything to the list, throw an error.


            return valid[rng.Next(valid.Count)];
        // Pick a random index between 0 and list size-1.
    // Return that Scripture object.

    }

            

    

        // Works with hideoneword in main
    /*static void HideOneWord(string[] words, int index)
    {
        if (index < 0 || index >= words.Length) return;
        words[index] = new string('_', words[index].Length);
    }
    static void ShowVerse(string book, int chapter, int verse, string[] words)
    {
        Console.WriteLine($"{book} {chapter}:{verse}");
        Console.WriteLine(string.Join(" ", words));
    }*/
    //Method to help pick a random word disappear
    /* static int PickVisibleIndex(string[] words, Random rng)
     {
         // simple scan for any word not already underscores
         var candidates = new List<int>();
         for (int i = 0; i < words.Length; i++)
         {
             bool allUnderscores = true;
             foreach (char c in words[i]) if (c != '_') { allUnderscores = false; break; }
             if (!allUnderscores) candidates.Add(i);
         }
         if (candidates.Count == 0) return -1;
       //  return candidates[rng.Next(candidates.Count)];
    // }*/
}



