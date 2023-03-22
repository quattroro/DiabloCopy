//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AstarModule : MonoBehaviour/*: Singleton<AstarModule>*/
{
    //public List<Vector3> regioninputpos = new List<Vector3>();
    public Vector2Int RegionSize;

    #region RegionClass

    [System.Serializable]
    public class testregion
    {

        public List<testlocalregion> LocalRegion = new List<testlocalregion>();

        //월드 타일맵에서 해당 리전의 위치
        public Vector3Int BottomRight = new Vector3Int(-1, -1, -1);
        public Vector3Int TopLeft = new Vector3Int(-1, -1, -1);
        public Vector3Int TopRight = new Vector3Int(-1, -1, -1);
        public Vector3Int BottomLeft = new Vector3Int(-1, -1, -1);

        public Vector2Int regionIndex;
        //하나의 큰 리전의 크기
        public Vector2Int RegionSize = new Vector2Int(-1, -1);

        public Node StartNode;

        
        public testregion(Vector3Int _bottomRight, Vector2Int _size)
        {

            TopLeft = new Vector3Int(_bottomRight.x + _size.x - 1, _bottomRight.y + _size.y - 1, 0);
            BottomRight = _bottomRight;

            BottomLeft = new Vector3Int(TopLeft.x, BottomRight.y, 0);
            TopRight = new Vector3Int(BottomRight.x, TopLeft.y, 0);

            RegionSize = _size;

            //NodeArray = new Node[_size.x, _size.y];
            //LocalRegion = new List<LocalRegion>();

            //for (int i = 0; i < (int)EnumTypes.Dir.DirMax; i++)
            //{
            //    Check[i] = false;
            //}

            //testlist.Contains
            //NodeArray.

        }

    }

    [System.Serializable]
    public class testlocalregion
    {
        //public List<KeyValuePair<testlocalregion, int>> neighbor = new List<KeyValuePair<testlocalregion, int>>();
        public List<Node> neighborstartnode = new List<Node>();
        public List<int> neighbordis = new List<int>();
        public Node StartNode;
    }

    [SerializeField]
    public testregion[] testRegions;

    //맵을 넘어가지 않는 한 무조건 8*8 지역의 정보를 가지고 있는다.
    //그 중에서도 움직일 수 있는 지역을 하위 지역으로 가지고 있는다.
    //[System.Serializable]
    public class Region
    {
        public int Num;
        public Vector2Int regionIndex;

        //하나의 리전 안에 있는 리전들 각각의 로컬 리전을 나누는 기준은 길이 이어져 있어서 움직일 수 있는가 이다.
        //따라서 하나의 로컬 리전은 온전히 길로만 이루어져 있거나 온전히 벽으로만 이루어져 있다.
        //public List<Region> LocalRegion;
        public List<LocalRegion> LocalRegion;



        //해당 방향에 있는 리전이랑 이동이 가능한지 판단
        public bool[] Check = new bool[(int)EnumTypes.Dir.DirMax];
        public Region[] NeighborRegion = new Region[(int)EnumTypes.Dir.DirMax];
        public int[] NeighborDistance = new int[(int)EnumTypes.Dir.DirMax];


        ////해당 방향이 체크가 되어 있는지
        //public bool[] IsChecked = new bool[8];

        public Vector3Int Local_TopLeft = new Vector3Int(-1, -1, -1);
        public Vector3Int Local_BottomRight = new Vector3Int(-1, -1, -1);
        public Vector3Int Local_TopRight = new Vector3Int(-1, -1, -1);
        public Vector3Int Local_BottomLeft = new Vector3Int(-1, -1, -1);


        //월드 타일맵에서 해당 리전의 위치
        public Vector3Int BottomRight = new Vector3Int(-1, -1, -1);
        public Vector3Int TopLeft = new Vector3Int(-1, -1, -1);
        public Vector3Int TopRight = new Vector3Int(-1, -1, -1);
        public Vector3Int BottomLeft = new Vector3Int(-1, -1, -1);

        //하나의 큰 리전의 크기
        public Vector2Int RegionSize = new Vector2Int(-1, -1);


        public Node[,] NodeArray;

        public Node StartNode;

        public Color color;

        public List<Node> testlist;

        public GrapheNode grapheNode;


        public Region(Vector3Int _bottomRight, Vector2Int _size)
        {
            color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));


            TopLeft = new Vector3Int(_bottomRight.x + _size.x - 1, _bottomRight.y + _size.y - 1, 0);
            BottomRight = _bottomRight;

            BottomLeft = new Vector3Int(TopLeft.x, BottomRight.y, 0);
            TopRight= new Vector3Int(BottomRight.x, TopLeft.y, 0);

            RegionSize = _size;

            NodeArray = new Node[_size.x, _size.y];
            LocalRegion = new List<LocalRegion>();

            for(int i=0;i< (int)EnumTypes.Dir.DirMax;i++)
            {
                Check[i] = false;
            }


            //testlist.Contains
            //NodeArray.

        }

        //
        public LocalRegion FindLacalRegion(int xpos,int ypos)
        {
            foreach(LocalRegion r in LocalRegion)
            {
                for(int y=0;y<RegionSize.y;y++)
                {
                    for(int x=0;x<RegionSize.x;x++)
                    {
                        if(r.NodeArray[x,y]!=null)
                        {
                            if(r.NodeArray[x,y].x == xpos&& r.NodeArray[x, y].y == ypos)
                            {
                                return r;
                            }
                        }
                    }
                }
            }
            return null;
        }

        //해당 위치가 해당 리전에 속해 있는지
        public bool IsInside(Vector3 pos)
        {
            return false;
        }

        //해당 위치가 해당 리전에 속해 있는지
        public bool IsInside(Vector3Int pos)
        {
            return false;
        }

        //월드에서의 인덱스를 넣어주면 해당 영역 안에 존재하는지 확인해준다.
        public bool IsInside(int x, int y)
        {

            return false;
        }

        //public void SetNeighborRegion(Region region, int dir)
        //{
        //    int dir_reverse = EnumTypes.Dir_Reverse[dir];

        //    if (this.IsMoveable[dir]!=-1)
        //    {
        //        return;
        //    }

        //    if(region==null)
        //    {
        //        this.IsMoveable[dir] = 0;
        //        region.IsMoveable[dir_reverse] = 0;
                
        //    }
        //    else
        //    {
        //        this.IsMoveable[dir] = 1;
        //        region.IsMoveable[dir_reverse] = 1;

        //        this.NeighborRegion[dir] = region;
        //        region.NeighborRegion[dir_reverse] = this;
        //    }

        //}

        public Vector3Int GetBottomRight()
        {
            return BottomRight;
        }


        public Vector2Int GetSize()
        {
            return RegionSize;
        }

        



        //다른 인접한 리전과 합친다.
        public void MergeRegion(Region region)
        {

        }


        public void SetRegion()
        {

        }

    }

    //로컬리전은 일정하지 않은 다각형 모양이기 때문에 각각의 vertex정보를 가지고 있는다.
    public class LocalRegion
    {
        public List<Vector2> vertex;
        public Vector2 center;


        //이동 가능한 인접 로컬 리전과 거리가 주어진다.
        public List<KeyValuePair<LocalRegion,int>> neighberRegions;
        public Region parentRegion;

        //// 부모와 똑같은 크기의 노드를 가지고 있지만 자신의 노드가 아닌것은 
        public Node[,] NodeArray;

        public Node StartNode;

        public Color color;


        //해당 방향에 있는 리전이랑 이동이 가능한지 판단
        public int[] IsMoveable = new int[(int)EnumTypes.Dir.DirMax];

        

        public LocalRegion(Region parentRegion)
        {
            color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

            this.parentRegion = parentRegion;

            NodeArray = new Node[parentRegion.RegionSize.x, parentRegion.RegionSize.y];

            //LocalRegion = new List<LocalRegion>();

            neighberRegions = new List<KeyValuePair<LocalRegion, int>>();

            for (int i = 0; i < (int)EnumTypes.Dir.DirMax; i++)
            {
                IsMoveable[i] = -1;
            }


            //testlist.Contains
            //NodeArray.

        }
    }

    //public struct Pair
    //{
    //    public LocalRegion region;
    //    public int distance;
    //}

    #endregion

    #region RegionSettings
    //벽으로된 리젼은 상관하지 않고
    //길로만 이루어진 리전들만 가지고 리전끼리의 이동이 가능한지 확인한다.
    //각 리전의 네 귀퉁이로부터 상하좌우 대각선 방향에 존재하는 리전을 찾아내서 길찾기를 실행해보고 움직일 수 있는지 없는지를 미리 확인한다. 


    //리젼을 나눠준다.
    public void InitSetting()
    {
        Vector3Int MapTopLeft = MapManager.Instance.TopLeftIndex;
        Vector3Int MapBottomRight = MapManager.Instance.BottomRightIndex;
        Vector2Int MapSize = MapManager.Instance.MapSize;

        RegionsSize = new Vector2Int(((MapSize.x / RegionSize.x) + 1),((MapSize.y / RegionSize.y) + 1));
        Regions = new Region[RegionsSize.x * RegionsSize.y];
        testRegions = new testregion[RegionsSize.x * RegionsSize.y];

        //리전들을 만들어 주고 
        for (int y = MapBottomRight.y; y < MapTopLeft.y; y = y + RegionSize.y)
        {
            for (int x = MapBottomRight.x; x < MapTopLeft.x; x = x + RegionSize.x)
            {
                CreateRegion(new Vector3Int(x, y, 0), new Vector2Int(8, 8));
            }
        }

        ////리전들을 만들어 주고 
        //for (int y = MapBottomRight.y; y < MapTopLeft.y; y = y + RegionSize.y)
        //{
        //    for (int x = MapBottomRight.x; x < MapTopLeft.x; x = x + RegionSize.x)
        //    {
        //        CreateRegion(new Vector3Int(x, y, 0), new Vector2Int(8, 8));
        //    }
        //}

        //해당 리전들이 서로 이동할 수 있는지 없는지 확인한다.
        //해당 리전의 

        RegionInitSetting();

    }



    //public List<List<bool>>[,] NodeArray;
    //public List<List<bool>> Resgiontestlist = new List<List<bool>>();
    //public List<bool> testlistdir;

    public int GrapheMax = 30;
    //각 인접 리전들끼리 이동이 가능한지, 이동이 가능하다면 거리가 얼마인지를 확인한다.
    public void RegionInitSetting()
    {
        GrapheWeight = new int[GrapheMax, GrapheMax];

        for(int i=0;i< GrapheMax; i++)
        {
            for(int j=0;j< GrapheMax; j++)
            {
                GrapheWeight[j, i] = 0;
            }
        }

        if (Regions.Length <= 0 || Regions == null)
        {
            Debug.Log("Regions is null");
            return;
        }

        int destx = 0;
        int desty = 0;

        int rdestx = 0;
        int rdesty = 0;

        int y = 0;
        int x = 0;


        Vector2Int topRight = Vector2Int.zero;
        Vector2Int bottomLeft = Vector2Int.zero;

        

        //하나의 리전의 8방향에 있는 리전들의 하위 리전들 끼리 모두 움직일 수 있는지 확인한다 
        for (y = 0; y < RegionsSize.y; y++)
        {
            for(x = 0; x < RegionsSize.x; x++)
            {
                //testlistdir = new List<bool>();
                //bool[] check = { false, false, false, false, false, false, false, false };

                for (int dir = 0; dir < 8; dir++)
                {
                    //Debug.Log($"{x + (y * RegionsSize.x)} 번 리전 {dir} 방향 검사");

                    destx = x + EnumTypes.dir_all[dir].x;
                    desty = y + EnumTypes.dir_all[dir].y;

                    rdestx = x + EnumTypes.r_dir_all[dir].x;
                    rdesty = y + EnumTypes.r_dir_all[dir].y;


                    //리전배열 안쪽이고
                    if (destx < 0 || destx >= RegionsSize.x || desty < 0 || desty >= RegionsSize.y)
                    {
                        //Debug.Log($"{x + (y * RegionsSize.x)} 번 리전 {dir} 방향 검사 안함");
                        continue;
                    }



                    //해당 방향을 아직 확인을 하지 않았을때
                    if (Regions[x + (y * RegionsSize.x)].Check[dir] == false)
                    //if (check[dir] == false)
                    {
                        //두개의 리전에 위치관계에 따라 영역을 지정해주고
                        switch(dir)
                        {
                            case (int)EnumTypes.Dir.UP:
                                topRight = (Vector2Int)Regions[destx + (desty * RegionsSize.x)].TopLeft;
                                bottomLeft = (Vector2Int)Regions[x + (y * RegionsSize.x)].BottomRight;
                                break;

                            case (int)EnumTypes.Dir.RIGHT:
                                topRight = (Vector2Int)Regions[destx + (desty * RegionsSize.x)].TopLeft;
                                bottomLeft = (Vector2Int)Regions[x + (y * RegionsSize.x)].BottomRight;
                                break;

                            case (int)EnumTypes.Dir.DOWN:
                                topRight = (Vector2Int)Regions[x + (y * RegionsSize.x)].TopLeft;
                                bottomLeft = (Vector2Int)Regions[destx + (desty * RegionsSize.x)].BottomRight;
                                break;

                            case (int)EnumTypes.Dir.LEFT:
                                topRight = (Vector2Int)Regions[x + (y * RegionsSize.x)].TopLeft;
                                bottomLeft = (Vector2Int)Regions[destx + (desty * RegionsSize.x)].BottomRight;
                                break;

                            case (int)EnumTypes.Dir.UPRIGHT:
                                topRight = (Vector2Int)Regions[destx + (desty * RegionsSize.x)].TopLeft;
                                bottomLeft = (Vector2Int)Regions[x + (y * RegionsSize.x)].BottomRight;
                                break;

                            case (int)EnumTypes.Dir.UPLEFT:
                                topRight = new Vector2Int(Regions[x + (y * RegionsSize.x)].TopLeft.x, Regions[destx + (desty * RegionsSize.x)].TopLeft.y);
                                bottomLeft = new Vector2Int(Regions[destx + (desty * RegionsSize.x)].BottomRight.x, Regions[x + (y * RegionsSize.x)].BottomRight.y);

                                //topRight = (Vector2Int)Regions[destx + (desty * RegionsSize.x)].TopRight;
                                //bottomLeft = (Vector2Int)Regions[x + (y * RegionsSize.x)].BottomLeft;
                                break;

                            case (int)EnumTypes.Dir.DOWNLEFT:
                                topRight = (Vector2Int)Regions[x + (y * RegionsSize.x)].TopLeft;
                                bottomLeft = (Vector2Int)Regions[destx + (desty * RegionsSize.x)].BottomRight;
                                break;

                            case (int)EnumTypes.Dir.DOWNRIGHT:

                                topRight = new Vector2Int(Regions[destx + (desty * RegionsSize.x)].TopLeft.x, Regions[x + (y * RegionsSize.x)].TopLeft.y);
                                bottomLeft = new Vector2Int(Regions[x + (y * RegionsSize.x)].BottomRight.x, Regions[destx + (desty * RegionsSize.x)].BottomRight.y);

                                //topRight = (Vector2Int)Regions[destx + (desty * RegionsSize.x)].TopRight;
                                //bottomLeft = (Vector2Int)Regions[x + (y * RegionsSize.x)].BottomLeft;
                                break;

                        }


                        //각 region의 시작지점 에서 시작지점으로 길이 존재하는지 확인한다.

                        Region startRegion = Regions[x + (y * RegionsSize.x)];
                        Region destRegion = Regions[destx + (desty * RegionsSize.x)];

                        testregion teststartRegion = testRegions[x + (y * RegionsSize.x)];
                        testregion deststartRegion = testRegions[destx + (desty * RegionsSize.x)];


                        int startCount = startRegion.LocalRegion.Count;
                        int destCount = destRegion.LocalRegion.Count;

                        LocalRegion startLocalRegion;
                        LocalRegion destLocalRegion;

                        testlocalregion teststartLocalRegion;
                        testlocalregion testdestLocalRegion;


                        Vector3 startPos;
                        Vector3 destPos;

                        List<Node> destList;

                        

                        for (int start = 0; start < startCount; start++)
                        {
                            for (int dest = 0; dest < destCount; dest++)
                            {
                                startLocalRegion = startRegion.LocalRegion[start];
                                destLocalRegion = destRegion.LocalRegion[dest];

                                teststartLocalRegion = teststartRegion.LocalRegion[start];
                                testdestLocalRegion = deststartRegion.LocalRegion[dest];


                                //Debug.Log($"{x + (y * RegionsSize.x)} 번째 리전 {start} 번째 로컬리전 [{startRegion.LocalRegion[start].NodeArray[0, 0].x},{startRegion.LocalRegion[start].NodeArray[0, 0].y} ]벽 검사 시작");

                                //시작지점이 벽인지 확인하고
                                if (!MapManager.Instance.IsWall(new Vector3Int(startRegion.LocalRegion[start].StartNode.x, startRegion.LocalRegion[start].StartNode.y, 0))&&
                                    !MapManager.Instance.IsWall(new Vector3Int(destRegion.LocalRegion[dest].StartNode.x, destRegion.LocalRegion[dest].StartNode.y, 0)))
                                {
                                    //시작지점과 도착지점의 월드 좌표를 받아온다.
                                    startPos = MapManager.Instance.MyGetCellCenterWorld(startRegion.LocalRegion[start].StartNode.x, startRegion.LocalRegion[start].StartNode.y);
                                    destPos = MapManager.Instance.MyGetCellCenterWorld(destRegion.LocalRegion[dest].StartNode.x, destRegion.LocalRegion[dest].StartNode.y);

                                    //경로 탐색
                                    destList = PathFinding(bottomLeft, topRight, startPos, destPos, false);

                                    //경로 존재
                                    if (destList != null)
                                    {
                                        if(destList.Count<16)
                                        {
                                            //startRegion.NeighborRegion[dir] = destRegion;
                                            //startRegion.NeighborDistance[dir] = destList.Count;
                                            //startRegion.IsMoveable[dir] = 1;

                                            //startLocalRegion.NeighborRegion[dir] = destLocalRegion;
                                            //startLocalRegion.NeighborDistance[dir] = destList.Count;
                                            //startLocalRegion.IsMoveable[dir] = 1;

                                            startLocalRegion.neighberRegions.Add(new KeyValuePair<LocalRegion, int>(destLocalRegion, destList.Count));
                                            teststartLocalRegion.neighborstartnode.Add(destLocalRegion.StartNode);
                                            teststartLocalRegion.neighbordis.Add(destList.Count);

                                            //destRegion.NeighborRegion[EnumTypes.R_Dir(dir)] = startRegion;
                                            //destRegion.NeighborDistance[EnumTypes.R_Dir(dir)] = destList.Count;
                                            //destRegion.IsMoveable[EnumTypes.R_Dir(dir)] = 1;

                                            //destLocalRegion.NeighborRegion[EnumTypes.R_Dir(dir)] = startLocalRegion;
                                            //destLocalRegion.NeighborDistance[EnumTypes.R_Dir(dir)] = destList.Count;
                                            //destLocalRegion.IsMoveable[EnumTypes.R_Dir(dir)] = 1;

                                            destLocalRegion.neighberRegions.Add(new KeyValuePair<LocalRegion, int>(startLocalRegion, destList.Count));
                                            testdestLocalRegion.neighborstartnode.Add(startLocalRegion.StartNode);
                                            testdestLocalRegion.neighbordis.Add(destList.Count);

                                            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                            ////중복된 노드인지도 확인을 할 필요가 있다.
                                            //GrapheNode startnode = new GrapheNode(startLocalRegion, destList.Count, NodeGraphe.Count);

                                            //startnode.x = startLocalRegion.StartNode.x;
                                            //startnode.y = startLocalRegion.StartNode.y;

                                            //if (NodeGraphe.Exists(a => a.IsEqual(startnode)))
                                            //{
                                            //    startnode = NodeGraphe.Find(a => a.IsEqual(startnode));
                                            //}
                                            //else
                                            //{
                                            //    NodeGraphe.Add(startnode);
                                            //    startLocalRegion.grapheNode = startnode;
                                            //}

                                            //GrapheNode destnode = new GrapheNode(destLocalRegion, destList.Count, NodeGraphe.Count);
                                            //destnode.x = destLocalRegion.StartNode.x;
                                            //destnode.y = destLocalRegion.StartNode.y;

                                            //if (NodeGraphe.Exists(a => a.IsEqual(destnode)))
                                            //{
                                            //    destnode = NodeGraphe.Find(a => a.IsEqual(destnode));
                                            //}
                                            //else
                                            //{
                                            //    NodeGraphe.Add(destnode);
                                            //    destLocalRegion.grapheNode = destnode;
                                            //}


                                            //GrapheWeight[startnode.Nodeindex, destnode.Nodeindex] = destList.Count;
                                            //GrapheWeight[destnode.Nodeindex, startnode.Nodeindex] = destList.Count;
                                            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                                            ///



                                            //Debug.Log($"{x + (y * RegionsSize.x)} 번째 리전의 {start} 번째 로컬리전과 {destx + (desty * RegionsSize.x)}번째 리전의 {dest} 번째 로컬리전 검사" +
                                            //   $"  [{startRegion.LocalRegion[start].StartNode.x},{startRegion.LocalRegion[start].StartNode.y} ] 경로 감지");


                                            //testlistdir.Add(true);
                                        }

                                    }
                                    //경로 존재 X
                                    else
                                    {
                                        //Debug.Log($"{x + (y * RegionsSize.x)} 번째 리전의 {start} 번째 로컬리전과 {destx + (desty * RegionsSize.x)}번째 리전의 {dest} 번째 로컬리전 검사" +
                                        //    $"  [{startRegion.LocalRegion[start].StartNode.x},{startRegion.LocalRegion[start].StartNode.y} ] 경로 감지 못함");
                                        //Debug.Log($"{x + (y * RegionsSize.x)} 번째 리전 {start} 번째 로컬리전 [{startRegion.LocalRegion[start].StartNode.x},{startRegion.LocalRegion[start].StartNode.y} ] 경로 감지 못함");

                                        //startLocalRegion.IsMoveable[dir] = 0;
                                        //destLocalRegion.IsMoveable[EnumTypes.R_Dir(dir)] = 0;

                                        //testlistdir.Add(false);
                                    }

                                }
                            }
                        }

                        startRegion.Check[dir] = true;
                        destRegion.Check[EnumTypes.R_Dir(dir)] = true;

                    }//if(Regions[x + (y * RegionsSize.x)].Check[dir] == false)


                }

                //testlist.Add(testlistdir);
            }
        }
    }



    //환인을 한 루트인지 아닌지 확인을 한 뒤에 들어온다.
    public bool CkechMoveable(Region region1, Region region2, int dir)
    {
        int dir_reverse = EnumTypes.Dir_Reverse[dir];

        //시작점과 끝점을 정한 다음에 

        //if(PathFinding)

        



        return false;
    }

    public Stack<int> stack = new Stack<int>();

    public bool[,] Ck = new bool[8, 8];

    //public List<Region> Regions = new List<Region>();
    public Region[] Regions;

    //큰 리전 개수
    public Vector2Int RegionsSize;

    


    public bool IsCheck(int x, int y, int padX, int padY)
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
            testregion testRegion = new testregion(bottomright, size);


            region.regionIndex = new Vector2Int(bottomright.x, bottomright.y);
            testRegion.regionIndex = new Vector2Int(bottomright.x, bottomright.y);


            //Regions.Add(region);
            Regions[(_bottomRight.x / RegionSize.x) + ((_bottomRight.y / RegionSize.y) * RegionsSize.x)] = region;
            testRegions[(_bottomRight.x / RegionSize.x) + ((_bottomRight.y / RegionSize.y) * RegionsSize.x)] = testRegion;


            for (int y = bottomright.y; y < maxsize.y; y++)
            {
                for (int x = bottomright.x; x < maxsize.x; x++)
                {
                    //방문한적이 없고
                    if (!IsCheck(x, y, bottomright.x, bottomright.y))
                    {

                        //해당 리전이 하위 리전이 된다.
                        LocalRegion region2 = new LocalRegion(region);
                        testlocalregion testregion2 = new testlocalregion();

                        //벽이 아니면
                        if (!MapManager.Instance.IsWall(new Vector3Int(x, y, 0)))
                        {
                            region2.StartNode = new Node(false, x, y);
                            testregion2.StartNode = new Node(false, x, y);

                            //이어져있는 길들을 탐색해서 리젼을 만들어 준다.
                            CheckRoad(region2, x, y, bottomright.x + size.x, bottomright.y + size.y);

                        }
                        else
                        {
                            region2.StartNode = new Node(true, x, y);
                            testregion2.StartNode = new Node(true, x, y);


                            region2.color = new Color(1.0f, 1.0f, 1.0f);
                            CheckWall(region2, x, y, bottomright.x + size.x, bottomright.y + size.y);

                        }

                        region.LocalRegion.Add(region2);
                        testRegion.LocalRegion.Add(testregion2);
                    }

                }
            }
        }
        else
        {
            return;
        }
    }

    //public void CheckWall(Region region, int x, int y, int maxX, int maxY)
    //{

    //}

    //public void CheckRoad(Region region, int x, int y, int maxX, int maxY)
    //{

    //}

    public void CheckWall(LocalRegion region, int x, int y, int maxX, int maxY)
    {
        int rocalIndextX = x - region.parentRegion.BottomRight.x;
        int rocalIndextY = y - region.parentRegion.BottomRight.y;

        SetCkech(x, y, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y, true);

        region.NodeArray[rocalIndextX, rocalIndextY] = new Node(true, x, y);
        region.parentRegion.NodeArray[rocalIndextX, rocalIndextY] = new Node(true, x, y);

        bool iswall = false;


        //상
        if (y + 1 < maxY && !IsCheck(x, y + 1, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y + 1, 0));
            if (iswall)
                CheckWall(region, x, y + 1, maxX, maxY);
        }
        //하
        if (rocalIndextY - 1 >= 0 && !IsCheck(x, y - 1, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y - 1, 0));
            if (iswall)
                CheckWall(region, x, y - 1, maxX, maxY);
        }
        //좌
        if (x + 1 < maxX && !IsCheck(x + 1, y, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x + 1, y, 0));
            if (iswall)
                CheckWall(region, x + 1, y, maxX, maxY);
        }
        //우
        if (rocalIndextX - 1 >= 0 && !IsCheck(x - 1, y, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x - 1, y, 0));
            if (iswall)
                CheckWall(region, x - 1, y, maxX, maxY);
        }

    }

    public void CheckRoad(LocalRegion region, int x, int y, int maxX, int maxY)
    {
        int rocalIndextX = x - region.parentRegion.BottomRight.x;
        int rocalIndextY = y - region.parentRegion.BottomRight.y;

        SetCkech(x, y, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y, true);

        region.NodeArray[rocalIndextX, rocalIndextY] = new Node(false, x, y);
        region.parentRegion.NodeArray[rocalIndextX, rocalIndextY] = new Node(true, x, y);

        bool iswall = false;

        //상
        if (y + 1 < maxY && !IsCheck(x, y + 1, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y + 1, 0));
            if (!iswall)
                CheckRoad(region, x, y + 1, maxX, maxY);
        }

        //하
        if (rocalIndextY - 1 >= 0 && !IsCheck(x, y - 1, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y - 1, 0));
            if (!iswall)
                CheckRoad(region, x, y - 1, maxX, maxY);
        }

        //좌
        if (x + 1 < maxX && !IsCheck(x + 1, y, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x + 1, y, 0));
            if (!iswall)
                CheckRoad(region, x + 1, y, maxX, maxY);
        }

        //우
        if (rocalIndextX - 1 >= 0 && !IsCheck(x - 1, y, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x - 1, y, 0));
            if (!iswall)
                CheckRoad(region, x - 1, y, maxX, maxY);
        }

    }

    #endregion

    #region Astar

    Vector2Int bottomLeft, topRight, startPos, targetPos;

    //Vector2Int bottomRight, topLeft;

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

    public List<List<Node>> TestFinalNodeListList = new List<List<Node>>();
    public int test;

    //public List<Node> test = new List<Node>();


    //시작 위치와 끝 위치를 받아와서 해당 크기만큼의 맵을 맵 매니저한테 받아온다.
    //그러곤 사용할 노드를 만들어 준다.
    //노드는 미리 만들어 두고 용량이 더 필요하면 새로 할당 받는다.
    //마우스가 클릭되면 현재 캐릭터의 월드위치가 시작점, 마우스의 클릭 월드위치가 목표지점으로 들어온다.
    //bottomLeft와 topRight 는 큰 리전을 기준으로 정한다.
    public void PathSetting(Vector3 start, Vector3 target)
    {
        startcell = MapManager.Instance.GetTileCellNum(new Vector2(start.x, start.y));//시작지점과 목표지점을 셀번호로 받아온다.
        targetcell = MapManager.Instance.GetTileCellNum(new Vector2(target.x, target.y));

        Vector2Int startRegionIndex = new Vector2Int(startcell.x / RegionSize.x, startcell.y / RegionSize.y);
        Vector2Int targetRegionIndex = new Vector2Int(targetcell.x / RegionSize.x, targetcell.y / RegionSize.y);

        Vector2Int bottomLeftRegionIndex = new Vector2Int(Mathf.Min(startRegionIndex.x, targetRegionIndex.x), Mathf.Min(startRegionIndex.y, targetRegionIndex.y));
        Vector2Int topRightRegionIndex = new Vector2Int(Mathf.Max(startRegionIndex.x, targetRegionIndex.x), Mathf.Max(startRegionIndex.y, targetRegionIndex.y));

        Vector2Int bottomRightRegionIndex = new Vector2Int(Mathf.Max(startRegionIndex.x, targetRegionIndex.x), Mathf.Min(startRegionIndex.y, targetRegionIndex.y));
        Vector2Int topLeftRegionIndex = new Vector2Int(Mathf.Max(startRegionIndex.x, targetRegionIndex.x), Mathf.Max(startRegionIndex.y, targetRegionIndex.y));


        Region startRegion = Regions[startRegionIndex.x + (startRegionIndex.y * RegionsSize.x)];
        Region targetRegion = Regions[targetRegionIndex.x + (targetRegionIndex.y * RegionsSize.x)];

        Region bottomLeftRegion = Regions[bottomLeftRegionIndex.x + (bottomLeftRegionIndex.y * RegionsSize.x)];
        Region topRightRegion = Regions[topRightRegionIndex.x + (topRightRegionIndex.y * RegionsSize.x)];


        //설정 필요(최대 연산 영역)
        bottomLeft = (Vector2Int)bottomLeftRegion.BottomRight;
        topRight = (Vector2Int)topRightRegion.TopLeft;

        //bottomRight = new Vector2Int(bottomLeftRegion.TopLeft.x, bottomLeftRegion.BottomRight.y);
        //topLeft = new Vector2Int(topRightRegion.BottomRight.x, topRightRegion.TopLeft.y);

        //bottomRight = 


        //설정 필요(시작과 도착지점)
        startPos = new Vector2Int(startcell.x, startcell.y);
        targetPos = new Vector2Int(targetcell.x, targetcell.y);





        //영역의 크기
        //int sizeX = targetcell.x - startcell.x;//시작지점과 목표지점을 이용해서 연산을 할 사이즈를 구한다.
        //int sizeY = targetcell.y - startcell.y;

        //if (sizeX < 0 || sizeY < 0)//둘중 하나라도 음수면 둘다 절댓값을 붙여준다.
        //{
        //    sizeX = Mathf.Abs(sizeX);
        //    sizeY = Mathf.Abs(sizeY);
        //}

        //sizeX += 1;
        //sizeY += 1;

        int sizeX = topRight.x - bottomLeft.x + 1;//시작지점과 목표지점을 이용해서 연산을 할 사이즈를 구한다.
        int sizeY = topRight.y - bottomLeft.y + 1;

        NodeArray = new Node[sizeX, sizeY];

        //항상 bottomleft와 topright 가 졍해져 있는 것이 아니기 때문에 출발지점과 도착지점의 위치에 따라 영역을 지정해주어야 한다.
        //if (targetcell.x < startcell.x && targetcell.y >= startcell.y)
        //{
        //    bottomLeft = new Vector2Int(targetcell.x, startcell.y);
        //    topRight = new Vector2Int(startcell.x, targetcell.y);
        //}
        //else if (targetcell.x >= startcell.x && targetcell.y < startcell.y)
        //{
        //    bottomLeft = new Vector2Int(startcell.x, targetcell.y);
        //    topRight = new Vector2Int(targetcell.x, startcell.y);
        //}
        //else if (targetcell.x < startcell.x && targetcell.y < startcell.y)
        //{
        //    bottomLeft = new Vector2Int(targetcell.x, targetcell.y);
        //    topRight = new Vector2Int(startcell.x, startcell.y);
        //}


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

                //test.Add(NodeArray[i, j]);
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

    public void PathSetting(Vector2Int bottomLeft, Vector2Int topRight, Vector3 start, Vector3 target)
    {
        startcell = MapManager.Instance.GetTileCellNum(new Vector2(start.x, start.y));//시작지점과 목표지점을 셀번호로 받아온다.
        targetcell = MapManager.Instance.GetTileCellNum(new Vector2(target.x, target.y));

        //Vector2Int startRegionIndex = new Vector2Int(startcell.x / RegionSize.x, startcell.y / RegionSize.y);
        //Vector2Int targetRegionIndex = new Vector2Int(targetcell.x / RegionSize.x, targetcell.y / RegionSize.y);

        //Vector2Int bottomLeftRegionIndex = new Vector2Int(Mathf.Min(startRegionIndex.x, targetRegionIndex.x), Mathf.Min(startRegionIndex.y, targetRegionIndex.y));
        //Vector2Int topRightRegionIndex = new Vector2Int(Mathf.Max(startRegionIndex.x, targetRegionIndex.x), Mathf.Max(startRegionIndex.y, targetRegionIndex.y));

        //Vector2Int bottomRightRegionIndex = new Vector2Int(Mathf.Max(startRegionIndex.x, targetRegionIndex.x), Mathf.Min(startRegionIndex.y, targetRegionIndex.y));
        //Vector2Int topLeftRegionIndex = new Vector2Int(Mathf.Max(startRegionIndex.x, targetRegionIndex.x), Mathf.Max(startRegionIndex.y, targetRegionIndex.y));


        //Region startRegion = Regions[startRegionIndex.x + (startRegionIndex.y * RegionsSize.x)];
        //Region targetRegion = Regions[targetRegionIndex.x + (targetRegionIndex.y * RegionsSize.x)];

        //Region bottomLeftRegion = Regions[bottomLeftRegionIndex.x + (bottomLeftRegionIndex.y * RegionsSize.x)];
        //Region topRightRegion = Regions[bottomLeftRegionIndex.x + (bottomLeftRegionIndex.y * RegionsSize.x)];


        //설정 필요(최대 연산 영역)
        this.bottomLeft = bottomLeft;
        this.topRight = topRight;

        //bottomRight = new Vector2Int(bottomLeftRegion.TopLeft.x, bottomLeftRegion.BottomRight.y);
        //topLeft = new Vector2Int(topRightRegion.BottomRight.x, topRightRegion.TopLeft.y);

        //bottomRight = 


        //설정 필요(시작과 도착지점)
        startPos = new Vector2Int(startcell.x, startcell.y);
        targetPos = new Vector2Int(targetcell.x, targetcell.y);





        //영역의 크기
        //int sizeX = targetcell.x - startcell.x;//시작지점과 목표지점을 이용해서 연산을 할 사이즈를 구한다.
        //int sizeY = targetcell.y - startcell.y;

        //if (sizeX < 0 || sizeY < 0)//둘중 하나라도 음수면 둘다 절댓값을 붙여준다.
        //{
        //    sizeX = Mathf.Abs(sizeX);
        //    sizeY = Mathf.Abs(sizeY);
        //}

        //sizeX += 1;
        //sizeY += 1;

        int sizeX = topRight.x - bottomLeft.x + 1;//시작지점과 목표지점을 이용해서 연산을 할 사이즈를 구한다.
        int sizeY = topRight.y - bottomLeft.y + 1;

        NodeArray = new Node[sizeX, sizeY];

        //항상 bottomleft와 topright 가 졍해져 있는 것이 아니기 때문에 출발지점과 도착지점의 위치에 따라 영역을 지정해주어야 한다.
        //if (targetcell.x < startcell.x && targetcell.y >= startcell.y)
        //{
        //    bottomLeft = new Vector2Int(targetcell.x, startcell.y);
        //    topRight = new Vector2Int(startcell.x, targetcell.y);
        //}
        //else if (targetcell.x >= startcell.x && targetcell.y < startcell.y)
        //{
        //    bottomLeft = new Vector2Int(startcell.x, targetcell.y);
        //    topRight = new Vector2Int(targetcell.x, startcell.y);
        //}
        //else if (targetcell.x < startcell.x && targetcell.y < startcell.y)
        //{
        //    bottomLeft = new Vector2Int(targetcell.x, targetcell.y);
        //    topRight = new Vector2Int(startcell.x, startcell.y);
        //}


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

                //test.Add(NodeArray[i, j]);
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



    //강제로 사용 영역을 제한해주는 길찾기
    //3차원 공간의 좌표들을 넣어준다.
    public List<Node> PathFinding(Vector2Int bottomLeft, Vector2Int topRight, Vector3 start, Vector3 target, bool allowDiagonal = true)
    {
        //초기 세팅 (시작점, 끝점, TopRight, BottomLeft 지정 리스트들 초기화 등등을 수행한다.)
        //PathSetting(start, target);

        PathSetting(bottomLeft, topRight, start, target);



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

                TestFinalNodeListList.Add(FinalNodeList);
                test++;

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


    //3차원 공간의 좌표들을 넣어준다.
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

                TestFinalNodeListList.Add(FinalNodeList);
                test++;

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
    #endregion

    #region GrapheAstar

    public int GrapheSize;
    //인접 매트리스 방식으로 구현
    public List<GrapheNode> NodeGraphe = new List<GrapheNode>();
    //각 간선의 가중치
    public int[,] GrapheWeight;



    ////시작노드, 목표 노드, 현재 탐색중인 노드
    //public LocalRegion StartRegion, TargetRegion, CurRegion;
    ////현재 노드와 인접해있고 벽이 아닌, 이동 가능한 노드들의 리스트
    //public List<LocalRegion> OpenRegionList;
    ////현재까지 지나온 노드들의 리스트
    //public List<LocalRegion> ClosedRegionList;
    ////최종 경로
    //public List<LocalRegion> FinalRegionList;


    //PriorityQueue<GrapheNode> pq = new PriorityQueue<GrapheNode>(PRIORITY_SORT_TYPE.ASCENDING);




    ////한 화면에 최대로 담기는 타일의 갯수가 가로,세로 10~11개 이기 때문에 이정도 크기로 고정하였다.
    //public Node[,] NodeArray = new Node[30, 30];
    //시작노드, 목표 노드, 현재 탐색중인 노드
    //public Region StartRegion, TargetRegion, CurRegion;
    ////현재 노드와 인접해있고 벽이 아닌, 이동 가능한 노드들의 리스트
    //public List<Node> OpenRegionList;
    ////현재까지 지나온 노드들의 리스트
    //public List<Node> ClosedRegionList;
    ////최종 경로
    //public List<Node> FinalRegionList;

    public void PathSettingCore(Vector3 start, Vector3 target)
    {

        //Vector2Int bottomLeftRegionIndex = new Vector2Int(Mathf.Min(startRegionIndex.x, targetRegionIndex.x), Mathf.Min(startRegionIndex.y, targetRegionIndex.y));
        //Vector2Int topRightRegionIndex = new Vector2Int(Mathf.Max(startRegionIndex.x, targetRegionIndex.x), Mathf.Max(startRegionIndex.y, targetRegionIndex.y));

        //Vector2Int bottomRightRegionIndex = new Vector2Int(Mathf.Max(startRegionIndex.x, targetRegionIndex.x), Mathf.Min(startRegionIndex.y, targetRegionIndex.y));
        //Vector2Int topLeftRegionIndex = new Vector2Int(Mathf.Max(startRegionIndex.x, targetRegionIndex.x), Mathf.Max(startRegionIndex.y, targetRegionIndex.y));


        

        //Region bottomLeftRegion = Regions[bottomLeftRegionIndex.x + (bottomLeftRegionIndex.y * RegionsSize.x)];
        //Region topRightRegion = Regions[topRightRegionIndex.x + (topRightRegionIndex.y * RegionsSize.x)];


        ////설정 필요(최대 연산 영역)
        //bottomLeft = (Vector2Int)bottomLeftRegion.BottomRight;
        //topRight = (Vector2Int)topRightRegion.TopLeft;


        //설정 필요(시작과 도착지점)
        //startPos = new Vector2Int(startcell.x, startcell.y);
        //targetPos = new Vector2Int(targetcell.x, targetcell.y);

        //StartRegion


        ////영역의 크기
        //int sizeX = topRight.x - bottomLeft.x + 1;//시작지점과 목표지점을 이용해서 연산을 할 사이즈를 구한다.
        //int sizeY = topRight.y - bottomLeft.y + 1;

        //NodeArray = new Node[sizeX, sizeY];


        ////벽정보를 받아와야 하기 때문에
        ////지작점부터 끝점까지 벽정보인지 아닌지 받아와서 노드를 만들어 준다.
        //for (int i = 0; i < sizeX; i++)
        //{
        //    for (int j = 0; j < sizeY; j++)
        //    {
        //        bool iswall = false;
        //        //시작점부터 끝점까지 타일맵의 셀들을 조사하면서 해당 셀이 벽인지 확인해서 노드에 넣어준다
        //        if (MapManager.Instance.IsWall(new Vector3Int(i + bottomLeft.x, j + bottomLeft.y, 0)))
        //        {
        //            iswall = true;
        //        }

        //        NodeArray[i, j] = new Node(iswall, i + bottomLeft.x, j + bottomLeft.y);

        //        //test.Add(NodeArray[i, j]);
        //    }
        //}



        //// 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
        //StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        //TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        startcell = MapManager.Instance.GetTileCellNum(new Vector2(start.x, start.y));//시작지점과 목표지점을 셀번호로 받아온다.
        targetcell = MapManager.Instance.GetTileCellNum(new Vector2(target.x, target.y));

        //시작 지점과 도착 지점의 리전의 인덱스를 받아온다.
        Vector2Int startRegionIndex = new Vector2Int(startcell.x / RegionSize.x, startcell.y / RegionSize.y);
        Vector2Int targetRegionIndex = new Vector2Int(targetcell.x / RegionSize.x, targetcell.y / RegionSize.y);

        //시작 시점과 도착 지점의 리전
        Region startRegion = Regions[startRegionIndex.x + (startRegionIndex.y * RegionsSize.x)];
        Region targetRegion = Regions[targetRegionIndex.x + (targetRegionIndex.y * RegionsSize.x)];

        //세부적인 로컬 리전을 받아온다.
        LocalRegion startLocalRegion = startRegion.FindLacalRegion(startcell.x, startcell.y);
        //int startLocalIndex = startLocalRegion.regionIndex;
        LocalRegion targetLocalRegion = targetRegion.FindLacalRegion(targetcell.x, targetcell.y);

        //StartRegion = startLocalRegion;
        StartNode = new Node(true, startLocalRegion.StartNode.x, startLocalRegion.StartNode.y, startLocalRegion);
        //TargetRegion = targetLocalRegion;
        TargetNode = new Node(true, targetLocalRegion.StartNode.x, targetLocalRegion.StartNode.y, targetLocalRegion);

        //OpenRegionList = new List<LocalRegion>() { StartRegion };
        //ClosedRegionList = new List<LocalRegion>();
        //FinalRegionList = new List<LocalRegion>();

        OpenList =  new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();

    }

    bool testStart = false;
    int limit = 0;
    //3차원 공간의 좌표들을 넣어준다.
    public List<Node> PathFindingCore(Vector3 start, Vector3 target)
    {
        //초기 세팅 (시작점, 끝점, TopRight, BottomLeft 지정 리스트들 초기화 등등을 수행한다.)
        PathSettingCore(start, target);


        limit = 0;
        while( OpenList.Count>0)
        {
            limit++;

            Debug.Log($"[길찾기] {limit} 번째 실행");

            if (limit > 100)
                break;

            CurNode = OpenList[0];

            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            for (int i=1;i< OpenList.Count;i++)
            {
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H)
                    CurNode = OpenList[i];
            }

            //Debug.Log($"[길찾기] 현재 그래프 노드 {CurNode.x} 번째 노드");

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);

            // 마지막
            if (CurNode.x == TargetNode.x && CurNode.y == TargetNode.y)
            {
                Node TargetCurNode = CurNode;

                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }

                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                return FinalNodeList;
            }

            int MoveCost = 0;

            List<KeyValuePair<LocalRegion, int>> neighborRegions = CurNode.localregion.neighberRegions;

            //모든 이웃 노드들에 대해 계산한다.
            for (int i=0;i< neighborRegions.Count;i++)
            {
                Node nextNode = new Node(true, neighborRegions[i].Key.StartNode.x, neighborRegions[i].Key.StartNode.y, neighborRegions[i].Key);
                MoveCost = neighborRegions[i].Value;

                //닫힌 리스트에 해당 노드가 없고
                if (!ClosedList.Exists(a=>a.x==nextNode.x && a.y == nextNode.y))
                {
                    // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
                    if (MoveCost < nextNode.G || !OpenList.Exists(a => a.x == nextNode.x && a.y == nextNode.y))
                    {
                        nextNode.G = MoveCost;
                        //NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetRegion.x) + Mathf.Abs(NeighborNode.y - TargetRegion.y)) * 10;
                        Vector3 nextPos = MapManager.Instance.MyGetCellCenterWorld(nextNode.x, nextNode.y);
                        Vector3 targetpos = MapManager.Instance.MyGetCellCenterWorld(TargetNode.x, TargetNode.y);

                        nextNode.H = Mathf.Abs(nextNode.x - TargetNode.x) + Mathf.Abs(nextNode.y - TargetNode.y);
                        nextNode.ParentNode = CurNode;

                        OpenList.Add(nextNode);
                        //Debug.Log($"[길찾기] 다음 오픈 리스트에 {NeighborNode.Nodeindex} 번째 노드 추가");
                    }
                }
                else
                {
                    int a = 0;
                }
            }


            //for(int i=0;i< GrapheMax; i++)
            //{
            //    //현재 노드와 이어져있는 모든 노드들에 대해 조사한다.
            //    if(GrapheWeight[CurRegion.Nodeindex,i] != 0)
            //    {
            //        GrapheNode NeighborNode = NodeGraphe[i];
            //        MoveCost = GrapheWeight[CurRegion.Nodeindex, i] + CurRegion.G;

            //        // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
            //        if (MoveCost < NeighborNode.G || !OpenRegionList.Contains(NeighborNode))
            //        {
            //            NeighborNode.G = MoveCost;
            //            //NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetRegion.x) + Mathf.Abs(NeighborNode.y - TargetRegion.y)) * 10;
            //            NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetRegion.x) + Mathf.Abs(NeighborNode.y - TargetRegion.y))/* * 10*/;
            //            NeighborNode.ParentNode = CurRegion;

            //            OpenRegionList.Add(NeighborNode);
            //            Debug.Log($"[길찾기] 다음 오픈 리스트에 {NeighborNode.Nodeindex} 번째 노드 추가");
            //        }
            //    }
            //}

           
        }


        //while (OpenList.Count > 0)
        //{
        //    // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
        //    CurNode = OpenList[0];

        //    for (int i = 1; i < OpenList.Count; i++)
        //        if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H)
        //            CurNode = OpenList[i];

        //    OpenList.Remove(CurNode);
        //    ClosedList.Add(CurNode);


        //    // 마지막
        //    if (CurNode == TargetNode)
        //    {
        //        Node TargetCurNode = TargetNode;
        //        while (TargetCurNode != StartNode)
        //        {
        //            FinalNodeList.Add(TargetCurNode);
        //            TargetCurNode = TargetCurNode.ParentNode;
        //        }
        //        FinalNodeList.Add(StartNode);
        //        FinalNodeList.Reverse();

        //        TestFinalNodeListList.Add(FinalNodeList);
        //        test++;

        //        return FinalNodeList;
        //    }


        //    // ↗↖↙↘
        //    if (allowDiagonal)
        //    {
        //        OpenListAdd(CurNode.x + 1, CurNode.y + 1);
        //        OpenListAdd(CurNode.x - 1, CurNode.y + 1);
        //        OpenListAdd(CurNode.x - 1, CurNode.y - 1);
        //        OpenListAdd(CurNode.x + 1, CurNode.y - 1);
        //    }

        //    // ↑ → ↓ ←
        //    OpenListAdd(CurNode.x, CurNode.y + 1);
        //    OpenListAdd(CurNode.x + 1, CurNode.y);
        //    OpenListAdd(CurNode.x, CurNode.y - 1);
        //    OpenListAdd(CurNode.x - 1, CurNode.y);
        //}
        return null;
    }

    void OpenListAddCore(int checkX, int checkY)
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
    #endregion

    #region Debug\Gizmos

    List<Color> testColorList = null;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if(testStart)
        {
            if (FinalNodeList != null)
            {
                if (FinalNodeList.Count > 0)
                {
                    for (int i = 0; i < FinalNodeList.Count - 1; i++)
                    {

                        //Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
                        Gizmos.DrawLine(MapManager.Instance.MyGetCellCenterWorld(FinalNodeList[i].localregion.StartNode.x, FinalNodeList[i].localregion.StartNode.y), MapManager.Instance.MyGetCellCenterWorld(FinalNodeList[i + 1].localregion.StartNode.x, FinalNodeList[i + 1].localregion.StartNode.y));
                    }

                }
            }
        }

        //if (FinalRegionList != null)
        //{
        //    if (FinalRegionList.Count != 0)
        //    {
        //        for (int i = 0; i < FinalRegionList.Count - 1; i++)
        //        {

        //            //Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
        //            Gizmos.DrawLine(MapManager.Instance.MyGetCellCenterWorld(FinalRegionList[i].region.StartNode.x, FinalRegionList[i].region.StartNode.y), MapManager.Instance.MyGetCellCenterWorld(FinalRegionList[i + 1].region.StartNode.x, FinalRegionList[i + 1].region.StartNode.y));
        //        }

        //    }
        //}


        //일반 길찾기 결과 보여줌
        //Gizmos.color = Color.white;


        //if (FinalNodeList.Count > 0)
        //{
        //    for (int i = 0; i < FinalNodeList.Count - 1; i++)
        //    {

        //        //Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
        //        Gizmos.DrawLine(MapManager.Instance.MyGetCellCenterWorld(FinalNodeList[i].x, FinalNodeList[i].y), MapManager.Instance.MyGetCellCenterWorld(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
        //    }

        //}

        //리전 초기화 보여줌
        //if (!testStart)
        //{
        //    if (TestFinalNodeListList != null)
        //    {
        //        if (testColorList == null && TestFinalNodeListList.Count > 0)
        //        {
        //            testColorList = new List<Color>();

        //            for (int i = 0; i < TestFinalNodeListList.Count; i++)
        //            {
        //                testColorList.Add(new Color(Random.Range(0, 1.0f), Random.Range(0, 1.0f), Random.Range(0, 1.0f)));
        //            }
        //        }

        //        for (int j = 0; j < TestFinalNodeListList.Count; j++)
        //        {
        //            if (TestFinalNodeListList[j] != null)
        //            {
        //                if (TestFinalNodeListList[j].Count != 0)
        //                {
        //                    Gizmos.color = testColorList[j];
        //                    for (int i = 0; i < TestFinalNodeListList[j].Count - 1; i++)
        //                    {
        //                        //Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
        //                        Gizmos.DrawLine(MapManager.Instance.MyGetCellCenterWorld(TestFinalNodeListList[j][i].x, TestFinalNodeListList[j][i].y), MapManager.Instance.MyGetCellCenterWorld(TestFinalNodeListList[j][i + 1].x, TestFinalNodeListList[j][i + 1].y));
        //                    }

        //                }
        //            }
        //        }
        //    }
        //}


        //foreach (List<Node> FinalNodeList in TestFinalNodeListList)
        //{


        //}


        //클릭된 리전 보여줌
        //Vector3 _topright = MapManager.Instance.MyGetCellCenterWorld(topRight.x, topRight.y);
        //Vector3 _bottomleft = MapManager.Instance.MyGetCellCenterWorld(bottomLeft.x, bottomLeft.y);

        //Gizmos.color = Color.cyan;
        //Gizmos.DrawCube(_topright, new Vector3(0.4f, 0.4f, 0.4f));
        //Gizmos.color = Color.red;
        //Gizmos.DrawCube(_bottomleft, new Vector3(0.4f, 0.4f, 0.4f));


        DrawRegion();
    }

    //Color[] color = { { 1.0f,1.0f,1.0f,1.0f}, }

    void DrawRegion()
    {

        if (Regions != null && Regions.Length > 0)
        {
            for (int i = 0; i < Regions.Length; i++)
            {

                //Vector3 _topright = MapManager.Instance.MyGetCellCenterWorld(Regions[i].TopLeft.x, Regions[i].TopLeft.y);
                //Vector3 _bottomleft = MapManager.Instance.MyGetCellCenterWorld(Regions[i].BottomRight.x, Regions[i].BottomRight.y);

                //Gizmos.color = Color.cyan;
                //Gizmos.DrawCube(_topright, new Vector3(0.4f, 0.4f, 0.4f));
                //Gizmos.color = Color.red;
                //Gizmos.DrawCube(_bottomleft, new Vector3(0.4f, 0.4f, 0.4f));

                //Gizmos.color = Regions[i].color;
                //foreach (Node a in Regions[i].NodeArray)
                //{
                //    //Gizmos.color = Regions[i].color;

                //    if (a != null)
                //    {
                //        //if(a.isWall)
                //        //{
                //        //    Gizmos.color = Color.white;
                //        //}

                //        Vector3 temp = MapManager.Instance.MyGetCellCenterWorld(a.x, a.y);

                //        Gizmos.DrawCube(temp, new Vector3(0.2f, 0.2f, 0.2f));

                //    }

                //}


                foreach (LocalRegion region in Regions[i].LocalRegion)
                {
                    //Gizmos.color = Color.blue;
                    //Vector3 temp2 = MapManager.Instance.MyGetCellCenterWorld(region.StartNode.x, region.StartNode.y);
                    //Gizmos.DrawCube(temp2, new Vector3(0.4f, 0.4f, 0.4f));

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



    }

    public GameObject teststartpos;
    public GameObject testtargetpos;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(!testStart)
                testStart = !testStart;

            Debug.Log("디버그 켜짐");

            PathFindingCore(teststartpos.transform.position, testtargetpos.transform.position);
        }
    }

    #endregion

    ////3차원 공간의 좌표들을 넣어준다.
    //public List<Node> PathFinding(Vector3 start, Vector3 target)
    //{
    //    //초기 세팅 (시작점, 끝점, TopRight, BottomLeft 지정 리스트들 초기화 등등을 수행한다.)
    //    PathSetting(start, target);

    //    while (OpenList.Count > 0)
    //    {
    //        // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
    //        CurNode = OpenList[0];

    //        for (int i = 1; i < OpenList.Count; i++)
    //            if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H)
    //                CurNode = OpenList[i];

    //        OpenList.Remove(CurNode);
    //        ClosedList.Add(CurNode);


    //        // 마지막
    //        if (CurNode == TargetNode)
    //        {
    //            Node TargetCurNode = TargetNode;
    //            while (TargetCurNode != StartNode)
    //            {
    //                FinalNodeList.Add(TargetCurNode);
    //                TargetCurNode = TargetCurNode.ParentNode;
    //            }
    //            FinalNodeList.Add(StartNode);
    //            FinalNodeList.Reverse();

    //            return FinalNodeList;
    //        }


    //        // ↗↖↙↘
    //        if (allowDiagonal)
    //        {
    //            OpenListAdd(CurNode.x + 1, CurNode.y + 1);
    //            OpenListAdd(CurNode.x - 1, CurNode.y + 1);
    //            OpenListAdd(CurNode.x - 1, CurNode.y - 1);
    //            OpenListAdd(CurNode.x + 1, CurNode.y - 1);
    //        }

    //        // ↑ → ↓ ←
    //        OpenListAdd(CurNode.x, CurNode.y + 1);
    //        OpenListAdd(CurNode.x + 1, CurNode.y);
    //        OpenListAdd(CurNode.x, CurNode.y - 1);
    //        OpenListAdd(CurNode.x - 1, CurNode.y);
    //    }
    //    return null;
    //}

    ////시작 위치와 끝 위치를 받아와서 해당 크기만큼의 맵을 맵 매니저한테 받아온다.
    ////그러곤 사용할 노드를 만들어 준다.
    ////노드는 미리 만들어 두고 용량이 더 필요하면 새로 할당 받는다.
    ////마우스가 클릭되면 현재 캐릭터의 월드위치가 시작점, 마우스의 클릭 월드위치가 목표지점으로 들어온다.
    ////bottomLeft와 topRight 는 큰 리전을 기준으로 정한다.

    //public void PathSetting(Vector3 start, Vector3 target)
    //{
    //    startcell = MapManager.Instance.GetTileCellNum(new Vector2(start.x, start.y));//시작지점과 목표지점을 셀번호로 받아온다.
    //    targetcell = MapManager.Instance.GetTileCellNum(new Vector2(target.x, target.y));

    //    bottomLeft = new Vector2Int(startcell.x, startcell.y);
    //    topRight = new Vector2Int(targetcell.x, targetcell.y);



    //    startPos = new Vector2Int(startcell.x, startcell.y);
    //    targetPos = new Vector2Int(targetcell.x, targetcell.y);


    //    int sizeX = targetcell.x - startcell.x;//시작지점과 목표지점을 이용해서 연산을 할 사이즈를 구한다.
    //    int sizeY = targetcell.y - startcell.y;


    //    if (sizeX < 0 || sizeY < 0)//둘중 하나라도 음수면 둘다 절댓값을 붙여준다.
    //    {
    //        sizeX = Mathf.Abs(sizeX);
    //        sizeY = Mathf.Abs(sizeY);
    //    }
    //    sizeX += 1;
    //    sizeY += 1;

    //    //항상 bottomleft와 topright 가 졍해져 있는 것이 아니기 때문에 출발지점과 도착지점의 위치에 따라 영역을 지정해주어야 한다.
    //    if (targetcell.x < startcell.x && targetcell.y >= startcell.y)
    //    {
    //        bottomLeft = new Vector2Int(targetcell.x, startcell.y);
    //        topRight = new Vector2Int(startcell.x, targetcell.y);
    //    }
    //    else if (targetcell.x >= startcell.x && targetcell.y < startcell.y)
    //    {
    //        bottomLeft = new Vector2Int(startcell.x, targetcell.y);
    //        topRight = new Vector2Int(targetcell.x, startcell.y);
    //    }
    //    else if (targetcell.x < startcell.x && targetcell.y < startcell.y)
    //    {
    //        bottomLeft = new Vector2Int(targetcell.x, targetcell.y);
    //        topRight = new Vector2Int(startcell.x, startcell.y);
    //    }



    //    //NodeArray = new Node[sizeX, sizeY];

    //    //벽정보를 받아와야 하기 때문에
    //    //지작점부터 끝점까지 벽정보인지 아닌지 받아와서 노드를 만들어 준다.
    //    for (int i = 0; i < sizeX; i++)
    //    {
    //        for (int j = 0; j < sizeY; j++)
    //        {
    //            bool iswall = false;
    //            //시작점부터 끝점까지 타일맵의 셀들을 조사하면서 해당 셀이 벽인지 확인해서 노드에 넣어준다
    //            if (MapManager.Instance.IsWall(new Vector3Int(i + bottomLeft.x, j + bottomLeft.y, 0)))
    //            {
    //                iswall = true;
    //            }

    //            NodeArray[i, j] = new Node(iswall, i + bottomLeft.x, j + bottomLeft.y);
    //            test.Add(NodeArray[i, j]);
    //        }
    //    }

    //    // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
    //    StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
    //    TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];


    //    OpenList = new List<Node>() { StartNode };
    //    ClosedList = new List<Node>();
    //    FinalNodeList = new List<Node>();

    //    //시작지점부터 목표지점까지의 공간과 벽정보들을 받아온다.
    //    //MapManager.GetI.ReadMapInfo(start, target);
    //}


}
