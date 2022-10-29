using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//��� ���Ե��� ������ ���â
public class EquipmentWindow : BaseUI
{
    //public List<BaseSlot> equipslots;
    public BaseSlot[] equipslotarr;//��� ���Ե��� ������ �迭

    public override void Init()
    {
        base.Init();
        _type = UIManager.UITYPES.INV;
    }

    //��� ���Կ� ��� �����Ѵ�.
    public bool EquipEquipments(BaseNode node, BaseSlot slot)
    {
        //������ ������ ��� �ƴϸ� ��� ����
        if (node.GetItemTypes() != EnumTypes.ItemTypes.Equips)
            return false;
        if (slot.SettingNode != null)
            return false;

        EquipSlot eslot = slot as EquipSlot;


        if(node.GetEquipTypes()==EnumTypes.EquipmentTypes.TwoHand)//�������� ��� ��� ����϶�
        {
            //�޼� ������ ��� ��� �־���� ���� ����
            if (equipslotarr[(int)EnumTypes.EquipmentTypes.LeftArm].SettingNode != null && equipslotarr[(int)EnumTypes.EquipmentTypes.RightArm].SettingNode != null)
                return false;
            slot.SetNode(node);
        }
        else if(node.GetEquipTypes() == EnumTypes.EquipmentTypes.RightArm)//������ ����϶��� �޼տ� ������ �������� ������ �ƴϿ����� ���� ����
        {
            ItemNode temp;
            if (equipslotarr[(int)EnumTypes.EquipmentTypes.LeftArm].SettingNode != null && equipslotarr[(int)EnumTypes.EquipmentTypes.LeftArm].SettingNode.GetEquipTypes() != EnumTypes.EquipmentTypes.TwoHand)
                return false;

            slot.SetNode(node);
        }
        else
        {
            if (node.GetEquipTypes() == eslot.equiptype)//����� ������ ���������� �������� Ȯ���ϰ����� �����ϸ� ����ִ´�.
            {
                slot.SetNode(node);
            }
            else
            {
                return false;
            }
        }

        //if(node.GetEquipTypes()==eslot.equiptype)//����� ������ ���������� �������� Ȯ���ϰ����� �����ϸ� ����ִ´�.
        //{
        //    slot.SetNode(node);
        //}
        //else
        //{
        //    //���� Ǯ������ �����̸� ���� �ִ� �ڸ��� �ǵ��ư���.
        //    //if (node.PreSlot.GetSlotTypes() != EnumTypes.SlotTypes.Equip)
        //    //    ItemBag.Instance.SetItem(node, node.PreSlot);
        //    //else
        //    //    EquipEquipments(node, node.PreSlot);
        //    return false;
        //}
        return true;
    }

    //��� �����ϸ� �����ų �Լ�
    public void EquipEvent()
    {

    }

    //��� �����ϸ� �����ų �Լ�
    public void UnEquipEvent()
    {

    }


    private void Awake()
    {
        BaseSlot[] temp = (BaseSlot[])GetComponentsInChildren<EquipSlot>();//��񽽷Ե��� �о�鿩��
        equipslotarr = new BaseSlot[(int)EnumTypes.EquipmentTypes.EquipMax];//�迭�� �������� ������ �°� ����ִ´�.
        for (int i=0;i< temp.Length;i++)
        {
            equipslotarr[(int)temp[i].GetEquipTypes()] = temp[i];
        }

        //equipslots = temps.ToList();

        for (int i = 0; i < equipslotarr.Length; i++)//�̺�Ʈ���� ������ش�.
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
