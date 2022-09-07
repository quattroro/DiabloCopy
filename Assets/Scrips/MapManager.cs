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
