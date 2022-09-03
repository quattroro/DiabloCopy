using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{

    Item item;

    public void SetItemType(int itemkind)
    {
        //item = new HPPotion("HPPotion", Item.ITEMTYPE.USEABLE, 1, 1);
        //Debug.Log("플레이어 add실행");
        switch ((Item.ITEMS)itemkind)
        {
            case Item.ITEMS.Axe:
                item = new Axe("Axe", Item.ITEMTYPE.EQUIP, 2, 3);
                break;

            case Item.ITEMS.HeavyArmor:
                item = new HeavyArmor("HeavyArmor", Item.ITEMTYPE.EQUIP, 2, 3);
                break;

            case Item.ITEMS.Helmet:
                item = new Helmet("Helmet", Item.ITEMTYPE.EQUIP, 2, 3);
                break;

            case Item.ITEMS.HPPotion:
                item = new HPPotion("HPPotion", Item.ITEMTYPE.EQUIP, 2, 3);
                break;

            case Item.ITEMS.LightArmor:
                item = new LightArmor("LightArmor", Item.ITEMTYPE.EQUIP, 2, 3);
                break;

            case Item.ITEMS.MPPotion:
                item = new MPPotion("MPPotion", Item.ITEMTYPE.EQUIP, 2, 3);
                break;


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemBag itemBag = GameManager.GetI.CS_Palyer.itemBag;
        if (collision.transform.tag == "Player")
        {
            if (itemBag.gameObject.activeSelf == false)
            {
                itemBag.gameObject.SetActive(true);
                itemBag.Add(item);
                itemBag.gameObject.SetActive(false);
            }
            else
            {
                itemBag.Add(item);
            }
        }
        GameObject.Destroy(this.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
