namespace NXaea.Arc
{
    public class Arc
    {
        public List<Group> Groups { get; set; } = new List<Group>();
    }

    public class Group
    {
        public string Name { get; set; } = string.Empty;
        public int Offset { get; set; } = 0;
        public int Length { get; set; } = 0;
        public List<Entry> OrderedEntries { get; set; } = new List<Entry>();
    }

    public class Entry
    {
        public string OriginalFilename { get; set; } = string.Empty;
        public int Offset { get; set; } = 0;
        public int Length { get; set; } = 0;
    }

    public class ArcInfo
    {
        public static readonly string[] GroupNames = {
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
    }
}
