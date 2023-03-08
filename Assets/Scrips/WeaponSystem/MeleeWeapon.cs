using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� ���ݰ� ��ų�� ������Ʈ ������ ������ �̿��Ͽ� ����
//������Ʈ ���Ͽ��� receiver�� ���(�ൿ�� �ϰԵǴ� ��ü)
public class MeleeWeapon : Weapon
{
    //public enum MELEEKIND {AXE,MACE,LONGSWORD,MELEEMAX};
    protected MELEEKIND kind;

    //Animator animator;

    //������ Ŭ���� ������ �ش� ��ġ�� ���Ͱ� ������ ���͸� Ÿ������ �̵��Ѵ�. ���͸� Ÿ������ �̵��Ҷ��� ��� ���� ��ġ�� ����� �Ѵ�. �ϴ� �ش� ��ġ�� �̵��Ѵ�.At
    //�ϴ� �����̴µ� Ÿ�� ���Ͱ� ���ù��� �ȿ� ������ �����̴°� ���߰� ���������� ���� ������ �ش����� ���ݽ���
    public virtual void MeleeAttackStart(Player player, baseMonster monster)
    {
        this.sc_player = player;
        this.sc_monster = monster;
        


    }
    public override void Attack()
    {
        //StartCoroutine("MeleeAttack");
    }

    public IEnumerator MeleeAttack()
    {
        while(true)
        {
            if (sc_player.State == PLAYERSTATE.ATTACK)
            {
                sc_player.playeranimator.SetTrigger("Attacktrigger");
                //sc_monster.CurHP -= damage;

                if(!sc_monster.BeAttacked(damage))
                {
                    //������ ���
                    //GameObject dropitem = GameObject.Instantiate(GameManager.GetI.DropItem);
                    //dropitem.GetComponent<DropItem>().SetItemType(Random.Range(0, (int)Item.ITEMS.ItemMax));
                    //dropitem.transform.position = sc_player.targetmonster.transform.position;

                    //���Ͱ� �׾�����

                    //ĳ���� ����ġ ����
                    sc_player.GetExp(100);

                    //���ݸ���
                    sc_player.TargetMonster = null;
                    sc_player.State = PLAYERSTATE.IDLE;

                    //�ش� ���� �ı�
                    sc_monster.Dead();
                }

                ////�÷��̾ �����̰ų� �Ҷ��� ������ �ٸ������� ó��
                //if (sc_monster.CurHP <= 0)
                //{
                //    //GameObject dropitem = GameObject.Instantiate(GameManager.GetI.DropItem);
                //    //dropitem.GetComponent<DropItem>().SetItemType(Random.Range(0, (int)Item.ITEMS.ItemMax));
                //    //dropitem.transform.position = sc_player.targetmonster.transform.position;

                //    sc_player.GetExp(100);
                //    //���ݸ���
                //    sc_player.TargetMonster = null;
                //    sc_player.State = PLAYERSTATE.IDLE;
                //    GameObject.Destroy(sc_monster.gameObject);
                //    yield break;
                //}
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(cooltime);
        }
    }


    void Start()
    {
        weaponkind = WEAPONKIND.MELEE;
        Debug.Log("Melee Start");
    }

}
