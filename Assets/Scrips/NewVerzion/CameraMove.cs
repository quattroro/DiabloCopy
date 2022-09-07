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


    //ī�޶� ���������� �̿��ؼ� �÷��̾ õõ�� ��������� ���ش�.
    public void CamMove()
    {
        targetpos = CS_Palyer.transform.position + Camerapos;//ī�޶� ������ ��ġ�� ���ϰ�
        direction = targetpos - transform.position;//���� ��ġ���� ������ ��ġ�� ���� ���͸� ���Ѵ�.
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
