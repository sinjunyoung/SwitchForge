namespace SwitchForge.Core.Disc;

public class CueFile
{
    public string FilePath { get; set; } = string.Empty;

    public List<CueFileEntry> Entries { get; set; } = [];
}