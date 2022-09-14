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
    public float screenratio;//세로를 기준으로 화면의 가로 비율
    private Vector3 _clickpos = Vector3.zero;
    private Vector3Int _tilemapcell = Vector3Int.zero;
    public GameObject Pointer = null;
    public Player CS_Palyer = null;
    public GameObject PlayerObj = null;
    public GameObject DropItem = null;

    public MoveAstar astar = null;

    public baseMonster testmonster;



    //오른쪽클릭 이동 왼쪽클릭 아이템획득, 몬스터 공격
    //마우스 움직임 관리
    //마우스가 캐릭터 위에 올라가거나 
    //몬스터 공격하도록
    public void MouseClick()
    {

        //마우스 왼클릭
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
        //카메라가플레이어바로 위에서 바라ㄷ보도록 해준다.
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
    //카메라가 선형보간을 이용해서 플레이어를 천천히 따라오도록 해준다.
    public void CameraMove()
    {
        targetpos = CS_Palyer.transform.position + Camerapos;//카메라가 가야할 위치를 구하고
        direction = targetpos - _MainCamera.transform.position;//현재 위치에서 가야할 위치고 가는 벡터를 구한다.
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
