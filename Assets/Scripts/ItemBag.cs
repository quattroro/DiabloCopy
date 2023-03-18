using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ ������ ��� ���̺�� ���������� �����Ѵ�.
public class ItemBag :MonoBehaviour
{
    [SerializeField]
    private Vector2Int InventorySlotSize = new Vector2Int(10, 4);//�κ��丮 ����
    public Vector2 InveltorySlotInterval;//����

    [SerializeField]
    private int QuickInvectoryNum = 8;//������ ����
    public Vector2 QuickSlotInterval;//����

    //public BaseSlot EquipSlot;
    public BaseSlot InventorySlot;
    public BaseSlot QuickSlot;

    
    public BaseSlot[] InventorySlotArr;//�κ��丮 ���Ե�
    public BaseSlot[] QuickSlotArr;//�����Ե�

    private void Awake()
    {
        InventorySlotInit();
    }

    //�ʱ�ȭ �۾� �����丮 ���Ե��� ����� �ְ� ��ġ�� �������ش�.
    public void InventorySlotInit()
    {
        BaseSlot temp;
        InventorySlotArr = new BaseSlot[InventorySlotSize.x * InventorySlotSize.y];
        InventorySlotArr[0] = InventorySlot;
        for (int y = 0; y < InventorySlotSize.y; y++)
        {
            for (int x = 0; x < InventorySlotSize.x; x++)
            {
                if (x != 0 || y != 0)
                {
                    temp = GameObject.Instantiate<BaseSlot>(InventorySlot);
                    temp.transform.parent = InventorySlot.transform.parent;
                    temp.transform.position = InventorySlot.transform.position + new Vector3(x * InveltorySlotInterval.x, -y * InveltorySlotInterval.y);
                    temp.SlotIndex = new Vector2Int(x, y);
                    InventorySlotArr[x + (y * InventorySlotSize.x)] = temp;

                }
            }
        }

        QuickSlotArr = new BaseSlot[QuickInvectoryNum];
        QuickSlotArr[0] = QuickSlot;
        for (int x = 1; x < QuickInvectoryNum; x++)
        {
            temp = GameObject.Instantiate<BaseSlot>(QuickSlot);
            temp.transform.parent = QuickSlot.transform.parent;
            temp.transform.position = QuickSlot.transform.position + new Vector3(x * QuickSlotInterval.x, 0);
            temp.SlotIndex = new Vector2Int(x, 0);
            
            QuickSlotArr[x] = temp;
        }

    }

    //�ش� ��� �ı�
    public void DeleteItem(BaseSlot slot, BaseNode node)
    {
        slot.RemoveNode(node);
    }

    //�̸� ������� ��� ��ü�� �Ѱܹ޾Ƽ� ����
    public void InsertItem(BaseSlot slot, BaseNode node)
    {
        if(slot.SettingNode==null)
        {
            slot.SetNode(node);
        }
    }

    //�������� ������ ���濡 �־��ش�.
    public void InsertItem(BaseNode node)
    {
        Vector2Int itemsize;
        bool flag = false;

        //���� �������� ������ ������ �������̸� �����԰� �κ��丮 â���� ���� ������ �������� �ִ��� ã�� ���� �������� ������ ������ �������� �ش�.
        if(node.IsStackAble())
        {
            for (int x = 0; x < QuickInvectoryNum; x++)
            {
                if (QuickSlotArr[x + 0].GetSettingNodeID() == node.GetItemID())
                {
                    QuickSlotArr[x + 0].SettingNode.ChangeStack(node.GetStack());
                    Debug.Log("������ ���� ����");
                    GameObject.Destroy(node.gameObject);
                    return;
                }

            }

            for (int y = 0; y < InventorySlotSize.y; y++)
            {
                for (int x = 0; x < InventorySlotSize.x; x++)
                {
                    if (InventorySlotArr[x + y * (InventorySlotSize.x)].SettingNode != null)
                    {

                        if (InventorySlotArr[x + y * (InventorySlotSize.x)].GetSettingNodeID() == node.GetItemID())
                        {
                            InventorySlotArr[x + y * (InventorySlotSize.x)].SettingNode.ChangeStack(node.GetStack());
                            Debug.Log("���� ���� ����");
                            GameObject.Destroy(node.gameObject);
                            return;
                        }
                    }
                }
            }
        }

        //������ �Ұ����� �������̸� �κ��丮 â���� �ش� �������� ũ�⸸ŭ ������ ����ִ��� Ȯ���ϰ� ��������� �κ��丮�� �������� �־��ְ� �ƴϸ� �׳� �����ش�.
        for (int y = 0; y < InventorySlotSize.y; y++)
        {
            for (int x = 0; x < InventorySlotSize.x; x++)
            {
                //������ ã�ٰ� ����ִ� ������ ������ 
                if (InventorySlotArr[x + y * (InventorySlotSize.x)].SettingNode == null)
                {
                    //�װ��������� �ش� �������� ũ�⸸ŭ ����ִ��� Ȯ���Ѵ�.

                    itemsize = node.GetSize();
                    if(SlotIsEmpty(EnumTypes.SlotTypes.Item,new Vector2Int(x,y),itemsize))
                    {
                        SetItem(node, new Vector2Int(x, y));
                        Debug.Log("������ �־���");
                        //InventorySlotArr[x + y * (InventorySlotSize.x)].SetNode(node, node.GetSize());
                        return;
                    }
                }
            }
        }
        //������� ���� ������ ��ü�� ���ְ� ������.
        Debug.Log("����� ����");
        GameObject.Destroy(node.gameObject);
        return;

    }

