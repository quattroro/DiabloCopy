using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� ���ݰ� ��ų�� ������Ʈ ������ ������ �̿��Ͽ� ����
//������Ʈ ���Ͽ��� receiver�� ���(�ൿ�� �ϰԵǴ� ��ü)
public class ShootWeapon : Weapon
{
    public enum MAGICKIND { FIREBOLT, CHARGESHOT, HOLYBOLT,MAGICMAX };

    protected SHOOTKIND kind;
    //������ ���۵Ǹ� ���� ���õ� ����������Ʈ�� �����ؼ� ������ �������ش�.
    public GameObject[] Magics;
    //UI��ư�� ����
    protected MAGICKIND NowMagic;


    public MAGICKIND MagicKind
    {
        get
        {
            return NowMagic;
        }
        set
        {
            NowMagic = value;
            baseMagic temp= Magics[(int)NowMagic].GetComponent<baseMagic>();
            damage = temp.Damage;
            cooltime = temp.CoolTime;

        }
    }


    //���� ��ü���� ������ �������� ������ �������� ��ȯ�Ѵ�.
    IEnumerator ShootAttack()
    {
        while (true)
        {
            if (sc_player.State == PLAYERSTATE.ATTACK)
            {
                Debug.Log("���� ������");
                GameObject obj = GameObject.Instantiate(Magics[(int)NowMagic]);
                obj.transform.position = sc_player.transform.position;
                baseMagic sc_magic = obj.GetComponent<baseMagic>();
                sc_magic.SetTarget(this, sc_player.sc_targetmonster);
                //���Ͱ� ������ ���� �������� �ʱ�ȭ ���ְ� ���͸� �����ش�.
                if (sc_monster.CurHP <= 0)
                {
                    GameObject dropitem = GameObject.Instantiate(GameManager.GetI.DropItem);
                    dropitem.GetComponent<DropItem>().SetItemType(Random.Range(0, (int)Item.ITEMS.ItemMax));
                    dropitem.transform.position = sc_player.targetmonster.transform.position;

                    sc_player.GetExp(100);

                    //���ݸ���
                    sc_player.TargetMonster = null;
                    sc_player.State = PLAYERSTATE.IDLE;
                    GameObject.Destroy(sc_monster.gameObject);
                    yield break;
                }



            }
            else
            {
                Debug.Log("�������� ��");
                yield break;
            }
            yield return new WaitForSeconds(cooltime);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        weaponkind = WEAPONKIND.BULLET;
        Debug.Log("Bullet Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
