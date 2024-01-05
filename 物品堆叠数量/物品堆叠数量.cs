using System;
using BepInEx;
using HarmonyLib;

namespace 物品堆叠数量
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.raft.item_stacking_number";
        public const string NAME = "物品堆叠数量";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "Raft.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll();
        }
    }


    [HarmonyPatch(typeof(ItemInstance_Inventory), "StackSize", MethodType.Getter)]
    class 物品堆叠数量
    { 
        public static bool Prefix(ref int __result)
        {
            __result = 99999;
            return false; //拦截原方法，直接使用我们给出的结果。
        }
    }
}