    //�������� ũ�⿡ ���缭 ���Կ� �־��ش�.
    public void SetItem(BaseNode node,Vector2Int index)
    {
        Vector2 imagesize;
        BaseSlot topleft = InventorySlotArr[(index.x) + (index.y) * (InventorySlotSize.x)];
        //BaseSlot bottomright = InventorySlotArr[(index.x + node.GetSize().x) + (index.y + node.GetSize().y) * (InventorySlotSize.x)];
        node.SettedSlotList.Clear();

        imagesize.x = topleft.rectTransform.rect.width * node.GetSize().x;//������ ũ�⸦ ���� ũ��� �������� �����ϴ� ������ ������ ���缭 �������ش�.
        imagesize.y = topleft.rectTransform.rect.height * node.GetSize().y;

        node.transform.parent = topleft.transform;
        node.rectTransform.sizeDelta = imagesize;
        node.rectTransform.localPosition = new Vector3(0, 0, 0);


        for (int y=0;y<node.GetSize().y;y++)
        {
            for(int x=0;x<node.GetSize().x;x++)
            {
                InventorySlotArr[(index.x + x) + (index.y + y) * (InventorySlotSize.x)].SetNodeData(node);//�������� �����ϴ� ���� �ִ� ��� ���Ե鿡�� �ش� ������ ������ �Ѱ��ش�.
            }
        }
        
        

    }

    //������ ���� �������� �������� ũ�⸸ŭ ������ ����ִ��� Ȯ��
    public bool SlotIsEmpty(EnumTypes.SlotTypes type, Vector2Int start, Vector2Int size)
    {
        if (type == EnumTypes.SlotTypes.Item)//������â
        {
            if (start.x + size.x > InventorySlotSize.x || start.y + size.y > InventorySlotSize.y)//�����ۻ��� ũ�⸦ �Ѿ�� false
                return false;

            for (int i = 0; i < size.y; i++)
            {
                for (int j = 0; j < size.x; j++)
                {
                    if (InventorySlotArr[(start.x + j) + (start.y + i) * (InventorySlotSize.x)].SettingNode != null)//�������� �����ϰԵ� ��� ���Ե��� ����־�� �Ѵ�.
                    {
                        return false;
                    }
                }
            }

        }
        else if (type == EnumTypes.SlotTypes.Quick)//������â
        {
            if (start.x + size.x >= QuickInvectoryNum)//������â ũ�⸦ �Ѿ�� false
                return false;

            if (QuickSlotArr[start.x].SettingNode != null)//�̹� ���õ� ��尡 ������ false
                return false;
        }
        else
        {

        }
        return true;
    }

    //�������� ũ�⿡ ���缭 ���Կ� �־��ش�.
    public bool SetItem(BaseNode node, BaseSlot slot/*���� �� ����*/)
    {
        Vector2 imagesize;
        BaseSlot topleft = slot;
        Vector2Int index = slot.SlotIndex;

        //��� �������� ţ���Կ� �������� �Ҷ� �Ǵ� �������� �������� ��� ���Կ� �������� �Ҷ� ���´�.
        if(node.GetItemTypes() == EnumTypes.ItemTypes.Equips&&slot.GetSlotTypes()==EnumTypes.SlotTypes.Quick
            || node.GetItemTypes() == EnumTypes.ItemTypes.StackAble && slot.GetSlotTypes() == EnumTypes.SlotTypes.Equip)
        {
            return false;
        }
        //������������ Ŭ�������ְ�
        node.SettedSlotList.Clear();

        //������ ũ�⸦ ���� ũ��� �������� �����ϴ� ������ ������ ���缭 �������ش�.
        imagesize.x = topleft.rectTransform.rect.width * node.GetSize().x;
        imagesize.y = topleft.rectTransform.rect.height * node.GetSize().y;

        node.transform.parent = topleft.transform;
        node.rectTransform.sizeDelta = imagesize;
        node.rectTransform.localPosition = new Vector3(0, 0, 0);

        

        //�������� �������� ������ �������϶�
        if(slot.GetSlotTypes()==EnumTypes.SlotTypes.Quick)
        {
            //������ Ÿ���� �����Կ� �� �� �ִ� Ÿ������ Ȯ���ϰ� �־��ش�.
            if (node.GetItemTypes() == EnumTypes.ItemTypes.StackAble)
                slot.SetNodeData(node);
            else
                return false;
        }
        else if(slot.GetSlotTypes() == EnumTypes.SlotTypes.Item)//������ �����϶�
        {
            for (int y = 0; y < node.GetSize().y; y++)
            {
                for (int x = 0; x < node.GetSize().x; x++)
                {
                    InventorySlotArr[(index.x + x) + (index.y + y) * (InventorySlotSize.x)].SetNodeData(node);//���Կ� �־��ش�.
                }
            }
        }
        else// ��� �����϶�
        {

        }
        return true;
        



    }

}
