namespace SwitchForge.Core.Disc;

public class CueTrack
{
    public int Number { get; set; }

    public string DataType { get; set; } = string.Empty;

    public List<CueIndex> Indexes { get; set; } = [];
}