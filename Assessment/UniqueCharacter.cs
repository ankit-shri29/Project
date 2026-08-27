using System;

public class UniqueCharacters
{
    private const char FirstLetter = 'a';
    private const char LastLetter = 'z';

    public static bool HasAllUniqueCharacters(ReadOnlySpan<char> input)
    {
        if (input.IsEmpty) return true;

        int seenCharacters = 0;

        for (int i = 0; i < input.Length; i++)
        {
            char character = input[i];

            if (character >= 'A' && character <= 'Z')
            {
                character = (char)(character | 0x20);
            }

            if (character < FirstLetter || character > LastLetter)
            {
                continue;
            }

            int bitPosition = character - FirstLetter;
            int characterMask = 1 << bitPosition;

            if ((seenCharacters & characterMask) != 0)
            {
                return false;
            }

            seenCharacters |= characterMask;
        }

        return true;
    }

    public static void Main()
    {
        string[] testInputs =
        {
            "hello",
            "world",
            "Adam"
        };

        foreach (string input in testInputs)
        {
            bool result = HasAllUniqueCharacters(input);
            Console.WriteLine($"\"{input}\" -> {result}");
        }
    }
}