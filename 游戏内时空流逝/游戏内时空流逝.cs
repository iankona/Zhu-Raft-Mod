using System;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using Knife.DeferredDecals.Spawn;
using UnityEngine;
using UnityEngine.AzureSky;

namespace 游戏内时空流逝
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.raft.game_time_progression";
        public const string NAME = "游戏内时空流逝";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "Raft.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll();
        }
    }

    [HarmonyPatch(typeof(AzureSkyController), "Start")]
    class 游戏内时空流逝
    {
        public static void Postfix(AzureSkyController __instance)
        {
            // Console.WriteLine("m_timeProgression"); // 0.02
            Traverse.Create(__instance).Field("m_timeProgression").SetValue(0.001f);
            // Console.WriteLine(m_timeProgression);
        }
    }

}




