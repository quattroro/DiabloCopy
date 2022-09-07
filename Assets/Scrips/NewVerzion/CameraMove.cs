using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float CameraSpeed;
    public float CameraVal;
    public Vector3 Camerapos;
    public Vector3 targetpos;
    public Vector3 direction;
    public Player CS_Palyer;


    //카메라가 선형보간을 이용해서 플레이어를 천천히 따라오도록 해준다.
    public void CamMove()
    {
        targetpos = CS_Palyer.transform.position + Camerapos;//카메라가 가야할 위치를 구하고
        direction = targetpos - transform.position;//현재 위치에서 가야할 위치고 가는 벡터를 구한다.
        Vector3 moveval = direction * CameraSpeed * Time.deltaTime;
        transform.position += moveval;

    }

    // Start is called before the first frame update
    void Start()
    {
        Camerapos = this.transform.position - CS_Palyer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CamMove();
    }
}
