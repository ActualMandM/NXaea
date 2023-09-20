using NXaea.Arc;

namespace NXaea
{ 
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                Console.WriteLine("Please provide a folder or pack/json file.");
                Console.WriteLine("Usage: NXaea <input> [<output>]");
                return;
            }
            else
            {
                if (Path.GetExtension(args[0]).ToLowerInvariant() is ".pack" or ".json")
                {
                    Extract.ExtractArc(args[0], args.Length == 2 ? args[1] : null);
                }
                else if (File.GetAttributes(args[0]).HasFlag(FileAttributes.Directory))
                {
                    Pack.PackArc(args[0], args.Length == 2 ? args[1] : null);
                }
            }
        }
    }
}