using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��� ����
public class EquipSlot : BaseSlot
{
    //��� Ÿ��
    public EnumTypes.EquipmentTypes equiptype;
    public override void Start()//�ʱ�ȭ
    {
        base.Start();
        slottype = EnumTypes.SlotTypes.Equip;
    }
    //��� Ÿ���� �����Ѵ�.
    public override EnumTypes.EquipmentTypes GetEquipTypes()
    {
        return equiptype;
    }

    void Update()
    {
        
    }
}
