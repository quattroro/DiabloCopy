using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;


//��ü �� ������ �޾ƿͼ� ������ ������.
//������ ������ ��Ģ 
//1. �⺻������ ������ Ÿ�ϸ� 8X8 �� ũ��� ������.
//2. ���� �������� �̵��� 8X8 ũ�⿡���� a*�̵��� ����Ѵ�.
//3. �ϳ��� ���� �������� ���� ��Ե� ������ �� �־�� �Ѵ�.(������ ���������� ������ �˰����� �ʿ�)
//4. �������� �ٸ� �������� �̵��ؾ� �ϴ°��� �������� A*�˰����� �̿��� ��ü���� ��θ� ���ϰ� �������� �������� Stupid Funnel �˰����� �����Ѵ�.
//5. �ϳ��� ���� ������ ������ ���� �� �� ���� ��찡 ������ ������ �����ش�.



public class MoveAstar : MonoBehaviour
{
    protected static MoveAstar m_StarInstance = null;

    public static MoveAstar GetI
    {
        get
        {
            if (m_StarInstance == null)
            {
                m_StarInstance = GameObject.FindObjectOfType<MoveAstar>();
            }
            return m_StarInstance;
        }
    }

    //public List<Vector3> regioninputpos = new List<Vector3>();
    public Vector2Int RegionSize;

    //���� �Ѿ�� �ʴ� �� ������ 8*8 ������ ������ ������ �ִ´�.
    //�� �߿����� ������ �� �ִ� ������ ���� �������� ������ �ִ´�.
    [System.Serializable]
    public class Region
    {
        
        public int Num;
        public List<Region> NeighborRegions;
        public List<Region> MoveAbleNeighborRegions;
        //�ϳ��� ���� �ȿ� �ִ� ������ ������ ���� ������ ������ ������ ���� �̾��� �־ ������ �� �ִ°� �̴�.
        //���� �ϳ��� ���� ������ ������ ��θ� �̷���� �ְų� ������ �����θ� �̷���� �ִ�.
        public List<Region> LocalRegion;

        public Vector3Int Local_TopLeft = new Vector3Int(-1,-1,-1);
        public Vector3Int Local_BottomRight = new Vector3Int(-1, -1, -1);
        public Vector3Int Local_TopRight = new Vector3Int(-1, -1, -1);
        public Vector3Int Local_BottomLeft = new Vector3Int(-1, -1, -1);

        public Vector3Int BottomRight = new Vector3Int(-1, -1, -1);
        public Vector2Int RegionSize = new Vector2Int(-1, -1);

        public Node[,] NodeArray;

        public Color color;


        public Region(Vector3Int _bottomRight, Vector2Int _size)
        {
            color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            BottomRight = _bottomRight;
            RegionSize = _size;
            //TopLeft = new Vector3Int(_bottomRight.x + _size.x - 1, _bottomRight.y + _size.y - 1,0);
            NodeArray = new Node[_size.x, _size.y];
            LocalRegion = new List<Region>();
        }


        public Vector3Int GetLocalBottomRight()
        {
            if(Local_TopRight == new Vector3Int(-1,-1,-1))
            {

            }
            return Local_TopRight;
        }

        public Vector3Int GetLocalTopLeft()
        {
            if (Local_TopLeft == new Vector3Int(-1, -1, -1))
            {

            }

            return Local_TopRight;
        }

        public Vector3Int GetLocalTopRight()
        {
            if (Local_TopLeft == new Vector3Int(-1, -1, -1))
            {

            }

            return Local_TopRight;
        }

        public Vector3Int GetLocalBottomLeft()
        {
            if (Local_TopLeft == new Vector3Int(-1, -1, -1))
            {

            }

            return Local_TopRight;
        }



        public Vector3Int GetBottomRight()
        {
            return BottomRight;
        }


        public Vector2Int GetSize()
        {
            return RegionSize;
        }

        //���忡���� �ε����� �־��ָ� �ش� ���� �ȿ� �����ϴ��� Ȯ�����ش�.
        public bool IsInside(int x,int y)
        {

            return false;
        }

        

        //�ٸ� ������ ������ ��ģ��.
        public void MergeRegion(Region region)
        {

        }

        
        public void SetRegion()
        {

        }

    }

    public void Start()
    {
        InitSetting();
    }

    //�����ε� ������ ������� �ʰ�
    //��θ� �̷���� �����鸸 ������ ���������� �̵��� �������� Ȯ���Ѵ�.
    //�� ������ �� �����̷κ��� �����¿� �밢�� ���⿡ �����ϴ� ������ ã�Ƴ��� ��ã�⸦ �����غ��� ������ �� �ִ��� �������� �̸� Ȯ���Ѵ�. 


