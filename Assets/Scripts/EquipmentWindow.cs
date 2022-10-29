using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//장비 슬롯들을 관리할 장비창
public class EquipmentWindow : BaseUI
{
    //public List<BaseSlot> equipslots;
    public BaseSlot[] equipslotarr;//장비 슬롯들을 관리할 배열

    public override void Init()
    {
        base.Init();
        _type = UIManager.UITYPES.INV;
    }

    //장비 슬롯에 장비를 장착한다.
    public bool EquipEquipments(BaseNode node, BaseSlot slot)
    {
        //아이템 유형이 장비가 아니면 등록 실패
        if (node.GetItemTypes() != EnumTypes.ItemTypes.Equips)
            return false;
        if (slot.SettingNode != null)
            return false;

        EquipSlot eslot = slot as EquipSlot;


        if(node.GetEquipTypes()==EnumTypes.EquipmentTypes.TwoHand)//넣으려는 장비가 양손 장비일때
        {
            //왼손 오른손 모두 비어 있어야지 장착 가능
            if (equipslotarr[(int)EnumTypes.EquipmentTypes.LeftArm].SettingNode != null && equipslotarr[(int)EnumTypes.EquipmentTypes.RightArm].SettingNode != null)
                return false;
            slot.SetNode(node);
        }
        else if(node.GetEquipTypes() == EnumTypes.EquipmentTypes.RightArm)//오른손 장비일때는 왼손에 작착된 아이템이 양손장비가 아니여야지 장착 가능
        {
            ItemNode temp;
            if (equipslotarr[(int)EnumTypes.EquipmentTypes.LeftArm].SettingNode != null && equipslotarr[(int)EnumTypes.EquipmentTypes.LeftArm].SettingNode.GetEquipTypes() != EnumTypes.EquipmentTypes.TwoHand)
                return false;

            slot.SetNode(node);
        }
        else
        {
            if (node.GetEquipTypes() == eslot.equiptype)//장비의 파츠가 장착가능한 파츠인지 확인하고장착 가능하면 집어넣는다.
            {
                slot.SetNode(node);
            }
            else
            {
                return false;
            }
        }

        //if(node.GetEquipTypes()==eslot.equiptype)//장비의 파츠가 장착가능한 파츠인지 확인하고장착 가능하면 집어넣는다.
        //{
        //    slot.SetNode(node);
        //}
        //else
        //{
        //    //장착 풀가능한 파츠이면 원래 있던 자리로 되돌아간다.
        //    //if (node.PreSlot.GetSlotTypes() != EnumTypes.SlotTypes.Equip)
        //    //    ItemBag.Instance.SetItem(node, node.PreSlot);
        //    //else
        //    //    EquipEquipments(node, node.PreSlot);
        //    return false;
        //}
        return true;
    }

    //장비를 장착하면 실행시킬 함수
    public void EquipEvent()
    {

    }

    //장비를 해제하면 실행시킬 함수
    public void UnEquipEvent()
    {

    }


    private void Awake()
    {
        BaseSlot[] temp = (BaseSlot[])GetComponentsInChildren<EquipSlot>();//장비슬롯들을 읽어들여서
        equipslotarr = new BaseSlot[(int)EnumTypes.EquipmentTypes.EquipMax];//배열에 열거형의 순서에 맞게 집어넣는다.
        for (int i=0;i< temp.Length;i++)
        {
            equipslotarr[(int)temp[i].GetEquipTypes()] = temp[i];
        }

        //equipslots = temps.ToList();

        for (int i = 0; i < equipslotarr.Length; i++)//이벤트들을 등록해준다.
        {
            if (equipslotarr[i] != null)
            {
                equipslotarr[i].InsertEvent(EquipEvent);
                equipslotarr[i].PickUpEvent(UnEquipEvent);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
