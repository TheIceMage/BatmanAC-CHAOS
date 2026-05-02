using BmSDK.BmGame;
using BmSDK.GFxUI;

namespace BmSDK.IceMage.GFxHUD;

public class GFxHUD
{
    public GFxHUD(FString extension, string name, string path = null)
    {
        ExtensionName = extension;
        LocalRootString = "_root.GrandMaster.ModuleMaster."+ExtensionName+".Load";
        OpenSWF(name, path);
    }

    public FString ExtensionName { get; }
    public FString LocalRootString { get; }
    public string SwfName;
    public string SwfPath;
    public SwfMovie Swf;
    public SwfMovie OpenSWF(string name, string path = null)
    {
        SwfName = name;
        SwfPath = path;
        if (path == null) path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\BmGame\Movies\", name + ".swf"));
        Swf = new SwfMovie(Game.GetWorldInfo(), ExtensionName.ToString());
        var file = File.ReadAllBytes(path);
        var arr = new TArray<byte>(file.Length);
        foreach (var bytes in file)
        {
            arr.Add(bytes);
        }
        Swf.RawData = arr;
        return Swf;
    }

    public void CreateHudExtension(RPlayerController RPC)
    {
        OpenSWF(SwfName, SwfPath);
        RPC.HudMovieNew.CreateHudExtension(ExtensionName, RPC.HudMovieNew.GetSwfMovieFullName(Swf));
        RPC.HudMovieNew.SetModuleFlagInt(ExtensionName, "PlayerSideIndex", RPC.PlayerNum);
        Debug.Log("Initiated " + ExtensionName + " from " + RPC.HudMovieNew.GetSwfMovieFullName(Swf));
    }

    public GFxMoviePlayer.FASValue GetVariable(FString Name, RPlayerController RPC)
    {
        FString Path = LocalRootString.ToString() + "." + Name;
        return RPC.HudMovieNew.GetVariable(Path);
    }
    public GFxObject GetVariableObject(FString Name, RPlayerController RPC)
    {
        FString Path = LocalRootString.ToString() + "." + Name;
        return RPC.HudMovieNew.GetVariableObject(Path);
    }

    public void SetVariable(FString Name, GFxMoviePlayer.FASValue Arg, RPlayerController RPC)
    {
        FString Path = LocalRootString.ToString() + "." + Name;
        RPC.HudMovieNew.SetVariable(Path, Arg);
    }

    public void SetVariableObject(FString Name, GFxObject Object, RPlayerController RPC)
    {
        FString Path = LocalRootString.ToString() + "." + Name;
        RPC.HudMovieNew.SetVariableObject(Path, Object);
    }

    public void ActionScriptVoid(FString Name, RPlayerController RPC)
    {
        FString Path = LocalRootString.ToString() + "." + Name;
        RPC.HudMovieNew.ActionScriptVoid(Path);
    }

    public GFxObject ActionScriptObject(FString Name, RPlayerController RPC)
    {
        FString Path = LocalRootString.ToString() + "." + Name;
        return RPC.HudMovieNew.ActionScriptObject(Path);
    }

    public GFxMoviePlayer.FASValue Invoke(FString Name, TArray<GFxMoviePlayer.FASValue> args, RPlayerController RPC)
    {
        FString Path = LocalRootString + "." + Name;
        var val = RPC.HudMovieNew.Invoke(Path, args);
        return val;
    }
}