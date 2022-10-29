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
    public Player CS_Player;
    //public Camera curCamera;


    // Start is called before the first frame update
    void Start()
    {
        if (CS_Player == null)
            CS_Player = GameManager.Instance.CS_Player;

        Camerapos = this.transform.position - CS_Player.transform.position;
        //GetMainCamera();
    }


    //ī�޶� ���������� �̿��ؼ� �÷��̾ õõ�� ��������� ���ش�.
    public void CamMove()
    {
        targetpos = CS_Player.transform.position + Camerapos;//ī�޶� ������ ��ġ�� ���ϰ�
        
        direction = targetpos - transform.position;//���� ��ġ���� ������ ��ġ�� ���� ���͸� ���Ѵ�.

        
        Vector3 moveval = Vector3.Lerp(transform.position, targetpos, 0.2f);

        transform.position = moveval;

    }

    

    // Update is called once per frame
    void Update()
    {
        CamMove();
    }
}