    //������ �����ش�.
    public void InitSetting()
    {
        Vector3Int MapTopLeft = MapManager.Instance.TopLeftIndex;
        Vector3Int MapBottomRight = MapManager.Instance.BottomRightIndex;
        Vector2Int MapSize = MapManager.Instance.MapSize;


        for (int y = MapBottomRight.y; y < MapTopLeft.y; y = y + RegionSize.y)
        {
            for (int x = MapBottomRight.x; x < MapTopLeft.x; x = x + RegionSize.x)
            {
                CreateRegion(new Vector3Int(x, y, 0), new Vector2Int(8, 8));
            }
        }

    }

    public Stack<int> stack = new Stack<int>();
    public bool[,] Ck = new bool[8, 8];
    public List<Region> Regions = new List<Region>();
    public int[,] dir = { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };

    public bool IsCheck(int x,int y, int padX, int padY)
    {
        
        return Ck[x - padX, y - padY];
    }

    public void SetCkech(int x, int y, int padX, int padY, bool val)
    {
        Ck[x - padX, y - padY] = val;
    }

    //�ش� ���� �ȿ��� �����ִ� ���, ��ֹ��� ��� �̾��� �ִ��� Ȯ���Ѵ�.
    //�̾������� ������ �̾����ִ°͵鳢�� ������ �����Ѵ�.
    public void CreateRegion(Vector3Int _bottomRight, Vector2Int _size)
    {
        Rect rect;
        Vector3Int bottomright;
        Vector2Int size;
        Vector2Int maxsize;
        //Region region = new Region(_bottomRight, _size);

        //�� �ȿ� �ִ��� Ȯ���ϰ� �� �ȿ� �ִ� ������ ©�� ������ ����� �ش�.
        if (MapManager.Instance.CheckBoundary(_bottomRight.x, _bottomRight.y, _size.x, _size.y, out rect))
        {
            bottomright = new Vector3Int((int)rect.x, (int)rect.y, 0);
            size = new Vector2Int((int)rect.width, (int)rect.height);
            maxsize = new Vector2Int((int)rect.x + (int)rect.width, (int)rect.y + (int)rect.height);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Ck[i, j] = false;
                }
            }

            Region region = new Region(bottomright, size);
            Regions.Add(region);

