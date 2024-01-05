using System;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;

namespace 物品获取倍数
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.raft.item_add_multiple";
        public const string NAME = "物品获取倍数";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "Raft.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll();
        }
    }

    [HarmonyPatch(typeof(PlayerInventory), "AddItem", new Type[] { typeof(string), typeof(int) })]
    class 物品获取倍数
    { 
        public static bool Prefix(string uniqueItemName, ref int amount)
        {
            var 物品列表 = new List<string>()
                    {
                        "Thatch",
                        "Plastic",
                        "Plank",
                        "Scrap",
                        "Stone",
                        // "Raw_Beet",
                        // "Raw_Potato",
                    };
            bool 有在列表 = 物品列表.Contains(uniqueItemName);
            if (有在列表)
            {
                amount = amount * 11;
            }
            // Console.WriteLine(uniqueItemName);
            return true; //运行原方法
        }
    }
}
// Thatch 棕榈叶
// Plastic 塑料
// Plank 厚木板
// Rope 绳子
// Scrap 碎石
// Stone 石头
// Flower_Yellow 
// Flower_Red
// Flower_White
// Seed_Flower_Yellow 种子
// Seed_Flower_Red
// Seed_Flower_White
// Seed_Pineapple 菠萝种子
// Raw_Beet 生甜菜根
// Raw_Potato 生土豆
// Pineapple 菠萝
