using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyLogic.Files;

namespace ConsoleAppTest
{
    enum CommandLineParam
    {
        InputFile,
        OutputFile,
        Threshold,
        SummaryType,
        None
    }

    public enum SummaryType
    {
        First,
        Second
    }

    public class CommandLineParams
    {
        public static (string, string, double, SummaryType) Parse(string[] args)
        {
            string inputFile = FuzzySetParser.DefaultPath;
            string outputFile = SummaryResultWriter.DefaultPath;
            double T1_THRESHOLD = 0.1;
            SummaryType typeToGenerate = SummaryType.First;

            CommandLineParam currentOption = CommandLineParam.None;

            foreach (string param in args)
            {
                switch (param)
                {
                    case "-i":
                        currentOption = CommandLineParam.InputFile;
                        break;
                    case "-o":
                        currentOption = CommandLineParam.OutputFile;
                        break;
                    case "-t":
                        currentOption = CommandLineParam.SummaryType;
                        break;
                    case "-d":
                        currentOption = CommandLineParam.Threshold;
                        break;
                    default:
                        switch (currentOption)
                        {
                            case CommandLineParam.InputFile:
                                inputFile = param;
                                break;
                            case CommandLineParam.OutputFile:
                                outputFile = param;
                                break;
                            case CommandLineParam.SummaryType:
                                typeToGenerate = param.StartsWith("f") ? SummaryType.First : SummaryType.Second;
                                break;
                            case CommandLineParam.Threshold:
                                T1_THRESHOLD = Double.Parse(param, CultureInfo.InvariantCulture);
                                break;
                        }
                        currentOption = CommandLineParam.None;
                        break;
                }
                
            }

            return (inputFile, outputFile, T1_THRESHOLD, typeToGenerate);
        }
    }
}
