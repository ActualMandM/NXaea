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

        public static void PackArc(string inputPath, string? outputPath)
        {
            Console.WriteLine("Pack.PackArc(): stub");
        }
    }
}
