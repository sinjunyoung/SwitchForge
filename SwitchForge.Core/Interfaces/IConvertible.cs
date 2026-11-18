namespace SwitchForge.Core.Interfaces;

public interface IConvertible
{
    List<string> AvailableFormats { get; }

    string SelectedTargetFormat { get; set; }
}