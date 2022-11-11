using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Tilemaps;


//맵에 관한 정보들을 가지고 정보를 요청하면 해당 정보들을 보내준다.
//타일맵 한칸의 가로길이 = 1, 세로길이 0.5
//맵 전체 정보를 가지고 있는다.
//맵 을 지역별로 나눠서 
public class MapManager : Singleton<MapManager>
{
    public enum ACT { ACT1, ACT2, ACTMAX };
    public GameObject[,] MapTiles = null;


    public Vector3Int TopLeftIndex;
    public Vector3Int BottomRightIndex; // 0,0 BottomRight가 원점
    public Vector2Int MapSize;

   


    public Transform BottomRight;
    public Transform TopLeft;




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
        //전체 맵의 정보를 가지고 있는다.
        BottomRight = this.transform.Find("BottomRight");
        TopLeft = this.transform.Find("TopLeft");

        BottomRightIndex = GetTileCellNum(new Vector2(BottomRight.position.x, BottomRight.position.y));
        TopLeftIndex = GetTileCellNum(new Vector2(TopLeft.position.x, TopLeft.position.y));

        MapSize.x = TopLeftIndex.x - BottomRightIndex.x;
        MapSize.y = TopLeftIndex.y - BottomRightIndex.y;


    }


    public Vector3 MyGetCellCenterWorld(int x, int y)
    {
        Vector3 pos = _tilemap.GetCellCenterWorld(new Vector3Int(x, y, 0));
        return pos;
    }

    //해당 셀 번호가 맵 안쪽인지 확인한다.
    public bool CheckBoundary(int x, int y)
    {
        if(x>=BottomRightIndex.x&&x<=TopLeftIndex.x&&y>=BottomRightIndex.y&&y<=TopLeftIndex.y)
            return true;

        return false;
    }


    //해당 영역이 맵 안쪽인지 확인한다.
    //맵 안쪽에 들어와 있으면 리턴으로 시작점과 사이즈 그대로 내보내주고
    //맵 안에 들어와 있지 않으면 
    //맵 안에 들어와 있는 만큼만 컬링해서 내보내준다.
    //두개의 사각형이 겹친다는 뜻
    //두개의 사각형이 겹치지 않으면 지정 영역은 맵 안에 없다는뜻 이때는 (0,0,0,0)을 내보낸다.
    public bool CheckBoundary(int x, int y, int sizeX, int sizeY , out Rect Bound)
    {
        Rect _bound = new Rect(0, 0, 0, 0);
        Bound = _bound;

        int endX = x + sizeX - 1;
        int endY = y + sizeY - 1;

        //겹치는 부분이 없을때
        if (x > TopLeftIndex.x) return false;
        if (endX < BottomRightIndex.x) return false;
        if (y > TopLeftIndex.y) return false;
        if (endY < BottomRightIndex.y) return false;


        _bound.x = Mathf.Max(x, BottomRightIndex.x);
        _bound.y = Mathf.Max(y, BottomRightIndex.y);
        _bound.width = Mathf.Min(endX, TopLeftIndex.x) - _bound.x;
        _bound.height = Mathf.Min(endY, TopLeftIndex.y) - _bound.y;

        Bound = _bound;
        return true;



        ////시작점이 맵 범위 안에 있고
        //if (x >= BottomRightIndex.x && x <= TopLeftIndex.x && y >= BottomRightIndex.y && y <= TopLeftIndex.y)
        //{
        //    _bound.x = x;
        //    _bound.y = y;

        //    //끝점도 맵 범위 안에 있으면
        //    if (endX >= BottomRightIndex.x && endX <= TopLeftIndex.x && endY >= BottomRightIndex.y && endY <= TopLeftIndex.y)
        //    {
        //        _bound.width = sizeX;
        //        _bound.height = sizeY;
        //        Bound = _bound;
        //        return true;
        //    }
        //}

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
