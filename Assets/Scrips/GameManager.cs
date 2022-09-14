using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;
    public static GameManager GetI
    {
        get
        {
            return _instance;
        }
    }

    public Tilemap _tilemap = null;
    public Camera _MainCamera = null;
    public float Camerasize;
    public float screenheight;
    public float screenwidth;
    public float screenratio;//���θ� �������� ȭ���� ���� ����
    private Vector3 _clickpos = Vector3.zero;
    private Vector3Int _tilemapcell = Vector3Int.zero;
    public GameObject Pointer = null;
    public Player CS_Palyer = null;
    public GameObject PlayerObj = null;
    public GameObject DropItem = null;

    public MoveAstar astar = null;

    public baseMonster testmonster;



    //������Ŭ�� �̵� ����Ŭ�� ������ȹ��, ���� ����
    //���콺 ������ ����
    //���콺�� ĳ���� ���� �ö󰡰ų� 
    //���� �����ϵ���
    public void MouseClick()
    {

        //���콺 ��Ŭ��
        if(Input.GetMouseButtonDown(0))
        {
            if(Input.mousePosition.y>=144f)
            {
                
                Ray ray = _MainCamera.ScreenPointToRay(Input.mousePosition);
                Vector3 point = _MainCamera.ScreenToWorldPoint(Input.mousePosition);


                RaycastHit2D[] hit = Physics2D.CircleCastAll(point, 0.1f, Vector2.zero, 0);
                foreach(RaycastHit2D a in hit)
                {
                    if(a.transform.tag=="Wall")
                    {
                        return;
                    }

                    if(a.transform.tag=="Enemy")
                    {
                        //Debug.Log("Attackmove");
                        CS_Palyer.AttackMove(CS_Palyer.transform.position, a.point, a.transform);
                        return;
                    }
                    

                }
                //Debug.Log("nomalmove");
                CS_Palyer.Move(CS_Palyer.transform.position, hit[0].point);
               
            }
            
        }
    }

    void Start()
    {
        Cursor.SetCursor(Resources.Load<Texture2D>("Cursur/Cursor2"), Vector2.zero, CursorMode.ForceSoftware);
        
    }

    private void Awake()
    {
        if(_instance==null)
        {
            _instance = this;
        }

        Camerasize = _MainCamera.orthographicSize;
        screenheight = Screen.height;
        screenwidth = Screen.width;
        CS_Palyer = FindObjectOfType<Player>();
        PlayerObj = GameObject.Find("Player");
        //ī�޶��÷��̾�ٷ� ������ �ٶ󤧺����� ���ش�.
        Vector3 temp = CS_Palyer.gameObject.transform.position;
        temp.z = -1;
        _MainCamera.transform.position = temp;
        Camerapos = _MainCamera.transform.position - CS_Palyer.transform.position;
        //Camerapos=
    }

    [Header("Camera")]
    public float CameraSpeed;
    public float CameraVal;
    public Vector3 Camerapos;
    public Vector3 targetpos;
    public Vector3 direction;
    //ī�޶� ���������� �̿��ؼ� �÷��̾ õõ�� ��������� ���ش�.
    public void CameraMove()
    {
        targetpos = CS_Palyer.transform.position + Camerapos;//ī�޶� ������ ��ġ�� ���ϰ�
        direction = targetpos - _MainCamera.transform.position;//���� ��ġ���� ������ ��ġ�� ���� ���͸� ���Ѵ�.
        Vector3 moveval = direction * CameraVal * Time.deltaTime;
        _MainCamera.transform.position += moveval;

    }

    [Header("Test")]
    public float val = 1920f / 1080f;
    public float temp2;
    void Update()
    {

        CameraMove();
        MouseClick();
    }
}
