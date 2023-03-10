using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : Singleton<GameManager>
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
    public Player CS_Player = null;
    public GameObject PlayerObj = null;
    public GameObject DropItem = null;

    //public MoveAstar astar = null;
    public AstarModule astarModule;


    public baseMonster testmonster;

    public Player GetPlayer()
    {
        return CS_Player;
    }

    public Camera GetMainCamera()
    {
        return _MainCamera;
    }

    

    void Start()
    {
        Cursor.SetCursor(Resources.Load<Texture2D>("Cursur/Cursor2"), Vector2.zero, CursorMode.ForceSoftware);

        MapManager.Instance.InitSetting();

        //AstarModule.Instance.InitSetting();

        //astarModule = new AstarModule();

        astarModule = FindObjectOfType<AstarModule>();
        astarModule.InitSetting();
        

    }

    public AstarModule GetAstarModule()
    {
        return astarModule;
    }

    private void Awake()
    {
        if(_instance==null)
        {
            _instance = this;
        }

        //Camerasize = _MainCamera.orthographicSize;
        //screenheight = Screen.height;
        //screenwidth = Screen.width;
        CS_Player = FindObjectOfType<Player>();
        PlayerObj = GameObject.Find("Player");
        //카메라가플레이어바로 위에서 바라ㄷ보도록 해준다.
        //Vector3 temp = CS_Player.gameObject.transform.position;
        //temp.z = -1;
        //_MainCamera.transform.position = temp;
        //Camerapos = _MainCamera.transform.position - CS_Player.transform.position;
        //Camerapos=
    }

    [Header("Camera")]
    public float CameraSpeed;
    public float CameraVal;
    public Vector3 Camerapos;
    public Vector3 targetpos;
    public Vector3 direction;
    //카메라가 선형보간을 이용해서 플레이어를 천천히 따라오도록 해준다.
    //public void CameraMove()
    //{
    //    targetpos = CS_Player.transform.position + Camerapos;//카메라가 가야할 위치를 구하고
    //    direction = targetpos - _MainCamera.transform.position;//현재 위치에서 가야할 위치고 가는 벡터를 구한다.
    //    Vector3 moveval = direction * CameraVal * Time.deltaTime;
    //    _MainCamera.transform.position += moveval;

    //}

    //[Header("Test")]
    //public float val = 1920f / 1080f;
    //public float temp2;
    //void Update()
    //{

    //    //CameraMove();
    //    //MouseClick();
    //}
}
