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
    public float screenratio;//���θ� �������� ȭ���� ���� ����
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
        //ī�޶��÷��̾�ٷ� ������ �ٶ󤧺����� ���ش�.
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
    //ī�޶� ���������� �̿��ؼ� �÷��̾ õõ�� ��������� ���ش�.
    //public void CameraMove()
    //{
    //    targetpos = CS_Player.transform.position + Camerapos;//ī�޶� ������ ��ġ�� ���ϰ�
    //    direction = targetpos - _MainCamera.transform.position;//���� ��ġ���� ������ ��ġ�� ���� ���͸� ���Ѵ�.
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
