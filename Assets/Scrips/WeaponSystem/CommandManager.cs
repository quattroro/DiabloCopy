using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//인보커 역할 커맨드들을 관리하고 행동을 실행시켜 준다.
public class CommandManager /*: MonoBehaviour*/
{
    //커맨드들을 관리할 자료구조는 Dictionary를 사용
    private Dictionary<string, IWeaponCommand> commanddic = new Dictionary<string, IWeaponCommand>();


    public void SetCommand(string name, IWeaponCommand command)
    {
        if(commanddic.ContainsValue(command))
        {
            //이미 커맨드 존재
            Debug.Log("이미 커맨드 존재");
            return;
        }
        commanddic.Add(name, command);
    }

    public void InvokeExecute(string name)
    {
        commanddic[name].Execute();
    }


    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
