using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIItemScript : MonoBehaviour
{
    
    //public int stock = 1;//사용가능 아이템은 여러개 스택이 가능
    public Item item;
    public ItemBag itembag;

    public void SetItem(Item item)
    {
        this.item = item;
        Debug.Log($"아이템 설정됨 이름 : {item.name}, 인덱스 : {item.myindex}");
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
