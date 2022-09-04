using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//장비 슬롯
public class EquipSlot : BaseSlot
{
    //장비 타입
    public EnumTypes.EquipmentTypes equiptype;
    public override void Start()//초기화
    {
        base.Start();
        slottype = EnumTypes.SlotTypes.Equip;
    }
    //장비 타입을 리턴한다.
    public override EnumTypes.EquipmentTypes GetEquipTypes()
    {
        return equiptype;
    }

    void Update()
    {
        
    }
}
