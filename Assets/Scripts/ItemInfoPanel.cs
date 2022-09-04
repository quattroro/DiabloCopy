using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//아이템 정보를 보여주는 패널
public class ItemInfoPanel : MonoBehaviour
{
    static bool _isActive;

    //보여줄 정보들
    public Image itemimage;
    public Text Itemname;

    public Text StrRequire;
    public Text DexRequire;

    public Text ItemClass;

    public Text Damage;
    public Text Price;

    public Text Durability;

    static public bool PanelIsActive
    {
        get
        {
            return _isActive;
        }
        set
        {
            _isActive = value;
        }
    }


    private void Awake()
    {
    }
    
    
    //정보창을 보여준다.
    public void ShowInfos(ItemNode node, Vector3 pos)
    {
        this.gameObject.SetActive(true);//활성화 시켜주고
        
        //아이템 노드의 데이터 들을 받아와서 보여준다
        this.itemimage.sprite = node.GetComponent<Image>().sprite;
        this.Itemname.text = node.itemName;
        this.StrRequire.text = string.Format("StrRequire : {0}", node.strrequire);
        this.DexRequire.text = string.Format("DexRequire : {0}", node.dexrequire);

        this.Durability.text = string.Format("MaxDurability : {0}", node.durability);

        this.Damage.text = string.Format("Damage : {0}~{1}", node.damage[0], node.damage[1]);

        this.Price.text = string.Format("{0}G",node.price);

        this.transform.position = pos;

    }

    //정보창을 숨겨준다.
    public void HideInfos()
    {
        this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        HideInfos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
