using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//�κ�Ŀ ���� Ŀ�ǵ���� �����ϰ� �ൿ�� ������� �ش�.
public class CommandManager /*: MonoBehaviour*/
{
    //Ŀ�ǵ���� ������ �ڷᱸ���� Dictionary�� ���
    private Dictionary<string, IWeaponCommand> commanddic = new Dictionary<string, IWeaponCommand>();


    public void SetCommand(string name, IWeaponCommand command)
    {
        if(commanddic.ContainsValue(command))
        {
            //�̹� Ŀ�ǵ� ����
            Debug.Log("�̹� Ŀ�ǵ� ����");
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
