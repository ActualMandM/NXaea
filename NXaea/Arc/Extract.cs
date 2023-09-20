using System.Text.Json;

namespace NXaea.Arc
{
    internal class Extract
    {
        public static void ExtractArc(string inputPath, string? outputPath)
        {
            string InputDirectory = Path.GetDirectoryName(inputPath)!;
            string OutputFolder = outputPath ?? Path.Combine(InputDirectory, Path.GetFileNameWithoutExtension(inputPath));
            string JsonPath = Path.Combine(InputDirectory, Path.GetFileNameWithoutExtension(inputPath) + ".json");
            string PackPath = Path.Combine(InputDirectory, Path.GetFileNameWithoutExtension(inputPath) + ".pack");

            if (!File.Exists(JsonPath) || !File.Exists(PackPath))
            {
                Console.WriteLine(".pack/.json file is missing.");
                return;
            }

            Arc Root = JsonSerializer.Deserialize<Arc>(File.ReadAllText(JsonPath))!;
            byte[] ArcData = File.ReadAllBytes(PackPath);

            foreach (Group GroupInfo in Root.Groups)
            {
                string GroupPath = Path.Combine(OutputFolder, GroupInfo.Name);

                if (!Directory.Exists(GroupPath))
                    Directory.CreateDirectory(GroupPath);

                foreach (Entry EntryInfo in GroupInfo.OrderedEntries)
                {
                    string EntryFolderPath = Path.Combine(GroupPath, Path.GetDirectoryName(EntryInfo.OriginalFilename)!);

                    if (!Directory.Exists(EntryFolderPath))
                        Directory.CreateDirectory(EntryFolderPath);

                    string EntryPath = Path.Combine(GroupPath, EntryInfo.OriginalFilename);

                    FileStream EntryFile = File.Create(EntryPath);
                    EntryFile.Write(ArcData, EntryInfo.Offset, EntryInfo.Length);
                    EntryFile.Close();
                }
            }
        }
    }
}
