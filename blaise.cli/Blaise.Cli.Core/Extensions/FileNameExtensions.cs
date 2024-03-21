
using System.IO;

namespace Blaise.Cli.Core.Extensions
{
    public static class FileNameExtensions
    {
        public static string GetQuestionnaireNameFromFile(this string questionnaireFile)
        {
            return Path.GetFileNameWithoutExtension(questionnaireFile);
        }
    }
}
