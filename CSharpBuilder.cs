using System;
using System.Text;

namespace Antlr_language;

public class CSharpBuilder {
    string OutputPath;
    StringBuilder Output = new StringBuilder();

    public CSharpBuilder(string outputPath) {
        this.OutputPath = outputPath;
    }

    public void AppendLine (string line) {
        Output.Append(line + "\n");
    }
    public void Append(string line)
    {
        Output.Append(line);
    }
    public void AcceptStringBuilder(StringBuilder sb) {
        Output = sb;
    }

    public void OutputResult () {
        using (StreamWriter outputFile = new StreamWriter(Path.GetFullPath(OutputPath)))
        {
            outputFile.WriteLine(Output.ToString());
        }
    }


}