            for (int y = bottomright.y; y < maxsize.y; y++)
            {
                for (int x = bottomright.x; x < maxsize.x; x++)
                {
                    //�湮������ ����
                    if (!IsCheck(x, y,bottomright.x,bottomright.y))
                    {
                        //Region region = new Region(bottomright, size);
                        Region localRegion = new Region(bottomright, size);
                        
                        //regioninputpos.Add(bottomright);

                        //���� �ƴϸ�
                        if (!MapManager.Instance.IsWall(new Vector3Int(x, y, 0)))
                        {
                            //�̾����ִ� ����� Ž���ؼ� ������ ����� �ش�.
                            CheckRoad(localRegion, x, y, bottomright.x + size.x, bottomright.y + size.y);
                        }
                        else
                        {
                            region.color = new Color(1.0f, 1.0f, 1.0f);
                            CheckWall(localRegion, x, y, bottomright.x + size.x, bottomright.y + size.y);
                        }

                        region.LocalRegion.Add(localRegion);
                    }

                }
            }
        }
        else
        {
            return;
        }

        
        
    }

    public void CheckWall(Region region, int x,int y,int maxX,int maxY)
    {
        int rocalIndextX = x - region.BottomRight.x;
        int rocalIndextY = y - region.BottomRight.y;

        SetCkech(x, y, region.BottomRight.x, region.BottomRight.y, true);
        region.NodeArray[rocalIndextX, rocalIndextY] = new Node(true, x, y);
        bool iswall = false;


        //��
        if (y + 1 < maxY && !IsCheck(x, y + 1, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y + 1, 0));
            if(iswall)
                CheckWall(region, x, y + 1, maxX, maxY);
        }
        //��
        if (rocalIndextY - 1 >= 0 && !IsCheck(x, y - 1, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y - 1, 0));
            if (iswall)
                CheckWall(region, x, y - 1, maxX, maxY);
        }
        //��
        if (x + 1 < maxX && !IsCheck(x + 1, y, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x + 1, y, 0));
            if (iswall)
                CheckWall(region, x + 1, y, maxX, maxY);
        }
        //��
        if (rocalIndextX - 1 >= 0 && !IsCheck(x - 1, y, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x - 1, y, 0));
            if (iswall)
                CheckWall(region, x - 1, y, maxX, maxY);
        }
            
    }

    public void CheckRoad(Region region, int x, int y,int maxX,int maxY)
    {
        int rocalIndextX = x - region.BottomRight.x;
        int rocalIndextY = y - region.BottomRight.y;

        SetCkech(x, y, region.BottomRight.x, region.BottomRight.y, true);
        region.NodeArray[rocalIndextX, rocalIndextY] = new Node(false, x, y);
        bool iswall = false;

        //��
        if (y + 1 < maxY && !IsCheck(x, y + 1, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y + 1, 0));
            if (!iswall)
                CheckRoad(region, x, y + 1, maxX, maxY);
        }    
            
        //��
        if (rocalIndextY - 1 >= 0 && !IsCheck(x, y - 1, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y - 1, 0));
            if (!iswall)
                CheckRoad(region, x, y - 1, maxX, maxY);
        }
            
        //��
        if (x + 1 < maxX && !IsCheck(x + 1, y, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x + 1, y, 0));
            if (!iswall)
                CheckRoad(region, x + 1, y, maxX, maxY);
        }
            
        //��
        if (rocalIndextX - 1 >= 0 && !IsCheck(x - 1, y, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x - 1, y, 0));
            if (!iswall)
                CheckRoad(region, x - 1, y, maxX, maxY);
        }
            
    }



    Vector2Int bottomLeft, topRight, startPos, targetPos;
    
    public bool allowDiagonal, dontCrossCorner;
    int sizeX, sizeY;
    public float cellsizeX = 1;
    public float cellsizeY = 0.5f;
    
    public Vector3Int startcell, targetcell;

    //�� ȭ�鿡 �ִ�� ���� Ÿ���� ������ ����,���� 10~11�� �̱� ������ ������ ũ��� �����Ͽ���.
    public Node[,] NodeArray = new Node[30, 30];
    //���۳��, ��ǥ ���, ���� Ž������ ���
    public Node StartNode, TargetNode, CurNode;
    //���� ���� �������ְ� ���� �ƴ�, �̵� ������ ������ ����Ʈ
    public List<Node> OpenList;
    //������� ������ ������ ����Ʈ
    public List<Node> ClosedList;
    //���� ���
    public List<Node> FinalNodeList;
    

    public List<Node> test = new List<Node>();


    

    


    //���� ��ġ�� �� ��ġ�� �޾ƿͼ� �ش� ũ�⸸ŭ�� ���� �� �Ŵ������� �޾ƿ´�.
    //�׷��� ����� ��带 ����� �ش�.
    //���� �̸� ����� �ΰ� �뷮�� �� �ʿ��ϸ� ���� �Ҵ� �޴´�.
    //���콺�� Ŭ���Ǹ� ���� ĳ������ ������ġ�� ������, ���콺�� Ŭ�� ������ġ�� ��ǥ�������� ���´�.
    public void PathSetting(Vector3 start, Vector3 target)
    {
        startcell = MapManager.Instance.GetTileCellNum(new Vector2(start.x, start.y));//���������� ��ǥ������ ����ȣ�� �޾ƿ´�.
        targetcell = MapManager.Instance.GetTileCellNum(new Vector2(target.x, target.y));

        bottomLeft = new Vector2Int(startcell.x, startcell.y);
        topRight = new Vector2Int(targetcell.x, targetcell.y);



        startPos = new Vector2Int(startcell.x, startcell.y);
        targetPos = new Vector2Int(targetcell.x, targetcell.y);


        int sizeX = targetcell.x - startcell.x;//���������� ��ǥ������ �̿��ؼ� ������ �� ����� ���Ѵ�.
        int sizeY = targetcell.y - startcell.y;
        if (sizeX < 0 || sizeY < 0)//���� �ϳ��� ������ �Ѵ� ������ �ٿ��ش�.
        {
            sizeX = Mathf.Abs(sizeX);
            sizeY = Mathf.Abs(sizeY);
        }
        sizeX += 1;
        sizeY += 1;

        //�׻� bottomleft�� topright �� ������ �ִ� ���� �ƴϱ� ������ ��������� ���������� ��ġ�� ���� ������ �������־�� �Ѵ�.
        if (targetcell.x < startcell.x && targetcell.y >= startcell.y)
        {
            bottomLeft = new Vector2Int(targetcell.x, startcell.y);
            topRight = new Vector2Int(startcell.x, targetcell.y);
        }
        else if (targetcell.x >= startcell.x && targetcell.y < startcell.y)
        {
            bottomLeft = new Vector2Int(startcell.x, targetcell.y);
            topRight = new Vector2Int(targetcell.x, startcell.y);
        }
        else if (targetcell.x < startcell.x && targetcell.y < startcell.y)
        {
            bottomLeft = new Vector2Int(targetcell.x, targetcell.y);
            topRight = new Vector2Int(startcell.x, startcell.y);
        }



        //NodeArray = new Node[sizeX, sizeY];

        //�������� �޾ƿ;� �ϱ� ������
        //���������� �������� ���������� �ƴ��� �޾ƿͼ� ��带 ����� �ش�.
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool iswall = false;
                //���������� �������� Ÿ�ϸ��� ������ �����ϸ鼭 �ش� ���� ������ Ȯ���ؼ� ��忡 �־��ش�
                if (MapManager.Instance.IsWall(new Vector3Int(i + bottomLeft.x, j + bottomLeft.y, 0)))
                {
                    iswall = true;
                }

                NodeArray[i, j] = new Node(iswall, i + bottomLeft.x, j + bottomLeft.y);
                test.Add(NodeArray[i, j]);
            }
        }

        // ���۰� �� ���, ��������Ʈ�� ��������Ʈ, ����������Ʈ �ʱ�ȭ
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];


        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();

        //������������ ��ǥ���������� ������ ���������� �޾ƿ´�.
        //MapManager.GetI.ReadMapInfo(start, target);
    }


    public List<Node> PathFinding(Vector3 start, Vector3 target)
    {
        //�ʱ� ���� (������, ����, TopRight, BottomLeft ���� ����Ʈ�� �ʱ�ȭ ����� �����Ѵ�.)
        PathSetting(start, target);

        while (OpenList.Count > 0)
        {
            // ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű��
            CurNode = OpenList[0];

            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) 
                    CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // ������
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                return FinalNodeList;
            }


            // �֢آע�
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }

            // �� �� �� ��
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
        return null;
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // �����¿� ������ ����� �ʰ�, ���� �ƴϸ鼭, ��������Ʈ�� ���ٸ�
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // �밢�� ����, �� ���̷� ��� �ȵ�
            if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // �ڳʸ� �������� ���� ������, �̵� �߿� �������� ��ֹ��� ������ �ȵ�
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // �̿���忡 �ְ�, �������� ���� ������ 16, �밢���� 14���, �������� ���� ������ 10
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            //int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 6);
            //int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);

            int MoveCost = 0;
            if (CurNode.x - checkX == 0)//���η� �̵�
            {
                MoveCost = CurNode.G + 10;
            }
            else if (CurNode.y - checkY == 0)//���η� �̵�
            {
                MoveCost = CurNode.G + 16;
            }
            else //������ �밢������ �̵�
            {
                MoveCost = CurNode.G + 14;
            }

            // �̵������ �̿����G���� �۰ų� �Ǵ� ��������Ʈ�� �̿���尡 ���ٸ� G, H, ParentNode�� ���� �� ��������Ʈ�� �߰�
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    

    void OnDrawGizmos()
    {
        if(FinalNodeList!=null)
        {
            if (FinalNodeList.Count != 0)
            {
                for (int i = 0; i < FinalNodeList.Count - 1; i++)
                {
                    //Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
                    Gizmos.DrawLine(MapManager.Instance.MyGetCellCenterWorld(FinalNodeList[i].x, FinalNodeList[i].y), MapManager.Instance.MyGetCellCenterWorld(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
                }

            }
        }
        Vector3 _topright = MapManager.Instance.MyGetCellCenterWorld(topRight.x, topRight.y);
        Vector3 _bottomleft = MapManager.Instance.MyGetCellCenterWorld(bottomLeft.x, bottomLeft.y);
        //Gizmos.color = Color.gray;
        //Gizmos.DrawLine(transform.position, _topright);
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawLine(transform.position, _bottomleft);
        //for(int i=0;i< regioninputpos.Count;i++)
        //{
        //    Vector3 temp = MapManager.Instance.MyGetCellCenterWorld((int)regioninputpos[i].x, (int)regioninputpos[i].y);
        //    Gizmos.DrawCube(temp, new Vector3(0.2f, 0.2f, 0.2f));
        //}

        DrawRegion();
    }

    //Color[] color = { { 1.0f,1.0f,1.0f,1.0f}, }

    void DrawRegion()
    {

        for (int i = 0; i < Regions.Count; i++)
        {
            
            foreach (Region region in Regions[i].LocalRegion)
            {
                Gizmos.color = region.color;
                foreach (Node a in region.NodeArray)
                {
                    if (a != null)
                    {
                        Vector3 temp = MapManager.Instance.MyGetCellCenterWorld(a.x, a.y);

                        Gizmos.DrawCube(temp, new Vector3(0.2f, 0.2f, 0.2f));
                    }

                }
            }
        }
    }

    void DrawGizmoRect(Vector3 bottomleft,Vector2 size)
    {

    }

    
}
