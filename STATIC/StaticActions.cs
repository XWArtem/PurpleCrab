using System;

public static class StaticActions
{
    public static Action<bool> ToggleInputControllerAction;
    public static Action<bool> TogglePlayerIsDeadAction;
    public static Action<bool> SetCameraActiveAction;
    public static Action LoadLastLevelAction;
    public static Action<int> LoadSceneOnNameAction;
}
