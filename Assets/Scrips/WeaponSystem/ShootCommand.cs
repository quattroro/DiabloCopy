using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand<Receiver> : IWeaponCommand
{
    public void Execute()
    {
        //m_reciever.Action();
        Action();

    }

    public ShootCommand()
    {

    }
    public ShootCommand(Receiver m, FunctionPointer p)
    {
        m_reciever = m;
        Action = p;
    }

    private Receiver m_reciever;
    private FunctionPointer Action;


    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
