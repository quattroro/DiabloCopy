using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.Tilemaps;


//�ʿ� ���� �������� ������ ������ ��û�ϸ� �ش� �������� �����ش�.
//Ÿ�ϸ� ��ĭ�� ���α��� = 1, ���α��� 0.5
//�� ��ü ������ ������ �ִ´�.
//�� �� �������� ������ 
public class MapManager : Singleton<MapManager>
{
    public enum ACT { ACT1, ACT2, ACTMAX };
    public GameObject[,] MapTiles = null;


    public Vector3Int TopLeftIndex;
    public Vector3Int BottomRightIndex; // 0,0 BottomRight�� ����
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

    //���� ����ũ�⿡ ���� ������ ������.
    //�� �Ÿ��� �̵��ؾ� �Ҷ� �� ��ü�� ������ Astar�˰����� ����ϴ°��� �ƴ� 
    //�������� �������Ѵ�
    //�� �������δ� ������ ������ ������ �� �ִ��� ������ ������ ������ �ִ´�.
    //�������� �ִ� ������ �ƴ� �����İ��� ������ ���� ���� ��ġ�� ������������ �������� ������ ���� �������� �������� ���ϰ� �����δ�.
    //�ϴ��� ���� �ְ� ���� ������� �ʰ� �����δ�.
    public void InitSetting()
    {
        //��ü ���� ������ ������ �ִ´�.
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

    //�ش� �� ��ȣ�� �� �������� Ȯ���Ѵ�.
    public bool CheckBoundary(int x, int y)
    {
        if(x>=BottomRightIndex.x&&x<=TopLeftIndex.x&&y>=BottomRightIndex.y&&y<=TopLeftIndex.y)
            return true;

        return false;
    }


    //�ش� ������ �� �������� Ȯ���Ѵ�.
    //�� ���ʿ� ���� ������ �������� �������� ������ �״�� �������ְ�
    //�� �ȿ� ���� ���� ������ 
    //�� �ȿ� ���� �ִ� ��ŭ�� �ø��ؼ� �������ش�.
    //�ΰ��� �簢���� ��ģ�ٴ� ��
    //�ΰ��� �簢���� ��ġ�� ������ ���� ������ �� �ȿ� ���ٴ¶� �̶��� (0,0,0,0)�� ��������.
    public bool CheckBoundary(int x, int y, int sizeX, int sizeY , out Rect Bound)
    {
        Rect _bound = new Rect(0, 0, 0, 0);
        Bound = _bound;

        int endX = x + sizeX - 1;
        int endY = y + sizeY - 1;

        //��ġ�� �κ��� ������
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



        ////�������� �� ���� �ȿ� �ְ�
        //if (x >= BottomRightIndex.x && x <= TopLeftIndex.x && y >= BottomRightIndex.y && y <= TopLeftIndex.y)
        //{
        //    _bound.x = x;
        //    _bound.y = y;

        //    //������ �� ���� �ȿ� ������
        //    if (endX >= BottomRightIndex.x && endX <= TopLeftIndex.x && endY >= BottomRightIndex.y && endY <= TopLeftIndex.y)
        //    {
        //        _bound.width = sizeX;
        //        _bound.height = sizeY;
        //        Bound = _bound;
        //        return true;
        //    }
        //}

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
        return false;
    }


    public Vector3Int GetTileCellNum(Vector3 pos)//������ǥ�� �����ָ� �ش� ��ǥ�� ����ȣ�� �޾Ƽ� �����ش�.
    {
        //��带 ���� �������ش�.
        Vector3Int temp = new Vector3Int(0, 0, 0);
        if (_tilemap != null)
        {
            temp = this._tilemap.WorldToCell(pos);
        }

        return temp;
    }


    //ȭ�鿡�� ���̴� ��ŭ�� raycast�� �̿��ؼ� �������� �޾ƿ´�.
    public void ReadMapInfo(Vector3 start, Vector3 target)
    {
        Vector2Int startcell = Vector2Int.zero;
        Vector2Int targetcell = Vector2Int.zero;


    }
}
