namespace CsBuilder;

internal static class IndentedTextWriterExtension
{
    internal static void GoOneLevelInside(this IndentedTextWriter indentedTextWriter)
    {
        indentedTextWriter.WriteLine("{");
        indentedTextWriter.Indent++;
    }

    internal static void GoOneLevelOutside(this IndentedTextWriter indentedTextWriter)
    {
        indentedTextWriter.Indent--;
        indentedTextWriter.WriteLine("}");
    }
}
