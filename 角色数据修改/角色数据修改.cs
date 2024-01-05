using System;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;

namespace 角色数据修改
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.raft.player_data_modif";
        public const string NAME = "角色数据修改";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "Raft.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll();
        }
    }

    [HarmonyPatch(typeof(PersonController), "Start")]
    class 角色数据修改
    {
        public static void Postfix(PersonController __instance)
        {
            __instance.normalSpeed *= 1.5f; // 行走
            __instance.sprintSpeed *= 1.5f; // 冲刺
            __instance.jumpSpeed *= 1.5f; // 跳跃
            __instance.swimSpeed *= 1.5f; // 游泳

        }
    }

    [HarmonyPatch(typeof(PersonController), "CalculateFallDamage")]
    class 角色掉落无伤
    {
        public static bool Prefix(ref float p_fallDuration)
        {
            return false; // 拦截掉落伤害计算方法
        }
    }

    [HarmonyPatch(typeof(PlayerStats), "Start")]
    class 角色状态修改 // 血条
    {
        public static void Postfix(PlayerStats __instance)
        {
            float multiple = 10.0f;
            Console.WriteLine("水、食物、健康");
            // 饱食度
            if ( (UnityEngine.Object)__instance.stat_hunger != (UnityEngine.Object)null )
            {
                __instance.stat_hunger.normalConsumable.SetMaxValue(__instance.stat_hunger.normalConsumable.Max * multiple);
                __instance.stat_hunger.normalConsumable.statTarget.SetMaxValue(__instance.stat_hunger.normalConsumable.statTarget.Max * multiple);
                __instance.stat_hunger.normalConsumable.statTarget.Value *= multiple;
                __instance.stat_hunger.normalConsumable.Value *= multiple;
                Console.WriteLine(__instance.stat_hunger.normalConsumable.Max);
            }

            // 口渴度
            if ((UnityEngine.Object)__instance.stat_thirst != (UnityEngine.Object)null)
            {
                __instance.stat_thirst.normalConsumable.SetMaxValue(__instance.stat_thirst.normalConsumable.Max * multiple);
                __instance.stat_thirst.normalConsumable.statTarget.SetMaxValue(__instance.stat_thirst.normalConsumable.statTarget.Max * multiple);
                __instance.stat_thirst.normalConsumable.statTarget.Value *= multiple;
                __instance.stat_thirst.normalConsumable.Value *= multiple;
                Console.WriteLine(__instance.stat_thirst.normalConsumable.Max);
            }

            // 生命值
            if ( (UnityEngine.Object)__instance.stat_health != (UnityEngine.Object)null )
            { 
                __instance.stat_health.SetMaxValue(__instance.stat_health.Max * multiple);
                Console.WriteLine(__instance.stat_health.Max);
            }

        }
    }
}


//[Info: Console] 水、食物、健康
//[Info: Console] 1000
//[Info: Console] 1000
//[Info: Console] 1000