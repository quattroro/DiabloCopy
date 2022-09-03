using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class IWeaponCommand : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}

//커맨드 패턴의 상위의 인터페이스
public interface IWeaponCommand
{
    public void Execute();
}