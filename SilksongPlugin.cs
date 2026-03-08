using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace Silksong;

[BepInAutoPlugin(id: "io.github.xrroman.GravityFlux", version: "1.0.0")]
public partial class SilksongPlugin : BaseUnityPlugin
{
    public static SilksongPlugin Instance;
    private GravityMenu _menu;

    private void Awake()
    {
        Instance = this;
        _menu = gameObject.AddComponent<GravityMenu>(); 

        var harmony = new Harmony(Id);
        harmony.PatchAll();

        Logger.LogInfo("Silksong Gravity Mod Loaded!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3) || (_menu.IsVisible && Input.GetKeyDown(KeyCode.Escape)))
        {
            _menu.Toggle();
        }
    }
}