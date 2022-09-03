using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyBolt : baseMagic
{
    //����
    public override void MagicMove()
    {

        if (targetmonster != null)
        {
            direction = targetmonster.transform.position - this.transform.position;
            this.transform.position = this.transform.position + (direction.normalized * magicspeed * Time.deltaTime);
        }
        ////��ǥ�� �������� �����δ�
        //if (Time.time - lasttime >= timeval)
        //{
        //    lasttime = Time.time;
        //}
    }

    //������ ���� �浹��������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            targetmonster.CurHP -= Damage;

            if (targetmonster.CurHP >= 0)
            {
                //�������ϴ� �Լ��� �����.

                
            }
            
        }
        //�浹 �ִϸ��̼��� �������� �ִϸ��̼��� �̺�Ʈ���̿��ؼ� ���ش�.
        animator.SetTrigger("Explosive");
        //Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            targetmonster.CurHP -= Damage;

            if (targetmonster.CurHP >= 0)
            {
                //�������ϴ� �Լ��� �����.



            }

        }
        //�浹 �ִϸ��̼��� �������� �ִϸ��̼��� �̺�Ʈ���̿��ؼ� ���ش�.
        animator.SetTrigger("Explosive");
        //Destroy(this.gameObject);
    }


    //�浹 �ִϸ��̼��� ��µ� ���Ŀ� �̺�Ʈ���̿��ؼ� �ش� �Լ��� ȣ����� ��ü�� �����ش�.
    public void IsDestroy()
    {
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Damage = 10;
        CoolTime = 1.2f;
    }

    // Update is called once per frame
    void Update()
    {
        MagicMove();
    }
}
