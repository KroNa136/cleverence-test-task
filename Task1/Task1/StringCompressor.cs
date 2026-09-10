using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task1;

/// <summary>
/// A compressor for strings containing only lowercase letters.
/// </summary>
public static class StringCompressor
{
    /// <summary>
    /// Compresses a string by replacing consecutive duplicate characters with a single character followed by the count of duplicates.
    /// </summary>
    /// <param name="str">The string to be compressed, which must contain only lowercase letters.</param>
    /// <returns>The compressed string.</returns>
    /// <exception cref="FormatException">Thrown when the provided string contains characters other than lowercase letters.</exception>
    public static string Compress(string str)
    {
        if (string.IsNullOrEmpty(str) || str.Length == 1)
            return str;

        if (!MatchesDecompressedFormat(str))
            throw new FormatException("The provided string must contain only lowercase letters.");

        StringBuilder sb = new();

        char currentChar = str[0];
        int currentCharCount = 1;

        void Append()
        {
            sb.Append(currentChar);

            if (currentCharCount > 1)
                sb.Append(currentCharCount);
        }

        for (int i = 1; i < str.Length; i++)
        {
            char nextChar = str[i];

            if (nextChar.Equals(currentChar))
            {
                currentCharCount++;
            }
            else
            {
                Append();

                currentChar = nextChar;
                currentCharCount = 1;
            }
        }

        Append();

        return sb.ToString();
    }

    /// <summary>
    /// Decompresses a string from a compressed format where characters are followed by their repeat counts.
    /// </summary>
    /// <param name="str">The compressed string to decompress.</param>
    /// <returns>The original, decompressed string.</returns>
    /// <exception cref="FormatException">Thrown when the input string does not match the expected compressed format.</exception>
    /// <exception cref="OverflowException">Thrown when one of the counts inside the compressed string exceeds the maximum integer value.</exception>
    public static string Decompress(string str)
    {
        if (string.IsNullOrEmpty(str) || str.Length == 1)
            return str;

        if (!MatchesCompressedFormat(str))
            throw new FormatException("The provided string must have a valid compressed format (e.g. ab2c3d4).");

        StringBuilder sb = new();

        char currentChar = str[0];
        StringBuilder currentCharCountSb = new();

        void Append() => sb.Append(currentChar, repeatCount: (currentCharCountSb.Length > 0) ? int.Parse(currentCharCountSb.ToString()) : 1);

        for (int i = 1; i < str.Length; i++)
        {
            char nextChar = str[i];

            if (char.IsDigit(nextChar))
            {
                currentCharCountSb.Append(nextChar);
            }
            else
            {
                Append();

                currentChar = nextChar;
                currentCharCountSb.Clear();
            }
        }

        Append();

        return sb.ToString();
    }

    private static bool MatchesDecompressedFormat(string str)
        => str.All(char.IsLower);

    private static bool MatchesCompressedFormat(string str)
    {
        if (!str.All(ch => char.IsLower(ch) || char.IsDigit(ch)))
            return false;

        if (str.Length < 1 || !char.IsLower(str[0]))
            return false;

        char currentChar = str[0];

        for (int i = 1; i < str.Length; i++)
        {
            char nextChar = str[i];

            if (char.IsLetter(currentChar) && (nextChar.Equals('0') || nextChar.Equals('1')))
                return false;
        }

        return true;
    }
}
