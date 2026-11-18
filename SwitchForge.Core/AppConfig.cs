using Common.Models;
using SwitchForge.Core.Services;
using SwitchForge.Core.Services.Switch;
using SwitchForge.Core.ViewModels;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SwitchForge.Core;

public class CommonConfig : ViewModelBase
{
    public double LogBoxHeight { get; set { SetProperty(ref field, value); } } = 130;

    public bool Topmost { get; set { SetProperty(ref field, value); } }
    
    public bool AutoCheckUpdate { get; set { SetProperty(ref field, value); } } = true;

    public string? Language { get; set { SetProperty(ref field, value); } } = null;

    public bool IsMuted { get; set { SetProperty(ref field, value); } }

    public double SoundVolume { get; set { SetProperty(ref field, value); } } = 50;

    public string CompleteSoundPath { get; set { SetProperty(ref field, value); } } = "complete.mp3";

    public string CancelSoundPath { get; set { SetProperty(ref field, value); } } = "cancel.mp3";

    public string ErrorSoundPath { get; set { SetProperty(ref field, value); } } = "error.mp3";
}

public class WindowConfig : ViewModelBase
{
    public double Left { get; set { SetProperty(ref field, value); } } = 100;

    public double Top { get; set { SetProperty(ref field, value); } } = 100;

    public double Width { get; set { SetProperty(ref field, value); } } = 900;

    public double Height { get; set { SetProperty(ref field, value); } } = 715;

    public bool IsMaximized { get; set { SetProperty(ref field, value); } } = false;
}

public class SwitchConfig : ViewModelBase
{
    public int CompressLevel { get; set { SetProperty(ref field, value); } } = 18;

    public int Lz4CompressLevel { get; set { SetProperty(ref field, value); } } = 9;

    public bool VerifyCompress { get; set { SetProperty(ref field, value); } } = false;

    public SwitchCompressionMode CompressionMode { get; set { SetProperty(ref field, value); } } = SwitchCompressionMode.Block;

    public bool ForceKeyGen0 { get; set { SetProperty(ref field, value); } } = false;

    public string? SaturnNspPath { get; set { SetProperty(ref field, value); } }

    public string RenameFormat { get; set { SetProperty(ref field, value); } } = RenameService.DefaultFormat;
}

public class OutputFoldersConfig : ViewModelBase
{
    public string? SwitchRepackOutputPath { get; set { SetProperty(ref field, value); } }

    public string? SwitchMergeOutputPath { get; set { SetProperty(ref field, value); } }

    public string? SwitchSplitOutputPath { get; set { SetProperty(ref field, value); } }

    public string? SwitchLz4MergeOutputPath { get; set { SetProperty(ref field, value); } }
}

public class KeyFilesConfig : ViewModelBase
{
    public string? SwitchProdKeysPath { get; set { SetProperty(ref field, value); } }

}

public class AppConfig : ViewModelBase
{
    private static readonly string DefaultFilePath = Path.ChangeExtension(Environment.ProcessPath!, "config.json");
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    private static readonly Lazy<AppConfig> _instance = new(() => new AppConfig().LoadInternal());
    public static AppConfig Instance => _instance.Value;

    public CommonConfig Common { get; set { SetProperty(ref field, value); } } = new();

    public WindowConfig Window { get; set { SetProperty(ref field, value); } } = new();

    public SwitchConfig Switch { get; set { SetProperty(ref field, value); } } = new();

    public OutputFoldersConfig OutputFolders { get; set { SetProperty(ref field, value); } } = new();

    public KeyFilesConfig KeyFiles { get; set { SetProperty(ref field, value); } } = new();

    [JsonConstructor]
    private AppConfig() { }

    private AppConfig LoadInternal()
    {
        if (!File.Exists(DefaultFilePath))
        {
            Save();
            SubscribeToChanges();
            return this;
        }

        try
        {
            var json = File.ReadAllText(DefaultFilePath);
            var loaded = JsonSerializer.Deserialize<AppConfig>(json);

            if (loaded != null)
            {
                Common = loaded.Common ?? new();
                Window = loaded.Window ?? new();
                Switch = loaded.Switch ?? new();
                OutputFolders = loaded.OutputFolders ?? new();
                KeyFiles = loaded.KeyFiles ?? new();
            }
        }
        catch
        {
            Save();
        }

        SubscribeToChanges();
        return this;
    }

    private void SubscribeToChanges()
    {
        void AutoSave(object? s, PropertyChangedEventArgs e) => Save();

        Common.PropertyChanged += AutoSave;
        Window.PropertyChanged += AutoSave;
        Switch.PropertyChanged += AutoSave;
        OutputFolders.PropertyChanged += AutoSave;
        KeyFiles.PropertyChanged += AutoSave;
        KeyFiles.PropertyChanged += (_, _) => KeyFileService.Apply(KeyFiles);

        KeyFileService.Apply(KeyFiles);
    }

    public void Save() => File.WriteAllText(DefaultFilePath, JsonSerializer.Serialize(this, JsonOptions));
}