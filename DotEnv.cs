namespace dotnetServer
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    public static class DotEnv
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var envRegex = new Regex("^(?<key>[\\w|_]+)=((\"(?<value>.+)\")|(?<value>.+))$", RegexOptions.ExplicitCapture);
                var match = envRegex.Match(line);

                var key = match.Groups["key"].Value;
                var value = match.Groups["value"].Value;

                if (String.IsNullOrEmpty(key) || String.IsNullOrEmpty(value))
                    continue;

                Environment.SetEnvironmentVariable(key, value);
            }
        }

        public static void Load()
        {
            var root = Directory.GetCurrentDirectory();
            var dotenvFilePath = Path.Combine(root, ".env");

            Load(dotenvFilePath);
        }
    }
}