using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgent2D : MonoBehaviour
{
    [Header("Steering")]
    public float speed = 1.0f;
    public float stoppingDistance = 0;

    [HideInInspector]//常にUnityエディタから非表示
    private Vector2 trace_area = Vector2.zero;
    public Vector2 destination
    {
        get { return trace_area; }
        set
        {
            trace_area = value;
            Trace(transform.position, value);
        }
    }
    public bool SetDestination(Vector2 target)
    {
        destination = target;
        return true;
    }


    // NavMesh に応じて経路を求める
    NavMeshPath path = null;
    int curIndex = 0;

    public bool Trace(Vector2 current, Vector2 target)
    {
        if (Vector2.Distance(current, target) <= stoppingDistance)
        {
            path = null;
            return true;
        }

        if(path == null)
        {
            path = new NavMeshPath();
            NavMesh.CalculatePath(current, target, NavMesh.AllAreas, path);
            curIndex = 0;
        }

        Vector2 corner = path.corners[curIndex];

        if (path.corners.Length >= 2)
        {
            if (Vector2.Distance(current, corner) <= stoppingDistance * 0.2)
            {
                curIndex++;
                corner = path.corners[curIndex];
            }
        }


        //else
        //{
        //    corner = target;
        //}


        transform.position = Vector2.MoveTowards(current, corner, speed * Time.deltaTime);

        return false;
    }


    //public bool Trace(Vector2 current, Vector2 target)
    //{
    //    if (Vector2.Distance(current, target) <= stoppingDistance)
    //    {
    //        return true;
    //    }

    //    // NavMesh に応じて経路を求める
    //    NavMeshPath path = new NavMeshPath();
    //    NavMesh.CalculatePath(current, target, NavMesh.AllAreas, path);

    //    Vector2 corner = path.corners[0];

    //    if (path.corners.Length >= 2)
    //    {
    //        if (Vector2.Distance(current, corner) <= 0.05f)
    //        {
    //            corner = path.corners[1];
    //        }
    //    }
    //    //else
    //    //{
    //    //    corner = target;
    //    //}


    //    transform.position = Vector2.MoveTowards(current, corner, speed * Time.deltaTime);

    //    return false;
    //}

}