using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 무기들의 공격과 스킬은 컴포넌트 디자인 패턴을 이용하여 제작
//컴포넌트 패턴에서 receiver를 담당(행동을 하게되는 객체)
public class ShootWeapon : Weapon
{
    public enum MAGICKIND { FIREBOLT, CHARGESHOT, HOLYBOLT,MAGICMAX };

    protected SHOOTKIND kind;
    //공격이 시작되면 현재 선택된 마법오브젝트를 생성해서 방향을 지정해준다.
    public GameObject[] Magics;
    //UI버튼과 연동
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


    //마법 객체들을 공격할 방향으로 지정된 방향으로 소환한다.
    IEnumerator ShootAttack()
    {
        while (true)
        {
            if (sc_player.State == PLAYERSTATE.ATTACK)
            {
                Debug.Log("마법 만들음");
                GameObject obj = GameObject.Instantiate(Magics[(int)NowMagic]);
                obj.transform.position = sc_player.transform.position;
                baseMagic sc_magic = obj.GetComponent<baseMagic>();
                sc_magic.SetTarget(this, sc_player.sc_targetmonster);
                //몬스터가 죽으면 몬스터 정보들을 초기화 해주고 몬스터를 없애준다.
                if (sc_monster.CurHP <= 0)
                {
                    GameObject dropitem = GameObject.Instantiate(GameManager.GetI.DropItem);
                    dropitem.GetComponent<DropItem>().SetItemType(Random.Range(0, (int)Item.ITEMS.ItemMax));
                    dropitem.transform.position = sc_player.targetmonster.transform.position;

                    sc_player.GetExp(100);

                    //공격멈춤
                    sc_player.TargetMonster = null;
                    sc_player.State = PLAYERSTATE.IDLE;
                    GameObject.Destroy(sc_monster.gameObject);
                    yield break;
                }



            }
            else
            {
                Debug.Log("마법공격 끝");
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
