using Common.Models;

namespace SwitchForge.Core.Models.Switch;

public sealed record NszSettings(int CompressLevel, SwitchCompressionMode CompressionMode);