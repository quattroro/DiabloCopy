using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBolt : baseMagic
{
    public float radian = 0f;
    public float degree = 0f;
    //유도
    public override void MagicMove()
    {
        //float radian = 0f;
        if (targetmonster != null)
        {
            direction = targetmonster.transform.position - this.transform.position;
            direction.Normalize();
            radian = Mathf.Atan2(direction.y, direction.x);
            degree = radian * Mathf.Rad2Deg - 90f;
            this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree));
            this.transform.position = this.transform.position + (direction * magicspeed * Time.deltaTime);
        }


        ////목표의 방향으로 움직인다
        //if (Time.time - lasttime >= timeval)
        //{
        //    lasttime = Time.time;
            
        //}
    }


    //마법이 적과 충돌했을때는
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌 들어옴1");
        if (collision.transform.tag == "Enemy")
        {
            targetmonster.CurHP -= Damage;
            if(targetmonster.CurHP>=0)
            {
                //뒷정리하는 함수를 만든다.



            }


            
        }
        //충돌 애니메이션이 있을때는 애니메이션의 이벤트를이용해서 없앤다.
        animator.SetTrigger("Explosive");
        //Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌 들어옴2");
        if (collision.transform.tag == "Enemy")
        {
            targetmonster.CurHP -= Damage;

            if (targetmonster.CurHP >= 0)
            {
                //뒷정리하는 함수를 만든다.



            }

        }
        //충돌 애니메이션이 있을때는 애니메이션의 이벤트를이용해서 없앤다.
        animator.SetTrigger("Explosive");
        //Destroy(this.gameObject);
    }


    //충돌 애니메이션이 출력된 이후에 이벤트를이용해서 해당 함수를 호출시켜 객체를 없애준다.
    public void IsDestroy()
    {
        Debug.Log("마법 사라짐");
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
