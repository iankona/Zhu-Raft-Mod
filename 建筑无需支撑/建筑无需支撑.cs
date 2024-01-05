using System;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;

namespace 建筑无需支撑
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.raft.block_build_anywhere";
        public const string NAME = "建筑无需支撑";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "Raft.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll();
        }
    }

    [HarmonyPatch(typeof(Block), "IsStable")]
    class 建筑无需支撑
    { 
        public static bool Prefix(Block __instance, ref bool __result)
        {
            __result = true;
            if ((UnityEngine.Object)__instance.stableComponent != (UnityEngine.Object)null)
                __instance.stableComponent.requiredHitCount = 0;
            return false;

        }
    }
}

