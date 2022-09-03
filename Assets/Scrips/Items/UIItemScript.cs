using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIItemScript : MonoBehaviour
{
    
    //public int stock = 1;//��밡�� �������� ������ ������ ����
    public Item item;
    public ItemBag itembag;

    public void SetItem(Item item)
    {
        this.item = item;
        Debug.Log($"������ ������ �̸� : {item.name}, �ε��� : {item.myindex}");
    }

    private void OnEnable()
    {
        if(item!=null)
        {
            if(item.type == Item.ITEMTYPE.USEABLE)
            {
                Text text = transform.Find("Text").GetComponent<Text>();
                text.text = $"{item.stock}";
            }
        }
    }

    public void UseItem()
    {
        itembag.UseItem(item.myindex);
    }
    // Start is called before the first frame update
    void Start()
    {
        itembag = GameObject.Find("ItemBag").GetComponent<ItemBag>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
