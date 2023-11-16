using System.Text.Json.Serialization;

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

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(Arc))]
    public partial class ArcContext : JsonSerializerContext
    {
    }
}
