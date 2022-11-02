using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


//맵에 관한 정보들을 가지고 정보를 요청하면 해당 정보들을 보내준다.
//타일맵 한칸의 가로길이 = 1, 세로길이 0.5
//맵 전체 정보를 가지고 있는다.
//맵 을 지역별로 나눠서 
public class MapManager : MonoBehaviour
{

    //맵 매니저는 맵의 정보를 얻을려고 할때 빈번하게 필요하기 때문에 싱글톤으로 구성
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

    public int RegionSizeX;
    public int RegionSizeY;

    //맵을 리젼크기에 따라 리젼을 나눈다.
    //긴 거리를 이동해야 할때 맵 전체를 가지고 Astar알고리즘을 사용하는것이 아닌 
    //지역별로 연산을한다
    //각 지역별로는 인적한 지역에 도착할 수 있는지 없는지 정보를 가지고 있는다.
    //목적지가 있는 지역이 아닌 지나쳐가는 지역의 경우는 현재 위치와 목적지까지의 일직선의 접점을 리젼 내에서의 목적지로 정하고 움직인다.
    //일단은 벽이 있고 없고 상관하지 않고 움직인다.
    public void InitSetting()
    {

    }


    public Vector3 MyGetCellCenterWorld(int x, int y)
    {
        Vector3 pos = _tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0));
        return pos;
    }
    


    //셀의 인덱스를 주면 해당 셀의 월드 위치를 구해서 해당 위치고 레이를쏴준다. 
    //그러곤 충돌한 객체의 레이어가 wall이면 true를 리턴, 아니면 false를 리턴한다.
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
        return false;
    }


    public Vector3Int GetTileCellNum(Vector3 pos)//월드좌표를 보내주면 해당 좌표의 셀번호를 받아서 보내준다.
    {
        //노드를 만들어서 리턴해준다.
        Vector3Int temp = new Vector3Int(0, 0, 0);
        if (_tilemap != null)
        {
            temp = this._tilemap.WorldToCell(pos);
        }

        return temp;
    }


    //화면에서 보이는 만큼만 raycast를 이용해서 맵정보를 받아온다.
    public void ReadMapInfo(Vector3 start, Vector3 target)
    {
        Vector2Int startcell = Vector2Int.zero;
        Vector2Int targetcell = Vector2Int.zero;


    }
}
