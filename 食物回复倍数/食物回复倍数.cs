using System;
using BepInEx;
using HarmonyLib;

namespace 食物回复倍数
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.raft.food_use_multiple";
        public const string NAME = "食物回复倍数";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "Raft.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll();
        }
    }

    [HarmonyPatch(typeof(Stat_Bonus_Consumable), "Consume")]
    class 食物回复倍数
    { 
        public static bool Prefix(ref float yield, ref float bonusYield, ref bool isRaw)
        {
            var multiple = 30.0f;
            if (yield > 0.0f) // 食物消耗
                yield *= multiple;
            if (bonusYield > 0.0f)// 触发额外效果
                bonusYield *= multiple;
            return true; //运行原方法
        }
    }
}

