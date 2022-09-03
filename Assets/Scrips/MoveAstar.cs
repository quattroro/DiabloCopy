using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAstar : MonoBehaviour
{
    //맵 매니저는 맵의 정보를 얻을려고 할때 빈번하게 필요하기 때문에 싱글톤으로 구성
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


    Vector2Int bottomLeft, topRight, startPos, targetPos;
    
    public bool allowDiagonal, dontCrossCorner;
    int sizeX, sizeY;
    public float cellsizeX = 1;
    public float cellsizeY = 0.5f;
    public Node[,] NodeArray = new Node[30, 30];//한 화면에 최대로 담기는 타일의 갯수가 가로,세로 10~11개 이기 때문에 이정도 크기로 함
    public Node StartNode, TargetNode, CurNode;
    public List<Node> OpenList, ClosedList;
    public Vector3Int startcell, targetcell;
    
    public List<Node> FinalNodeList;

    public List<Node> test = new List<Node>();

    

    

    //시작 위치와 끝 위치를 받아와서 해당 크기만큼의 맵을 맵 매니저한테 받아온다.
    //그러곤 사용할 노드를 만들어 준다.
    //노드는 미리 만들어 두고 용량이 더 필요하면 새로 할당 받는다.
    //마우스가 클릭되면 현재 캐릭터의 월드위치가 시작점, 마우스의 클릭 월드위치가 목표지점으로 들어온다.
    public void InitSetting(Vector3 start, Vector3 target)
    {
        startcell = MapManager.GetI.GetTileCellNum(new Vector2(start.x, start.y));//시작지점과 목표지점을 셀번호로 받아온다.
        targetcell = MapManager.GetI.GetTileCellNum(new Vector2(target.x, target.y));
        
        bottomLeft = new Vector2Int(startcell.x, startcell.y);
        topRight = new Vector2Int(targetcell.x, targetcell.y);
        startPos = new Vector2Int(startcell.x,startcell.y);
        targetPos = new Vector2Int(targetcell.x, targetcell.y);


        int sizeX = targetcell.x - startcell.x;//시작지점과 목표지점을 이용해서 연산을 할 사이즈를 구한다.
        int sizeY = targetcell.y - startcell.y;
        if (sizeX < 0 || sizeY < 0)//둘중 하나라도 음수면 그냥 둘다 절댓값을 붙여준다.
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
            for(int j=0;j<sizeY;j++)
            {
                bool iswall = false;
                //시작점부터 끝점까지 타일맵의 셀들을 조사하면서 해당 셀이 벽인지 확인해서 노드에 넣어준다
                if (MapManager.GetI.IsWall(new Vector3Int(i + bottomLeft.x, j + bottomLeft.y, 0))) 
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
        InitSetting(start, target);

        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];

            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

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

                //for (int i = 0; i < FinalNodeList.Count; i++) print(i + "번째는 " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
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


            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            //int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 6);
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);

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
                    Gizmos.DrawLine(MapManager.GetI.MyGetCellCenterWorld(FinalNodeList[i].x, FinalNodeList[i].y), MapManager.GetI.MyGetCellCenterWorld(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
                }

            }
        }
        
            
    }


    
}
