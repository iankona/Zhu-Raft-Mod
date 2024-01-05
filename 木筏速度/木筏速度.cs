using System;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace 木筏速度
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.raft.water_drift_speed";
        public const string NAME = "木筏速度";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "Raft.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll();
        }
    }


    [HarmonyPatch(typeof(Raft), "Start")]
    class 木筏速度
    { 
        public static void Postfix(Raft __instance)
        {
            __instance.maxVelocity = 20.0f * 5.0f; //5.0f
            __instance.maxSpeed = 20.0f * 1.5f; //1.5f
            __instance.waterDriftSpeed = 0.25f; //1.5f
            // float currentMovementSpeed = (float)AccessTools.Field(typeof(Raft), "currentMovementSpeed").GetValue(__instance);
            // currentMovementSpeed = 0.25f; // 1.0f
        }
    }
}


