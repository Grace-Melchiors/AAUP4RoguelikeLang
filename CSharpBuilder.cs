namespace Antlr_language;

public class CSharpBuilder {
    string OutputPath = "Content/output/output" + DateTime.now() + ".cs";

    public CSharpBuilder(string outputPath) {
        OutputPath = outputPath;
    }

}