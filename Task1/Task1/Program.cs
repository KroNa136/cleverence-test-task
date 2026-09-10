using Task1;

// Valid format (compression + decompression)

string originalString = "aaabbcccdde";
string compressedString = StringCompressor.Compress(originalString);
string decompressedString = StringCompressor.Decompress(compressedString);

Console.WriteLine($"Original string:\n{originalString}\n\nCompressed string:\n{compressedString}\n\nDecompressed string:\n{decompressedString}\n");
Console.WriteLine("---------------------------------------------\n");

// Invalid format (compression + decompression)

string invalidFormatString = "q0w1e0r1t0y1";

try
{
    string compressedInvalidFormatString = StringCompressor.Compress(invalidFormatString);
}
catch (Exception ex)
{
    OutputCompressingError(ex, invalidFormatString);
}

try
{
    string decompressedInvalidStringFormat = StringCompressor.Decompress(invalidFormatString);
}
catch (Exception ex)
{
    OutputDecompressingError(ex, invalidFormatString);
}

Console.WriteLine("---------------------------------------------\n");

// Integer overflow when decompressing a string

string integerOverflowString = "a999999999999999999";

try
{
    string decompressedIntegerOverflowString = StringCompressor.Decompress(integerOverflowString);
}
catch (Exception ex)
{
    OutputDecompressingError(ex, integerOverflowString);
}

static void OutputCompressingError(Exception ex, string processedString)
{
    Console.WriteLine($"Got an exception when trying to compress the following string: {processedString}\n{ex.GetType()}: {ex.Message}\n");
}

static void OutputDecompressingError(Exception ex, string processedString)
{
    Console.WriteLine($"Got an exception when trying to decompress the following string: {processedString}\n{ex.GetType()}: {ex.Message}\n");
}