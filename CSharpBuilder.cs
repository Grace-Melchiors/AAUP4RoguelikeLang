using System;
using System.Text;

namespace Antlr_language;

public class CSharpBuilder {
    string OutputFolder;
    string OutputFile;
    StringBuilder Output = new StringBuilder();

    public CSharpBuilder() {
        OutputFolder = "Content/output";
        OutputFile = "output" + /*DateTime.Now.ToShortDateString() +*/ ".txt";
    }
    public CSharpBuilder(string outputFolder, string outputFile) {
        OutputFolder = outputFolder;
        OutputFile = outputFile;
    }

    public void AppendLine (string line) {
        Output.Append(line + "\n");
    }
    public void Append(string line)
    {
        Output.Append(line);
    }
    public void InsertStringBuilder(StringBuilder sb) {
        Output = sb;
    }

    public void OutputResult () {
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(OutputFolder, OutputFile)))
        {
            outputFile.WriteLine(Output.ToString());
        }
    }


}