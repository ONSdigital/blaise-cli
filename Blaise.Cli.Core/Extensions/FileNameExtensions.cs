namespace Blaise.Cli.Core.Extensions
{
    using System.IO;

    public static class FileNameExtensions
    {
        public static string GetQuestionnaireNameFromFile(this string questionnaireFile)
        {
            return Path.GetFileNameWithoutExtension(questionnaireFile);
        }
    }
}
