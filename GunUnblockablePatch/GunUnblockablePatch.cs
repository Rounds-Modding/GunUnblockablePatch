using BepInEx; // requires BepInEx.dll and BepInEx.Harmony.dll
using UnityEngine; // requires UnityEngine.dll, UnityEngine.CoreModule.dll
using HarmonyLib; // requires 0Harmony.dll
using System.Collections;
using Photon.Pun;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
// requires Assembly-CSharp.dll

namespace GunUnblockablePatch
{
    
    [BepInPlugin(ModId, ModName, "0.0.0.0")]
    [BepInProcess("Rounds.exe")]
    public class GunUnblockablePatch : BaseUnityPlugin
    {
        private void Awake()
        {
            new Harmony(ModId).PatchAll();
        }
        private void Start()
        {

        }

        private const string ModId = "pykess.rounds.plugins.gununblockablepatch";

        private const string ModName = "GunUnblockablePatch";
    }
}