using System;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace 扩充背包格数
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess(GAME_PROCESS)]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "cn.zhufile.raft.bag_add_slot";
        public const string NAME = "扩充背包格数";
        public const string VERSION = "1.0.0";
        private const string GAME_PROCESS = "Raft.exe";


        public void Start()
        {
            new Harmony(GUID).PatchAll();
        }
    }


    [HarmonyPatch(typeof(Inventory), "InitializeSlots")]
    class 增加背包数量
    {
        public static int bagStartIndex;
        public static bool Prefix(Inventory __instance)
        {
            bagStartIndex = __instance.allSlots.Count;
            return true;
        }
    
        public static void Postfix(Inventory __instance)
        {
            if (__instance.allSlots.Count <= 0) // 防止已研究界面一直显示及显示出错
                return;
            int 增加格子数 = 20;
            var slottype = __instance.allSlots[0].slotType;
            if (slottype == SlotType.Hotbar) // 初始背包
                增加格子数 = 35; 
            if (slottype == SlotType.Normal) // 储物箱
                增加格子数 = 24; 

            Slot allSlot = __instance.allSlots[bagStartIndex];
            Transform parent = allSlot.gameObject.transform.parent;
            List<Slot> collection = new List<Slot>();
            for (int index = 0; index < 增加格子数; ++index)
            {
                Slot component = UnityEngine.Object.Instantiate<GameObject>(allSlot.gameObject, parent, true).GetComponent<Slot>();
                component.InitializeEventListeners(__instance);
                collection.Add(component);
            }
            if (__instance.allSlots[0].slotType == SlotType.Hotbar)
            {
                for (int index = __instance.allSlots.Count - 1; index >= bagStartIndex; --index)
                {
                    if (__instance.allSlots[index].slotType == SlotType.Backpack)
                    {
                        __instance.allSlots.InsertRange(index + 1, (IEnumerable<Slot>)collection);
                        break;
                    }
                }
            }
            else
                __instance.allSlots.AddRange((IEnumerable<Slot>)collection);
        }
    }


    [HarmonyPatch(typeof(PlayerInventory), "InitializeSlots")]
    class 增加背包面板
    {
        public static void Postfix(PlayerInventory __instance)
        {
            int num = Mathf.CeilToInt(30.0f/5.0f);
            RectTransform component = __instance.gameObject.GetComponent<RectTransform>();
            component.anchoredPosition = new Vector2(component.anchoredPosition.x, component.anchoredPosition.y + (float)num * 30.0f);
        }
    }
}



//public enum SlotType
//{
//    Hotbar,
//    Normal,
//    Equipment,
//    Backpack,
//}