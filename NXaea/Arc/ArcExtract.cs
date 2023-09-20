using System.Text.Json;

namespace NXaea.Arc
{
    internal class ArcExtract
    {
        public static void Extract(string InputPath, string? OutputPath)
        {
            string InputDirectory = Path.GetDirectoryName(InputPath);
            string OutputFolder = OutputPath != null ? OutputPath : Path.Combine(InputDirectory, Path.GetFileNameWithoutExtension(InputPath));
            string JsonPath = Path.Combine(InputDirectory, Path.GetFileNameWithoutExtension(InputPath) + ".json");
            string PackPath = Path.Combine(InputDirectory, Path.GetFileNameWithoutExtension(InputPath) + ".pack");

            if (!File.Exists(JsonPath) || !File.Exists(PackPath))
            {
                Console.WriteLine(".pack/.json file is missing.");
                return;
            }

            string JsonData = File.ReadAllText(JsonPath);
            Arc ArcRoot = JsonSerializer.Deserialize<Arc>(JsonData);
            byte[] ArcData = File.ReadAllBytes(PackPath);

            foreach (Group GroupInfo in ArcRoot.Groups)
            {
                string GroupPath = Path.Combine(OutputFolder, GroupInfo.Name);

                if (!Directory.Exists(GroupPath))
                    Directory.CreateDirectory(GroupPath);

                foreach (Entry EntryInfo in GroupInfo.OrderedEntries)
                {
                    string EntryFolder = Path.GetDirectoryName(EntryInfo.OriginalFilename);
                    string EntryFolderPath = Path.Combine(GroupPath, EntryFolder);

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
