using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


//�ʿ� ���� �������� ������ ������ ��û�ϸ� �ش� �������� �����ش�.
//Ÿ�ϸ� ��ĭ�� ���α��� = 1, ���α��� 0.5
public class MapManager : MonoBehaviour
{

    //�� �Ŵ����� ���� ������ �������� �Ҷ� ����ϰ� �ʿ��ϱ� ������ �̱������� ����
    protected static MapManager m_Instance = null;

    public static MapManager GetI
    {
        get
        {
            if(m_Instance==null)
            {
                m_Instance = GameObject.FindObjectOfType<MapManager>();
            }
            return m_Instance;
        }
    }

    //private void Awake()
    //{
    //    _Wallmap = Find
    //}

    //����Ƽ���� �����ϴ� Ÿ�ϸ��� ����� �̿��ؼ� Ÿ���� ���� ��ġ�� ������ �ʰ� ���� ������ �������� �̿��� ����



    public enum ACT { ACT1, ACT2, ACTMAX };
    public GameObject[,] MapTiles = null;
    public int mapsize_x;
    public int mapsize_y;
    private ACT m_act;

    public Camera _MainCamera = null;
    public Tilemap _tilemap = null;
    public Tilemap _Wallmap = null;
    private Vector3Int _tilemapcell = Vector3Int.zero;


    public ACT act
    {
        get
        {
            return m_act;
        }
        set
        {
            switch(value)
            {
                case ACT.ACT1:
                    
                    break;

                case ACT.ACT2:

                    break;

            }
        }
    }

    public Vector3 MyGetCellCenterWorld(int x, int y)
    {

        Vector3 pos = _tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0));
        return pos;
    }
    


    //���� �ε����� �ָ� �ش� ���� ���� ��ġ�� ���ؼ� �ش� ��ġ�� ���̸����ش�. 
    //�׷��� �浹�� ��ü�� ���̾ wall�̸� true�� ����, �ƴϸ� false�� �����Ѵ�.

    public bool IsWall(Vector3Int index)
    {
        Vector3 worldpos = _tilemap.GetCellCenterWorld(index);

        Vector3 screenpos = _MainCamera.WorldToScreenPoint(worldpos);
        Ray ray = _MainCamera.ScreenPointToRay(screenpos);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);

        //Vector3 worldpos = 
        //Debug.DrawRay(worldpos, worldpos * 10, Color.blue, 3.5f);

        RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, Vector2.zero);

        foreach(RaycastHit2D a in hit)
        {
            if(a.transform.tag == "Wall")
            {
                return true;
            }
        }



        //_Wallmap = hit.transform.GetComponent<Tilemap>();
        ////_Wallmap.GetTile(index);
        ////Pointer.transform.position = hit.point;
        //if (_Wallmap != null)
        //{
        //    //_tilemap.RefreshAllTiles();
        //    //_tilemapcell = this._tilemap.WorldToCell(hit.point);
        //    if (_Wallmap.gameObject.layer == LayerMask.NameToLayer("Wall"))
        //    {
        //        return true;
        //        Debug.Log("wall");
        //    }
            
            

        //}



        return false;
    }


    //public Vector3 GetCellToWorldCenterpos(Tilemap tile, Vector3Int cell)
    //{
    //    Vector3 temp;
    //    //temp=tile.getcell
    //}



    public Vector3Int GetTileCellNum(Vector3 pos)//������ǥ�� �����ָ� �ش� ��ǥ�� ����ȣ�� �޾Ƽ� �����ش�.
    {



        //��带 ���� �������ش�.
        Vector3Int temp = new Vector3Int(0, 0, 0);
        //RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        //_tilemap = hit.transform.GetComponent<Tilemap>();
        if (_tilemap != null)
        {
            //_tilemap.RefreshAllTiles();
            temp = this._tilemap.WorldToCell(pos);
            //Debug.Log(_tilemap.tag);

        }
        //Debug.Log($"�� ��ȣ[{_tilemapcell.x},{_tilemapcell.y},{_tilemapcell.z}]");

        //Vector3Int temp = new Vector3Int(0, 0, 0);
        //RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        //_tilemap = hit.transform.GetComponent<Tilemap>();
        //if (_tilemap != null)
        //{
        //    //_tilemap.RefreshAllTiles();
        //    temp = this._tilemap.WorldToCell(hit.point);
        //    //Debug.Log(_tilemap.tag);

        //}
        //Debug.Log($"�� ��ȣ[{_tilemapcell.x},{_tilemapcell.y},{_tilemapcell.z}]");

        return temp;
    }



    public void CreateMap()
    {

    }

    //ȭ�鿡�� ���̴� ��ŭ�� raycast�� �̿��ؼ� �������� �޾ƿ´�.
    public void ReadMapInfo(Vector3 start, Vector3 target)
    {
        Vector2Int startcell = Vector2Int.zero;
        Vector2Int targetcell = Vector2Int.zero;


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
