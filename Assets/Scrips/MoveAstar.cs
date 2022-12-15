using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;


//전체 맵 구조를 받아와서 리전을 나눈다.
//리전을 나누는 규칙 
//1. 기본적으로 리전은 타일맵 8X8 의 크기로 나뉜다.
//2. 리젼 내에서의 이동은 8X8 크기에서의 a*이동을 사용한다.
//3. 하나의 리전 내에서는 어디든 어떻게든 움직일 수 있어야 한다.(리젼을 유기적으로 나누는 알고리즘이 필요)
//4. 리젼에서 다른 리젼으로 이동해야 하는경우는 리전끼리 A*알고리즘을 이용해 전체적인 경로를 구하고 세부적인 움직임은 Stupid Funnel 알고리즘을 적용한다.
//5. 하나의 리전 내에서 벽으로 막혀 갈 수 없는 경우가 있으면 리젼을 나눠준다.



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

    //맵을 넘어가지 않는 한 무조건 8*8 지역의 정보를 가지고 있는다.
    //그 중에서도 움직일 수 있는 지역을 하위 지역으로 가지고 있는다.
    [System.Serializable]
    public class Region
    {
        
        public int Num;
        public List<Region> NeighborRegions;
        public List<Region> MoveAbleNeighborRegions;
        //하나의 리전 안에 있는 리전들 각각의 로컬 리전을 나누는 기준은 길이 이어져 있어서 움직일 수 있는가 이다.
        //따라서 하나의 로컬 리전은 온전히 길로만 이루어져 있거나 온전히 벽으로만 이루어져 있다.
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

        //월드에서의 인덱스를 넣어주면 해당 영역 안에 존재하는지 확인해준다.
        public bool IsInside(int x,int y)
        {

            return false;
        }

        

        //다른 인접한 리전과 합친다.
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

    //벽으로된 리젼은 상관하지 않고
    //길로만 이루어진 리전들만 가지고 리전끼리의 이동이 가능한지 확인한다.
    //각 리전의 네 귀퉁이로부터 상하좌우 대각선 방향에 존재하는 리전을 찾아내서 길찾기를 실행해보고 움직일 수 있는지 없는지를 미리 확인한다. 


    //리젼을 나눠준다.
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

    //해당 지역 안에서 갈수있는 길과, 장애물이 모두 이어져 있는지 확인한다.
    //이어져있지 않으면 이어져있는것들끼리 리젼을 구성한다.
    public void CreateRegion(Vector3Int _bottomRight, Vector2Int _size)
    {
        Rect rect;
        Vector3Int bottomright;
        Vector2Int size;
        Vector2Int maxsize;
        //Region region = new Region(_bottomRight, _size);

        //맵 안에 있는지 확인하고 맵 안에 있는 범위만 짤라서 리전을 만들어 준다.
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
                    //방문한적이 없고
                    if (!IsCheck(x, y,bottomright.x,bottomright.y))
                    {
                        //Region region = new Region(bottomright, size);
                        Region localRegion = new Region(bottomright, size);
                        
                        //regioninputpos.Add(bottomright);

                        //벽이 아니면
                        if (!MapManager.Instance.IsWall(new Vector3Int(x, y, 0)))
                        {
                            //이어져있는 길들을 탐색해서 리젼을 만들어 준다.
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


        //상
        if (y + 1 < maxY && !IsCheck(x, y + 1, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y + 1, 0));
            if(iswall)
                CheckWall(region, x, y + 1, maxX, maxY);
        }
        //하
        if (rocalIndextY - 1 >= 0 && !IsCheck(x, y - 1, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y - 1, 0));
            if (iswall)
                CheckWall(region, x, y - 1, maxX, maxY);
        }
        //좌
        if (x + 1 < maxX && !IsCheck(x + 1, y, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x + 1, y, 0));
            if (iswall)
                CheckWall(region, x + 1, y, maxX, maxY);
        }
        //우
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

        //상
        if (y + 1 < maxY && !IsCheck(x, y + 1, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y + 1, 0));
            if (!iswall)
                CheckRoad(region, x, y + 1, maxX, maxY);
        }    
            
        //하
        if (rocalIndextY - 1 >= 0 && !IsCheck(x, y - 1, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y - 1, 0));
            if (!iswall)
                CheckRoad(region, x, y - 1, maxX, maxY);
        }
            
        //좌
        if (x + 1 < maxX && !IsCheck(x + 1, y, region.BottomRight.x, region.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x + 1, y, 0));
            if (!iswall)
                CheckRoad(region, x + 1, y, maxX, maxY);
        }
            
        //우
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

    //한 화면에 최대로 담기는 타일의 갯수가 가로,세로 10~11개 이기 때문에 이정도 크기로 고정하였다.
    public Node[,] NodeArray = new Node[30, 30];
    //시작노드, 목표 노드, 현재 탐색중인 노드
    public Node StartNode, TargetNode, CurNode;
    //현재 노드와 인접해있고 벽이 아닌, 이동 가능한 노드들의 리스트
    public List<Node> OpenList;
    //현재까지 지나온 노드들의 리스트
    public List<Node> ClosedList;
    //최종 경로
    public List<Node> FinalNodeList;
    

    public List<Node> test = new List<Node>();


    

    


    //시작 위치와 끝 위치를 받아와서 해당 크기만큼의 맵을 맵 매니저한테 받아온다.
    //그러곤 사용할 노드를 만들어 준다.
    //노드는 미리 만들어 두고 용량이 더 필요하면 새로 할당 받는다.
    //마우스가 클릭되면 현재 캐릭터의 월드위치가 시작점, 마우스의 클릭 월드위치가 목표지점으로 들어온다.
    public void PathSetting(Vector3 start, Vector3 target)
    {
        startcell = MapManager.Instance.GetTileCellNum(new Vector2(start.x, start.y));//시작지점과 목표지점을 셀번호로 받아온다.
        targetcell = MapManager.Instance.GetTileCellNum(new Vector2(target.x, target.y));

        bottomLeft = new Vector2Int(startcell.x, startcell.y);
        topRight = new Vector2Int(targetcell.x, targetcell.y);



        startPos = new Vector2Int(startcell.x, startcell.y);
        targetPos = new Vector2Int(targetcell.x, targetcell.y);


        int sizeX = targetcell.x - startcell.x;//시작지점과 목표지점을 이용해서 연산을 할 사이즈를 구한다.
        int sizeY = targetcell.y - startcell.y;
        if (sizeX < 0 || sizeY < 0)//둘중 하나라도 음수면 둘다 절댓값을 붙여준다.
        {
            sizeX = Mathf.Abs(sizeX);
            sizeY = Mathf.Abs(sizeY);
        }
        sizeX += 1;
        sizeY += 1;

        //항상 bottomleft와 topright 가 졍해져 있는 것이 아니기 때문에 출발지점과 도착지점의 위치에 따라 영역을 지정해주어야 한다.
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

        //벽정보를 받아와야 하기 때문에
        //지작점부터 끝점까지 벽정보인지 아닌지 받아와서 노드를 만들어 준다.
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool iswall = false;
                //시작점부터 끝점까지 타일맵의 셀들을 조사하면서 해당 셀이 벽인지 확인해서 노드에 넣어준다
                if (MapManager.Instance.IsWall(new Vector3Int(i + bottomLeft.x, j + bottomLeft.y, 0)))
                {
                    iswall = true;
                }

                NodeArray[i, j] = new Node(iswall, i + bottomLeft.x, j + bottomLeft.y);
                test.Add(NodeArray[i, j]);
            }
        }

        // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
        StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];


        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();

        //시작지점부터 목표지점까지의 공간과 벽정보들을 받아온다.
        //MapManager.GetI.ReadMapInfo(start, target);
    }


    public List<Node> PathFinding(Vector3 start, Vector3 target)
    {
        //초기 세팅 (시작점, 끝점, TopRight, BottomLeft 지정 리스트들 초기화 등등을 수행한다.)
        PathSetting(start, target);

        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];

            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) 
                    CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // 마지막
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


            // ↗↖↙↘
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }

            // ↑ → ↓ ←
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
        return null;
    }

    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // 대각선 허용시, 벽 사이로 통과 안됨
            if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // 이웃노드에 넣고, 수평으로 가는 직선은 16, 대각선은 14비용, 수직으로 가는 직선은 10
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            //int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 6);
            //int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);

            int MoveCost = 0;
            if (CurNode.x - checkX == 0)//세로로 이동
            {
                MoveCost = CurNode.G + 10;
            }
            else if (CurNode.y - checkY == 0)//가로로 이동
            {
                MoveCost = CurNode.G + 16;
            }
            else //나머지 대각선으로 이동
            {
                MoveCost = CurNode.G + 14;
            }

            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
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
