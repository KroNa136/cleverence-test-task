using Task1;

namespace Tests;

public class StringCompressorTests
{
    [Fact]
    public void Compress_CorrectFormat_ReturnsCompressedString()
    {
        string originalString = "aaabbcccddeffggg";
        string compressedString = StringCompressor.Compress(originalString);
        Assert.Equal("a3b2c3d2ef2g3", compressedString);
    }

    [Fact]
    public void Compress_NotOnlyLowercaseLetters_ThrowsFormatException()
    {
        string originalString = "aAaBBCC";
        Assert.Throws<FormatException>(() => StringCompressor.Compress(originalString));
    }

    [Fact]
    public void Decompress_CorrectFormat_ReturnsDecompressedString()
    {
        string originalString = "a3b2c3d2ef2g3";
        string decompressedString = StringCompressor.Decompress(originalString);
        Assert.Equal("aaabbcccddeffggg", decompressedString);
    }

    [Fact]
    public void Decompress_ZeroCount_ThrowsFormatException()
    {
        string originalString = "a3b0";
        Assert.Throws<FormatException>(() => StringCompressor.Decompress(originalString));
    }

    [Fact]
    public void Decompress_OneCount_ThrowsFormatException()
    {
        string originalString = "a1b2";
        Assert.Throws<FormatException>(() => StringCompressor.Decompress(originalString));
    }

    [Fact]
    public void Decompress_IntegerOverflowCount_ThrowsOverflowException()
    {
        string originalString = "a999999999999";
        Assert.Throws<OverflowException>(() => StringCompressor.Decompress(originalString));
    }
}