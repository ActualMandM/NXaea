using System.Text.Json;

namespace NXaea.Arc
{
    internal class Pack
    {
        private static readonly string[] GroupNames = {
            "startup",
            "audio_init",
            "buttons",
            "mainmenu",
            "topbar",
            "base_shutters",
            "jackets_large",
            "jackets_small",
            "packs",
            "charts",
            "songselect_bgs",
            "character_sprites",
            "not_large_png",
            "not_large_jpg",
            "not_audio_or_images",
            "audio_wav",
            "not_audio",
            "Fallback"
        };

        private static Arc Root = new();
        private static List<byte> Data = new();
        private static string InputPath = string.Empty;

        public static void PackArc(string inputPath, string? outputPath)
        {
            InputPath = inputPath;

            foreach (string GroupName in GroupNames)
            {
                Group GroupInfo = new()
                {
                    Name = GroupName
                };

                Root.Groups.Add(GroupInfo);
            }

            RecursiveSearch(inputPath);

            LoadEntries();

            File.WriteAllText((outputPath ?? inputPath) + ".json", JsonSerializer.Serialize(Root, new JsonSerializerOptions { WriteIndented = true }));
            File.WriteAllBytes((outputPath ?? inputPath) + ".pack", Data.ToArray());
        }

        public static void RecursiveSearch(string folderPath)
        {
            foreach (string folder in Directory.GetDirectories(folderPath))
            {
                RecursiveSearch(folder);
            }

            foreach (string file in Directory.GetFiles(folderPath))
            {
                CreateEntry(file);
            }
        }

        public static void CreateEntry(string filePath)
        {
            string InternalPath = filePath.Replace(InputPath, "")[1..];
            string GroupName = InternalPath.Split('\\').First();

            if (!GroupNames.Contains(GroupName))
            {
                Console.WriteLine(GroupName + " does not exist in the game. Skipping.");
                return;
            }

            foreach (Group GroupInfo in Root.Groups)
            {
                if (GroupInfo.Name != GroupName)
                    continue;

                Entry EntryInfo = new()
                {
                    OriginalFilename = InternalPath.Substring(GroupName.Length + 1).Replace('\\', '/')
                };

                GroupInfo.OrderedEntries.Add(EntryInfo);
            }
        }

        public static void LoadEntries()
        {
            for (int i = 0; i < Root.Groups.Count; i++)
            {
                Group CurGroup = Root.Groups[i];

                foreach (Entry EntryInfo in CurGroup.OrderedEntries)
                {
                    byte[] FileData = File.ReadAllBytes(Path.Combine(InputPath, CurGroup.Name, EntryInfo.OriginalFilename));

                    CurGroup.Length += FileData.Length;
                    EntryInfo.Length = FileData.Length;
                    EntryInfo.Offset = Data.Count;

                    Data.AddRange(FileData);
                }

                if (i > 0)
                {
                    Group PrevGroup = Root.Groups[i - 1];
                    CurGroup.Offset = PrevGroup.Offset + PrevGroup.Length;
                }
            }
        }
    }
}
