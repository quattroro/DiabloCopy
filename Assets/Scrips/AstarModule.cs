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

        //���� Ÿ�ϸʿ��� �ش� ������ ��ġ
        public Vector3Int BottomRight = new Vector3Int(-1, -1, -1);
        public Vector3Int TopLeft = new Vector3Int(-1, -1, -1);
        public Vector3Int TopRight = new Vector3Int(-1, -1, -1);
        public Vector3Int BottomLeft = new Vector3Int(-1, -1, -1);

        public Vector2Int regionIndex;
        //�ϳ��� ū ������ ũ��
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

    //���� �Ѿ�� �ʴ� �� ������ 8*8 ������ ������ ������ �ִ´�.
    //�� �߿����� ������ �� �ִ� ������ ���� �������� ������ �ִ´�.
    //[System.Serializable]
    public class Region
    {
        public int Num;
        public Vector2Int regionIndex;

        //�ϳ��� ���� �ȿ� �ִ� ������ ������ ���� ������ ������ ������ ���� �̾��� �־ ������ �� �ִ°� �̴�.
        //���� �ϳ��� ���� ������ ������ ��θ� �̷���� �ְų� ������ �����θ� �̷���� �ִ�.
        //public List<Region> LocalRegion;
        public List<LocalRegion> LocalRegion;



        //�ش� ���⿡ �ִ� �����̶� �̵��� �������� �Ǵ�
        public bool[] Check = new bool[(int)EnumTypes.Dir.DirMax];
        public Region[] NeighborRegion = new Region[(int)EnumTypes.Dir.DirMax];
        public int[] NeighborDistance = new int[(int)EnumTypes.Dir.DirMax];


        ////�ش� ������ üũ�� �Ǿ� �ִ���
        //public bool[] IsChecked = new bool[8];

        public Vector3Int Local_TopLeft = new Vector3Int(-1, -1, -1);
        public Vector3Int Local_BottomRight = new Vector3Int(-1, -1, -1);
        public Vector3Int Local_TopRight = new Vector3Int(-1, -1, -1);
        public Vector3Int Local_BottomLeft = new Vector3Int(-1, -1, -1);


        //���� Ÿ�ϸʿ��� �ش� ������ ��ġ
        public Vector3Int BottomRight = new Vector3Int(-1, -1, -1);
        public Vector3Int TopLeft = new Vector3Int(-1, -1, -1);
        public Vector3Int TopRight = new Vector3Int(-1, -1, -1);
        public Vector3Int BottomLeft = new Vector3Int(-1, -1, -1);

        //�ϳ��� ū ������ ũ��
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

        //�ش� ��ġ�� �ش� ������ ���� �ִ���
        public bool IsInside(Vector3 pos)
        {
            return false;
        }

        //�ش� ��ġ�� �ش� ������ ���� �ִ���
        public bool IsInside(Vector3Int pos)
        {
            return false;
        }

        //���忡���� �ε����� �־��ָ� �ش� ���� �ȿ� �����ϴ��� Ȯ�����ش�.
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

        



        //�ٸ� ������ ������ ��ģ��.
        public void MergeRegion(Region region)
        {

        }


        public void SetRegion()
        {

        }

    }

    //���ø����� �������� ���� �ٰ��� ����̱� ������ ������ vertex������ ������ �ִ´�.
    public class LocalRegion
    {
        public List<Vector2> vertex;
        public Vector2 center;


        //�̵� ������ ���� ���� ������ �Ÿ��� �־�����.
        public List<KeyValuePair<LocalRegion,int>> neighberRegions;
        public Region parentRegion;

        //// �θ�� �Ȱ��� ũ���� ��带 ������ ������ �ڽ��� ��尡 �ƴѰ��� 
        public Node[,] NodeArray;

        public Node StartNode;

        public Color color;


        //�ش� ���⿡ �ִ� �����̶� �̵��� �������� �Ǵ�
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
    //�����ε� ������ ������� �ʰ�
    //��θ� �̷���� �����鸸 ������ ���������� �̵��� �������� Ȯ���Ѵ�.
    //�� ������ �� �����̷κ��� �����¿� �밢�� ���⿡ �����ϴ� ������ ã�Ƴ��� ��ã�⸦ �����غ��� ������ �� �ִ��� �������� �̸� Ȯ���Ѵ�. 


    //������ �����ش�.
    public void InitSetting()
    {
        Vector3Int MapTopLeft = MapManager.Instance.TopLeftIndex;
        Vector3Int MapBottomRight = MapManager.Instance.BottomRightIndex;
        Vector2Int MapSize = MapManager.Instance.MapSize;

        RegionsSize = new Vector2Int(((MapSize.x / RegionSize.x) + 1),((MapSize.y / RegionSize.y) + 1));
        Regions = new Region[RegionsSize.x * RegionsSize.y];
        testRegions = new testregion[RegionsSize.x * RegionsSize.y];

        //�������� ����� �ְ� 
        for (int y = MapBottomRight.y; y < MapTopLeft.y; y = y + RegionSize.y)
        {
            for (int x = MapBottomRight.x; x < MapTopLeft.x; x = x + RegionSize.x)
            {
                CreateRegion(new Vector3Int(x, y, 0), new Vector2Int(8, 8));
            }
        }

        ////�������� ����� �ְ� 
        //for (int y = MapBottomRight.y; y < MapTopLeft.y; y = y + RegionSize.y)
        //{
        //    for (int x = MapBottomRight.x; x < MapTopLeft.x; x = x + RegionSize.x)
        //    {
        //        CreateRegion(new Vector3Int(x, y, 0), new Vector2Int(8, 8));
        //    }
        //}

        //�ش� �������� ���� �̵��� �� �ִ��� ������ Ȯ���Ѵ�.
        //�ش� ������ 

        RegionInitSetting();

    }



    //public List<List<bool>>[,] NodeArray;
    //public List<List<bool>> Resgiontestlist = new List<List<bool>>();
    //public List<bool> testlistdir;

    public int GrapheMax = 30;
    //�� ���� �����鳢�� �̵��� ��������, �̵��� �����ϴٸ� �Ÿ��� �������� Ȯ���Ѵ�.
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

        

        //�ϳ��� ������ 8���⿡ �ִ� �������� ���� ������ ���� ��� ������ �� �ִ��� Ȯ���Ѵ� 
        for (y = 0; y < RegionsSize.y; y++)
        {
            for(x = 0; x < RegionsSize.x; x++)
            {
                //testlistdir = new List<bool>();
                //bool[] check = { false, false, false, false, false, false, false, false };

                for (int dir = 0; dir < 8; dir++)
                {
                    //Debug.Log($"{x + (y * RegionsSize.x)} �� ���� {dir} ���� �˻�");

                    destx = x + EnumTypes.dir_all[dir].x;
                    desty = y + EnumTypes.dir_all[dir].y;

                    rdestx = x + EnumTypes.r_dir_all[dir].x;
                    rdesty = y + EnumTypes.r_dir_all[dir].y;


                    //�����迭 �����̰�
                    if (destx < 0 || destx >= RegionsSize.x || desty < 0 || desty >= RegionsSize.y)
                    {
                        //Debug.Log($"{x + (y * RegionsSize.x)} �� ���� {dir} ���� �˻� ����");
                        continue;
                    }



                    //�ش� ������ ���� Ȯ���� ���� �ʾ�����
                    if (Regions[x + (y * RegionsSize.x)].Check[dir] == false)
                    //if (check[dir] == false)
                    {
                        //�ΰ��� ������ ��ġ���迡 ���� ������ �������ְ�
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


                        //�� region�� �������� ���� ������������ ���� �����ϴ��� Ȯ���Ѵ�.

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


                                //Debug.Log($"{x + (y * RegionsSize.x)} ��° ���� {start} ��° ���ø��� [{startRegion.LocalRegion[start].NodeArray[0, 0].x},{startRegion.LocalRegion[start].NodeArray[0, 0].y} ]�� �˻� ����");

                                //���������� ������ Ȯ���ϰ�
                                if (!MapManager.Instance.IsWall(new Vector3Int(startRegion.LocalRegion[start].StartNode.x, startRegion.LocalRegion[start].StartNode.y, 0))&&
                                    !MapManager.Instance.IsWall(new Vector3Int(destRegion.LocalRegion[dest].StartNode.x, destRegion.LocalRegion[dest].StartNode.y, 0)))
                                {
                                    //���������� ���������� ���� ��ǥ�� �޾ƿ´�.
                                    startPos = MapManager.Instance.MyGetCellCenterWorld(startRegion.LocalRegion[start].StartNode.x, startRegion.LocalRegion[start].StartNode.y);
                                    destPos = MapManager.Instance.MyGetCellCenterWorld(destRegion.LocalRegion[dest].StartNode.x, destRegion.LocalRegion[dest].StartNode.y);

                                    //��� Ž��
                                    destList = PathFinding(bottomLeft, topRight, startPos, destPos, false);

                                    //��� ����
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
                                            ////�ߺ��� ��������� Ȯ���� �� �ʿ䰡 �ִ�.
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



                                            //Debug.Log($"{x + (y * RegionsSize.x)} ��° ������ {start} ��° ���ø����� {destx + (desty * RegionsSize.x)}��° ������ {dest} ��° ���ø��� �˻�" +
                                            //   $"  [{startRegion.LocalRegion[start].StartNode.x},{startRegion.LocalRegion[start].StartNode.y} ] ��� ����");


                                            //testlistdir.Add(true);
                                        }

                                    }
                                    //��� ���� X
                                    else
                                    {
                                        //Debug.Log($"{x + (y * RegionsSize.x)} ��° ������ {start} ��° ���ø����� {destx + (desty * RegionsSize.x)}��° ������ {dest} ��° ���ø��� �˻�" +
                                        //    $"  [{startRegion.LocalRegion[start].StartNode.x},{startRegion.LocalRegion[start].StartNode.y} ] ��� ���� ����");
                                        //Debug.Log($"{x + (y * RegionsSize.x)} ��° ���� {start} ��° ���ø��� [{startRegion.LocalRegion[start].StartNode.x},{startRegion.LocalRegion[start].StartNode.y} ] ��� ���� ����");

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



    //ȯ���� �� ��Ʈ���� �ƴ��� Ȯ���� �� �ڿ� ���´�.
    public bool CkechMoveable(Region region1, Region region2, int dir)
    {
        int dir_reverse = EnumTypes.Dir_Reverse[dir];

        //�������� ������ ���� ������ 

        //if(PathFinding)

        



        return false;
    }

    public Stack<int> stack = new Stack<int>();

    public bool[,] Ck = new bool[8, 8];

    //public List<Region> Regions = new List<Region>();
    public Region[] Regions;

    //ū ���� ����
    public Vector2Int RegionsSize;

    


    public bool IsCheck(int x, int y, int padX, int padY)
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
                    //�湮������ ����
                    if (!IsCheck(x, y, bottomright.x, bottomright.y))
                    {

                        //�ش� ������ ���� ������ �ȴ�.
                        LocalRegion region2 = new LocalRegion(region);
                        testlocalregion testregion2 = new testlocalregion();

                        //���� �ƴϸ�
                        if (!MapManager.Instance.IsWall(new Vector3Int(x, y, 0)))
                        {
                            region2.StartNode = new Node(false, x, y);
                            testregion2.StartNode = new Node(false, x, y);

                            //�̾����ִ� ����� Ž���ؼ� ������ ����� �ش�.
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


        //��
        if (y + 1 < maxY && !IsCheck(x, y + 1, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y + 1, 0));
            if (iswall)
                CheckWall(region, x, y + 1, maxX, maxY);
        }
        //��
        if (rocalIndextY - 1 >= 0 && !IsCheck(x, y - 1, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y - 1, 0));
            if (iswall)
                CheckWall(region, x, y - 1, maxX, maxY);
        }
        //��
        if (x + 1 < maxX && !IsCheck(x + 1, y, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x + 1, y, 0));
            if (iswall)
                CheckWall(region, x + 1, y, maxX, maxY);
        }
        //��
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

        //��
        if (y + 1 < maxY && !IsCheck(x, y + 1, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y + 1, 0));
            if (!iswall)
                CheckRoad(region, x, y + 1, maxX, maxY);
        }

        //��
        if (rocalIndextY - 1 >= 0 && !IsCheck(x, y - 1, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x, y - 1, 0));
            if (!iswall)
                CheckRoad(region, x, y - 1, maxX, maxY);
        }

        //��
        if (x + 1 < maxX && !IsCheck(x + 1, y, region.parentRegion.BottomRight.x, region.parentRegion.BottomRight.y))
        {
            iswall = MapManager.Instance.IsWall(new Vector3Int(x + 1, y, 0));
            if (!iswall)
                CheckRoad(region, x + 1, y, maxX, maxY);
        }

        //��
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

    public List<List<Node>> TestFinalNodeListList = new List<List<Node>>();
    public int test;

    //public List<Node> test = new List<Node>();


    //���� ��ġ�� �� ��ġ�� �޾ƿͼ� �ش� ũ�⸸ŭ�� ���� �� �Ŵ������� �޾ƿ´�.
    //�׷��� ����� ��带 ����� �ش�.
    //���� �̸� ����� �ΰ� �뷮�� �� �ʿ��ϸ� ���� �Ҵ� �޴´�.
    //���콺�� Ŭ���Ǹ� ���� ĳ������ ������ġ�� ������, ���콺�� Ŭ�� ������ġ�� ��ǥ�������� ���´�.
    //bottomLeft�� topRight �� ū ������ �������� ���Ѵ�.
    public void PathSetting(Vector3 start, Vector3 target)
    {
        startcell = MapManager.Instance.GetTileCellNum(new Vector2(start.x, start.y));//���������� ��ǥ������ ����ȣ�� �޾ƿ´�.
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


        //���� �ʿ�(�ִ� ���� ����)
        bottomLeft = (Vector2Int)bottomLeftRegion.BottomRight;
        topRight = (Vector2Int)topRightRegion.TopLeft;

        //bottomRight = new Vector2Int(bottomLeftRegion.TopLeft.x, bottomLeftRegion.BottomRight.y);
        //topLeft = new Vector2Int(topRightRegion.BottomRight.x, topRightRegion.TopLeft.y);

        //bottomRight = 


        //���� �ʿ�(���۰� ��������)
        startPos = new Vector2Int(startcell.x, startcell.y);
        targetPos = new Vector2Int(targetcell.x, targetcell.y);





        //������ ũ��
        //int sizeX = targetcell.x - startcell.x;//���������� ��ǥ������ �̿��ؼ� ������ �� ����� ���Ѵ�.
        //int sizeY = targetcell.y - startcell.y;

        //if (sizeX < 0 || sizeY < 0)//���� �ϳ��� ������ �Ѵ� ������ �ٿ��ش�.
        //{
        //    sizeX = Mathf.Abs(sizeX);
        //    sizeY = Mathf.Abs(sizeY);
        //}

        //sizeX += 1;
        //sizeY += 1;

        int sizeX = topRight.x - bottomLeft.x + 1;//���������� ��ǥ������ �̿��ؼ� ������ �� ����� ���Ѵ�.
        int sizeY = topRight.y - bottomLeft.y + 1;

        NodeArray = new Node[sizeX, sizeY];

        //�׻� bottomleft�� topright �� ������ �ִ� ���� �ƴϱ� ������ ��������� ���������� ��ġ�� ���� ������ �������־�� �Ѵ�.
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

                //test.Add(NodeArray[i, j]);
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

    public void PathSetting(Vector2Int bottomLeft, Vector2Int topRight, Vector3 start, Vector3 target)
    {
        startcell = MapManager.Instance.GetTileCellNum(new Vector2(start.x, start.y));//���������� ��ǥ������ ����ȣ�� �޾ƿ´�.
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


        //���� �ʿ�(�ִ� ���� ����)
        this.bottomLeft = bottomLeft;
        this.topRight = topRight;

        //bottomRight = new Vector2Int(bottomLeftRegion.TopLeft.x, bottomLeftRegion.BottomRight.y);
        //topLeft = new Vector2Int(topRightRegion.BottomRight.x, topRightRegion.TopLeft.y);

        //bottomRight = 


        //���� �ʿ�(���۰� ��������)
        startPos = new Vector2Int(startcell.x, startcell.y);
        targetPos = new Vector2Int(targetcell.x, targetcell.y);





        //������ ũ��
        //int sizeX = targetcell.x - startcell.x;//���������� ��ǥ������ �̿��ؼ� ������ �� ����� ���Ѵ�.
        //int sizeY = targetcell.y - startcell.y;

        //if (sizeX < 0 || sizeY < 0)//���� �ϳ��� ������ �Ѵ� ������ �ٿ��ش�.
        //{
        //    sizeX = Mathf.Abs(sizeX);
        //    sizeY = Mathf.Abs(sizeY);
        //}

        //sizeX += 1;
        //sizeY += 1;

        int sizeX = topRight.x - bottomLeft.x + 1;//���������� ��ǥ������ �̿��ؼ� ������ �� ����� ���Ѵ�.
        int sizeY = topRight.y - bottomLeft.y + 1;

        NodeArray = new Node[sizeX, sizeY];

        //�׻� bottomleft�� topright �� ������ �ִ� ���� �ƴϱ� ������ ��������� ���������� ��ġ�� ���� ������ �������־�� �Ѵ�.
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

                //test.Add(NodeArray[i, j]);
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



    //������ ��� ������ �������ִ� ��ã��
    //3���� ������ ��ǥ���� �־��ش�.
    public List<Node> PathFinding(Vector2Int bottomLeft, Vector2Int topRight, Vector3 start, Vector3 target, bool allowDiagonal = true)
    {
        //�ʱ� ���� (������, ����, TopRight, BottomLeft ���� ����Ʈ�� �ʱ�ȭ ����� �����Ѵ�.)
        //PathSetting(start, target);

        PathSetting(bottomLeft, topRight, start, target);



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

                TestFinalNodeListList.Add(FinalNodeList);
                test++;

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


    //3���� ������ ��ǥ���� �־��ش�.
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

                TestFinalNodeListList.Add(FinalNodeList);
                test++;

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
    #endregion

    #region GrapheAstar

    public int GrapheSize;
    //���� ��Ʈ���� ������� ����
    public List<GrapheNode> NodeGraphe = new List<GrapheNode>();
    //�� ������ ����ġ
    public int[,] GrapheWeight;



    ////���۳��, ��ǥ ���, ���� Ž������ ���
    //public LocalRegion StartRegion, TargetRegion, CurRegion;
    ////���� ���� �������ְ� ���� �ƴ�, �̵� ������ ������ ����Ʈ
    //public List<LocalRegion> OpenRegionList;
    ////������� ������ ������ ����Ʈ
    //public List<LocalRegion> ClosedRegionList;
    ////���� ���
    //public List<LocalRegion> FinalRegionList;


    //PriorityQueue<GrapheNode> pq = new PriorityQueue<GrapheNode>(PRIORITY_SORT_TYPE.ASCENDING);




    ////�� ȭ�鿡 �ִ�� ���� Ÿ���� ������ ����,���� 10~11�� �̱� ������ ������ ũ��� �����Ͽ���.
    //public Node[,] NodeArray = new Node[30, 30];
    //���۳��, ��ǥ ���, ���� Ž������ ���
    //public Region StartRegion, TargetRegion, CurRegion;
    ////���� ���� �������ְ� ���� �ƴ�, �̵� ������ ������ ����Ʈ
    //public List<Node> OpenRegionList;
    ////������� ������ ������ ����Ʈ
    //public List<Node> ClosedRegionList;
    ////���� ���
    //public List<Node> FinalRegionList;

    public void PathSettingCore(Vector3 start, Vector3 target)
    {

        //Vector2Int bottomLeftRegionIndex = new Vector2Int(Mathf.Min(startRegionIndex.x, targetRegionIndex.x), Mathf.Min(startRegionIndex.y, targetRegionIndex.y));
        //Vector2Int topRightRegionIndex = new Vector2Int(Mathf.Max(startRegionIndex.x, targetRegionIndex.x), Mathf.Max(startRegionIndex.y, targetRegionIndex.y));

        //Vector2Int bottomRightRegionIndex = new Vector2Int(Mathf.Max(startRegionIndex.x, targetRegionIndex.x), Mathf.Min(startRegionIndex.y, targetRegionIndex.y));
        //Vector2Int topLeftRegionIndex = new Vector2Int(Mathf.Max(startRegionIndex.x, targetRegionIndex.x), Mathf.Max(startRegionIndex.y, targetRegionIndex.y));


        

        //Region bottomLeftRegion = Regions[bottomLeftRegionIndex.x + (bottomLeftRegionIndex.y * RegionsSize.x)];
        //Region topRightRegion = Regions[topRightRegionIndex.x + (topRightRegionIndex.y * RegionsSize.x)];


        ////���� �ʿ�(�ִ� ���� ����)
        //bottomLeft = (Vector2Int)bottomLeftRegion.BottomRight;
        //topRight = (Vector2Int)topRightRegion.TopLeft;


        //���� �ʿ�(���۰� ��������)
        //startPos = new Vector2Int(startcell.x, startcell.y);
        //targetPos = new Vector2Int(targetcell.x, targetcell.y);

        //StartRegion


        ////������ ũ��
        //int sizeX = topRight.x - bottomLeft.x + 1;//���������� ��ǥ������ �̿��ؼ� ������ �� ����� ���Ѵ�.
        //int sizeY = topRight.y - bottomLeft.y + 1;

        //NodeArray = new Node[sizeX, sizeY];


        ////�������� �޾ƿ;� �ϱ� ������
        ////���������� �������� ���������� �ƴ��� �޾ƿͼ� ��带 ����� �ش�.
        //for (int i = 0; i < sizeX; i++)
        //{
        //    for (int j = 0; j < sizeY; j++)
        //    {
        //        bool iswall = false;
        //        //���������� �������� Ÿ�ϸ��� ������ �����ϸ鼭 �ش� ���� ������ Ȯ���ؼ� ��忡 �־��ش�
        //        if (MapManager.Instance.IsWall(new Vector3Int(i + bottomLeft.x, j + bottomLeft.y, 0)))
        //        {
        //            iswall = true;
        //        }

        //        NodeArray[i, j] = new Node(iswall, i + bottomLeft.x, j + bottomLeft.y);

        //        //test.Add(NodeArray[i, j]);
        //    }
        //}



        //// ���۰� �� ���, ��������Ʈ�� ��������Ʈ, ����������Ʈ �ʱ�ȭ
        //StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
        //TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];

        startcell = MapManager.Instance.GetTileCellNum(new Vector2(start.x, start.y));//���������� ��ǥ������ ����ȣ�� �޾ƿ´�.
        targetcell = MapManager.Instance.GetTileCellNum(new Vector2(target.x, target.y));

        //���� ������ ���� ������ ������ �ε����� �޾ƿ´�.
        Vector2Int startRegionIndex = new Vector2Int(startcell.x / RegionSize.x, startcell.y / RegionSize.y);
        Vector2Int targetRegionIndex = new Vector2Int(targetcell.x / RegionSize.x, targetcell.y / RegionSize.y);

        //���� ������ ���� ������ ����
        Region startRegion = Regions[startRegionIndex.x + (startRegionIndex.y * RegionsSize.x)];
        Region targetRegion = Regions[targetRegionIndex.x + (targetRegionIndex.y * RegionsSize.x)];

        //�������� ���� ������ �޾ƿ´�.
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
    //3���� ������ ��ǥ���� �־��ش�.
    public List<Node> PathFindingCore(Vector3 start, Vector3 target)
    {
        //�ʱ� ���� (������, ����, TopRight, BottomLeft ���� ����Ʈ�� �ʱ�ȭ ����� �����Ѵ�.)
        PathSettingCore(start, target);


        limit = 0;
        while( OpenList.Count>0)
        {
            limit++;

            Debug.Log($"[��ã��] {limit} ��° ����");

            if (limit > 100)
                break;

            CurNode = OpenList[0];

            // ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű��
            for (int i=1;i< OpenList.Count;i++)
            {
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H)
                    CurNode = OpenList[i];
            }

            //Debug.Log($"[��ã��] ���� �׷��� ��� {CurNode.x} ��° ���");

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);

            // ������
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

            //��� �̿� ���鿡 ���� ����Ѵ�.
            for (int i=0;i< neighborRegions.Count;i++)
            {
                Node nextNode = new Node(true, neighborRegions[i].Key.StartNode.x, neighborRegions[i].Key.StartNode.y, neighborRegions[i].Key);
                MoveCost = neighborRegions[i].Value;

                //���� ����Ʈ�� �ش� ��尡 ����
                if (!ClosedList.Exists(a=>a.x==nextNode.x && a.y == nextNode.y))
                {
                    // �̵������ �̿����G���� �۰ų� �Ǵ� ��������Ʈ�� �̿���尡 ���ٸ� G, H, ParentNode�� ���� �� ��������Ʈ�� �߰�
                    if (MoveCost < nextNode.G || !OpenList.Exists(a => a.x == nextNode.x && a.y == nextNode.y))
                    {
                        nextNode.G = MoveCost;
                        //NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetRegion.x) + Mathf.Abs(NeighborNode.y - TargetRegion.y)) * 10;
                        Vector3 nextPos = MapManager.Instance.MyGetCellCenterWorld(nextNode.x, nextNode.y);
                        Vector3 targetpos = MapManager.Instance.MyGetCellCenterWorld(TargetNode.x, TargetNode.y);

                        nextNode.H = Mathf.Abs(nextNode.x - TargetNode.x) + Mathf.Abs(nextNode.y - TargetNode.y);
                        nextNode.ParentNode = CurNode;

                        OpenList.Add(nextNode);
                        //Debug.Log($"[��ã��] ���� ���� ����Ʈ�� {NeighborNode.Nodeindex} ��° ��� �߰�");
                    }
                }
                else
                {
                    int a = 0;
                }
            }


            //for(int i=0;i< GrapheMax; i++)
            //{
            //    //���� ���� �̾����ִ� ��� ���鿡 ���� �����Ѵ�.
            //    if(GrapheWeight[CurRegion.Nodeindex,i] != 0)
            //    {
            //        GrapheNode NeighborNode = NodeGraphe[i];
            //        MoveCost = GrapheWeight[CurRegion.Nodeindex, i] + CurRegion.G;

            //        // �̵������ �̿����G���� �۰ų� �Ǵ� ��������Ʈ�� �̿���尡 ���ٸ� G, H, ParentNode�� ���� �� ��������Ʈ�� �߰�
            //        if (MoveCost < NeighborNode.G || !OpenRegionList.Contains(NeighborNode))
            //        {
            //            NeighborNode.G = MoveCost;
            //            //NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetRegion.x) + Mathf.Abs(NeighborNode.y - TargetRegion.y)) * 10;
            //            NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetRegion.x) + Mathf.Abs(NeighborNode.y - TargetRegion.y))/* * 10*/;
            //            NeighborNode.ParentNode = CurRegion;

            //            OpenRegionList.Add(NeighborNode);
            //            Debug.Log($"[��ã��] ���� ���� ����Ʈ�� {NeighborNode.Nodeindex} ��° ��� �߰�");
            //        }
            //    }
            //}

           
        }


        //while (OpenList.Count > 0)
        //{
        //    // ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű��
        //    CurNode = OpenList[0];

        //    for (int i = 1; i < OpenList.Count; i++)
        //        if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H)
        //            CurNode = OpenList[i];

        //    OpenList.Remove(CurNode);
        //    ClosedList.Add(CurNode);


        //    // ������
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


        //    // �֢آע�
        //    if (allowDiagonal)
        //    {
        //        OpenListAdd(CurNode.x + 1, CurNode.y + 1);
        //        OpenListAdd(CurNode.x - 1, CurNode.y + 1);
        //        OpenListAdd(CurNode.x - 1, CurNode.y - 1);
        //        OpenListAdd(CurNode.x + 1, CurNode.y - 1);
        //    }

        //    // �� �� �� ��
        //    OpenListAdd(CurNode.x, CurNode.y + 1);
        //    OpenListAdd(CurNode.x + 1, CurNode.y);
        //    OpenListAdd(CurNode.x, CurNode.y - 1);
        //    OpenListAdd(CurNode.x - 1, CurNode.y);
        //}
        return null;
    }

    void OpenListAddCore(int checkX, int checkY)
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


        //�Ϲ� ��ã�� ��� ������
        //Gizmos.color = Color.white;


        //if (FinalNodeList.Count > 0)
        //{
        //    for (int i = 0; i < FinalNodeList.Count - 1; i++)
        //    {

        //        //Gizmos.DrawLine(new Vector2(FinalNodeList[i].x, FinalNodeList[i].y), new Vector2(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
        //        Gizmos.DrawLine(MapManager.Instance.MyGetCellCenterWorld(FinalNodeList[i].x, FinalNodeList[i].y), MapManager.Instance.MyGetCellCenterWorld(FinalNodeList[i + 1].x, FinalNodeList[i + 1].y));
        //    }

        //}

        //���� �ʱ�ȭ ������
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


        //Ŭ���� ���� ������
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

            Debug.Log("����� ����");

            PathFindingCore(teststartpos.transform.position, testtargetpos.transform.position);
        }
    }

    #endregion

    ////3���� ������ ��ǥ���� �־��ش�.
    //public List<Node> PathFinding(Vector3 start, Vector3 target)
    //{
    //    //�ʱ� ���� (������, ����, TopRight, BottomLeft ���� ����Ʈ�� �ʱ�ȭ ����� �����Ѵ�.)
    //    PathSetting(start, target);

    //    while (OpenList.Count > 0)
    //    {
    //        // ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű��
    //        CurNode = OpenList[0];

    //        for (int i = 1; i < OpenList.Count; i++)
    //            if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H)
    //                CurNode = OpenList[i];

    //        OpenList.Remove(CurNode);
    //        ClosedList.Add(CurNode);


    //        // ������
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


    //        // �֢آע�
    //        if (allowDiagonal)
    //        {
    //            OpenListAdd(CurNode.x + 1, CurNode.y + 1);
    //            OpenListAdd(CurNode.x - 1, CurNode.y + 1);
    //            OpenListAdd(CurNode.x - 1, CurNode.y - 1);
    //            OpenListAdd(CurNode.x + 1, CurNode.y - 1);
    //        }

    //        // �� �� �� ��
    //        OpenListAdd(CurNode.x, CurNode.y + 1);
    //        OpenListAdd(CurNode.x + 1, CurNode.y);
    //        OpenListAdd(CurNode.x, CurNode.y - 1);
    //        OpenListAdd(CurNode.x - 1, CurNode.y);
    //    }
    //    return null;
    //}

    ////���� ��ġ�� �� ��ġ�� �޾ƿͼ� �ش� ũ�⸸ŭ�� ���� �� �Ŵ������� �޾ƿ´�.
    ////�׷��� ����� ��带 ����� �ش�.
    ////���� �̸� ����� �ΰ� �뷮�� �� �ʿ��ϸ� ���� �Ҵ� �޴´�.
    ////���콺�� Ŭ���Ǹ� ���� ĳ������ ������ġ�� ������, ���콺�� Ŭ�� ������ġ�� ��ǥ�������� ���´�.
    ////bottomLeft�� topRight �� ū ������ �������� ���Ѵ�.

    //public void PathSetting(Vector3 start, Vector3 target)
    //{
    //    startcell = MapManager.Instance.GetTileCellNum(new Vector2(start.x, start.y));//���������� ��ǥ������ ����ȣ�� �޾ƿ´�.
    //    targetcell = MapManager.Instance.GetTileCellNum(new Vector2(target.x, target.y));

    //    bottomLeft = new Vector2Int(startcell.x, startcell.y);
    //    topRight = new Vector2Int(targetcell.x, targetcell.y);



    //    startPos = new Vector2Int(startcell.x, startcell.y);
    //    targetPos = new Vector2Int(targetcell.x, targetcell.y);


    //    int sizeX = targetcell.x - startcell.x;//���������� ��ǥ������ �̿��ؼ� ������ �� ����� ���Ѵ�.
    //    int sizeY = targetcell.y - startcell.y;


    //    if (sizeX < 0 || sizeY < 0)//���� �ϳ��� ������ �Ѵ� ������ �ٿ��ش�.
    //    {
    //        sizeX = Mathf.Abs(sizeX);
    //        sizeY = Mathf.Abs(sizeY);
    //    }
    //    sizeX += 1;
    //    sizeY += 1;

    //    //�׻� bottomleft�� topright �� ������ �ִ� ���� �ƴϱ� ������ ��������� ���������� ��ġ�� ���� ������ �������־�� �Ѵ�.
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

    //    //�������� �޾ƿ;� �ϱ� ������
    //    //���������� �������� ���������� �ƴ��� �޾ƿͼ� ��带 ����� �ش�.
    //    for (int i = 0; i < sizeX; i++)
    //    {
    //        for (int j = 0; j < sizeY; j++)
    //        {
    //            bool iswall = false;
    //            //���������� �������� Ÿ�ϸ��� ������ �����ϸ鼭 �ش� ���� ������ Ȯ���ؼ� ��忡 �־��ش�
    //            if (MapManager.Instance.IsWall(new Vector3Int(i + bottomLeft.x, j + bottomLeft.y, 0)))
    //            {
    //                iswall = true;
    //            }

    //            NodeArray[i, j] = new Node(iswall, i + bottomLeft.x, j + bottomLeft.y);
    //            test.Add(NodeArray[i, j]);
    //        }
    //    }

    //    // ���۰� �� ���, ��������Ʈ�� ��������Ʈ, ����������Ʈ �ʱ�ȭ
    //    StartNode = NodeArray[startPos.x - bottomLeft.x, startPos.y - bottomLeft.y];
    //    TargetNode = NodeArray[targetPos.x - bottomLeft.x, targetPos.y - bottomLeft.y];


    //    OpenList = new List<Node>() { StartNode };
    //    ClosedList = new List<Node>();
    //    FinalNodeList = new List<Node>();

    //    //������������ ��ǥ���������� ������ ���������� �޾ƿ´�.
    //    //MapManager.GetI.ReadMapInfo(start, target);
    //}


}
