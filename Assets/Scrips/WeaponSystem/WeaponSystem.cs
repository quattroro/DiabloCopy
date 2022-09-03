using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    enum weaponcmdkind { MELEE,SHOOT, CMDMAX};
    //public Dictionary<weaponcmdkind, MeleeCommand> cmdlist;
    private CommandManager commandMgr = null;

    private void Awake()
    {
        commandMgr = new CommandManager();

        Weapon_Staff staff = new Weapon_Staff();
        Weapon_Axe axe = new Weapon_Axe();
        Weapon_Mace mace = new Weapon_Mace();

        //MeleeCommand<MeleeWeapon> meleecommand = new MeleeCommand<MeleeWeapon>(axe, axe.MeleeAttackStart);
        //MeleeCommand<ShootWeapon> shootcommand = new MeleeCommand<ShootWeapon>(staff, staff.ShootAttack);

        //commandMgr.SetCommand("MeleeAttack", meleecommand);
        //commandMgr.SetCommand("ShootAttack", shootcommand);

    }

    void WeaponAttack(weaponcmdkind cmdkind)
    {
        if(weaponcmdkind.MELEE==cmdkind)
        {
            commandMgr.InvokeExecute("MeleeAttack");
        }
        else if(weaponcmdkind.SHOOT==cmdkind)
        {
            commandMgr.InvokeExecute("ShootAttack");
        }    
    }
    



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            commandMgr.InvokeExecute("MeleeAttack");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            commandMgr.InvokeExecute("ShootAttack");
        }
    }
}
