namespace SwitchForge.Core.Models.Switch;

public enum RenameState
{
    Ready,

    Unchanged,

    MissingInfo,

    EmptyName,

    NameTooLong,

    Duplicate,

    TargetExists
}