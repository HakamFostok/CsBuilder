namespace CsBuilder.Test;

public class UnitTest1
{
    [Fact]
    public void CreateSimpleFile()
    {
        bool newCsharp = false;
        string className = "MyClass";
        string namespaceName = "MyNamespace";
        string interfaceName = "global::System.IDisposable";

        ICsFileBuilder builder = new CsFileBuilder();
        builder.AddAutoGeneratedHeader("Disposer")
               .AddEmptyLine()
               .AddFileScopedNamespace(namespaceName, newCsharp)
               .AddEmptyLine(newCsharp)
               .AddNamespace(namespaceName, !newCsharp)
               .AddStatementAndStartBlock($"partial class {className} : {interfaceName}")
               .AddStatements("private bool disposed = false;",
                                    string.Empty,
                                    "partial void DisposeManaged();",
                                    "partial void DisposeUnmanaged();",
                                    string.Empty)
               .AddStatementAndStartBlock($"~{className}()")
               .AddStatements("Dispose(false);")
               .EndBlock()
               .AddEmptyLine()
               .AddStatementAndStartBlock("private void Dispose(bool disposing)")
               .AddOneLineIf("!disposed", "return;")
               .AddEmptyLine()
               .AddOneLineIf("disposing", "DisposeManaged();")
               .AddStatements("DisposeUnmanaged();",
                                    "disposed = true;")
               .EndBlock()
               .AddEmptyLine()
               .AddStatementAndStartBlock("public void Dispose()")
               .AddStatements("Dispose(true);",
                                    "global::System.GC.SuppressFinalize(this);")
               .EndBlock()
               .EndBlock()
               .EndNamespace();

        string file = builder.Build();
        Console.WriteLine(file);
    }
}