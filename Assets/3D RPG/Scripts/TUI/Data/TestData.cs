using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._3D_RPG.Scripts.TUI.Data
{

    public class ItemData : IData
    {
        public string ItemName;
        public string ItemDescription;
        public int ItemPrice;
    }

    public class PortionData : ItemData
    {
        public int HpRecovery;
        public int MpRecovery;
    }
    public class EquipData : ItemData
    {
        public int AddAttack;
        public int AddDefence;
    }
    public class SlotDataTest : IData
    {
        public ItemData Item;
        public int Count;

        public void AddItem(ItemData item, int count)
        {
            
        }
    }

    public class QuickSlotData2 : IData
    {
        public List<SlotDataTest> SlotDatas;
    }

    public class InventoryData2 : IData
    {
        public List<SlotDataTest> SlotDatas;
    }

    public class EquipmentData2 : IData
    {
        public List<SlotDataTest> SlotDatas;
    }
    public class ShopData2 : IData
    {
        public List<SlotDataTest> SlotDatas;
    }
    public class PlayerData
    {
        public static PlayerData Instance;

        public InventoryData InventoryData;
        public EquipmentData EquipmentData;
        public QuickSlotData QuickSlotData;

        public void Save()
        {
            // 데이터 저장
        }

        public static PlayerData Load()
        {
            // 데이터 불러오기
            return new PlayerData();
        }
    }

    //public class Test
    //{
    //    public void TestFunc()
    //    {
    //        PortionData portionData = new PortionData();

    //        PlayerData.Instance.InventoryData.SlotDatas[5].Item = portionData;
    //        PlayerData.Instance.InventoryData.SlotDatas[5].Count = 5;

    //        PlayerData.Instance.QuickSlotData.SlotDatas[3].Item = portionData;
    //    }

    //    // 상인으로부터 구매한 아이템과 개수가 인자로 전달됨 (가정)
    //    public void Buy(ItemData data, int count)
    //    {
    //        if (PlayerData.Instance.InventoryData.SlotDatas[0].Item != null)
    //        {
    //            PlayerData.Instance.InventoryData.SlotDatas[0].Count += count;
    //        }
    //        else
    //        {
    //            PlayerData.Instance.InventoryData.SlotDatas[0].Item = data;
    //            PlayerData.Instance.InventoryData.SlotDatas[0].Count = count;
    //        }

    //    }

    //    public void TestFunc2()
    //    {
    //        PlayerData.Instance.Save();
    //    }
    //}


    public class TestData
    {
    }
}
