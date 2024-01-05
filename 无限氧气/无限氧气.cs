using System;
using BepInEx;
using HarmonyLib;

namespace 无限氧气
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.raft.max_oxygen";
        public const string NAME = "无限氧气";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "Raft.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll();
        }
    }


    [HarmonyPatch(typeof(Stat_Oxygen), "Update")]
    class 无限氧气
    { 
        public static bool Prefix(Stat_Oxygen __instance)
        {
            __instance.Value = __instance.Max;
            return false; // 拦截原方法
        }
    }
}

