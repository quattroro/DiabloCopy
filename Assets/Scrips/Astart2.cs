//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Astart2 : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    private void AStar()
//    {
//        int[] dirY = new int[] { -1, 0, 1, 0, -1, 1, 1, -1 };
//        int[] dirX = new int[] { 0, -1, 0, 1, -1, -1, 1, 1 };
//        int[] cost = new int[] { 10, 10, 10, 10, 14, 14, 14, 14 };

//        bool[,] closed = new bool[_board.Size, _board.Size];
//        int[,] open = new int[_board.Size, _board.Size];
//        PriorityQueue<PQNode> pq = new PriorityQueue<PQNode>();
//        Pos[,] parent = new Pos[_board.Size, _board.Size];

//        for (int y = 0; y < _board.Size; y++)
//            for (int x = 0; x < _board.Size; x++)
//                open[y, x] = Int32.MaxValue;

//        //시작점 발견
//        open[PosY, PosX] = 10 * (Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX));
//        pq.Push(new PQNode() { F = 10 * (Math.Abs(_board.DestY - PosY) + Math.Abs(_board.DestX - PosX)), G = 0, Y = PosY, X = PosX });
//        parent[PosY, PosX] = new Pos(PosY, PosX);

//        //제일 좋은 후보 찾기
//        while (pq.Count() > 0)
//        {
//            PQNode node = pq.Pop();

//            //이미 방문했으면 스킵
//            if (closed[node.Y, node.X])
//                continue;

//            //f(n)이 가장 작은 노드 방문 완료
//            closed[node.Y, node.X] = true;

//            if (node.Y == _board.DestY && node.X == _board.DestX)
//                break;

//            for (int i = 0; i < dirY.Length; i++)
//            {
//                int nextY = node.Y + dirY[i];
//                int nextX = node.X + dirX[i];

//                //유효범위 체크
//                if (nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size)
//                    continue;
//                if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
//                    continue;
//                if (closed[nextY, nextX])
//                    continue;

//                int g = node.G + cost[i];
//                int h = 10 * (Math.Abs(_board.DestY - nextY) + Math.Abs(_board.DestX - nextX));

//                if (open[nextY, nextX] < g + h)
//                    continue;

//                open[nextY, nextX] = g + h;
//                pq.Push(new PQNode() { F = g + h, G = g, Y = nextY, X = nextX });
//                parent[nextY, nextX] = new Pos(node.Y, node.X);
//            }
//        }

//        CalcPathFromParent(parent);
//    }
//}
