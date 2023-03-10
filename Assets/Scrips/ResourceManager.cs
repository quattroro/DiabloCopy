using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class ResourceManager : Singleton<ResourceManager>
{
    MyObjectPool.ObjectPoolManager poolManager = new MyObjectPool.ObjectPoolManager();

    Dictionary<string, List<GameObject>> objlist;

    //어드레서블로 로드 & 생성

    //화면에 3개 이상 생성되는 오브젝트는 오브젝트 풀링을 사용

    //public T InstantiateObj<T>(string adressableName) where T : class
    //{
    //    //일단 해당
    //    //Addressable 사용 버전
    //    var temp = Addressables.LoadAssetAsync<GameObject>(adressableName);
    //    GameObject result = temp.WaitForCompletion();

    //    //Resources 사용 버전
    //    var temp = Resources.Load<GameObject>(adressableName);
    //    GameObject result = temp;

    //    if (result == null)
    //    {
    //        Debug.LogError("어드레서블 로드 오류" + adressableName + "존재하지 않음");
    //        return default(T);
    //    }

    //    T resulttype = result.GetComponent<T>();
    //    Debug.Log("타입" + resulttype.GetType().ToString());

    //    if (poolManager.IsPooling(adressableName))//풀링을 하고 있는 객체면 풀링에서 꺼내서 주고
    //    {
    //        Debug.Log("풀링생성");
    //        return poolManager.GetObject<T>(adressableName);
    //    }
    //    else//아니면 그냥 생성해준다.
    //    {
    //        Debug.Log("그냥 생성");

    //        //Addressable 사용 버전
    //        temp = Addressables.InstantiateAsync(adressableName);
    //        result = temp.WaitForCompletion();

    //        ////Resources 사용 버전
    //        ////result = GameObject.Instantiate<T>(result);


    //        if (typeof(T) == typeof(GameObject))
    //            return result as T;
    //        else
    //            return result.GetComponent<T>();

    //    }

    //}

    public T InstantiateObj<T>(string PrefabName) where T : class
    {
        //일단 해당

        //Resources 사용 버전
        var temp = Resources.Load<GameObject>(PrefabName);
        //GameObject result = temp;

        if (temp == null)
        {
            Debug.LogError("프리팹 오류" + PrefabName + "존재하지 않음");
            return default(T);
        }

        //T resulttype = result.GetComponent<T>();
        //Debug.Log("타입" + resulttype.GetType().ToString());

        if (poolManager.IsPooling(PrefabName))//풀링을 하고 있는 객체면 풀링에서 꺼내서 주고
        {
            Debug.Log("풀링생성");
            return poolManager.GetObject<T>(PrefabName);
        }
        else//아니면 그냥 생성해준다.
        {
            Debug.Log("그냥 생성");

            GameObject result = GameObject.Instantiate<GameObject>(temp);

            if (typeof(T) == typeof(GameObject))
                return result as T;
            else
                return result.GetComponent<T>();

        }

    }


    public void DestroyObj<T>(string PrefabName, GameObject obj)
    {
        if (obj == null)
            return;

        if (poolManager.IsPooling(PrefabName))//풀링을 하고 있는 객체면 풀링에서 꺼내서 삭제
        {
            Debug.Log("풀링 돌려줌");
            poolManager.ReturnObject(PrefabName, obj);
        }
        else
        {
            Debug.Log("그냥삭제");
            //Addressables.ReleaseInstance(obj);
            GameObject.Destroy(obj);
        }
    }

    public void RegistPoolManager<T>(string PrefabName)
    {
        poolManager.CreatePool<T>(PrefabName);
    }
}


namespace MyObjectPool
{
    //해당 타입의 풀들을 만들어서 관리한다.
    public class ObjectPoolManager
    {
        public Dictionary<string, ObjectPool> PoolDic = new Dictionary<string, ObjectPool>();

        //public bool IsPooling(string typestring)
        //{
        //    return PoolDic.ContainsKey(typestring);
        //}

        public bool IsPooling(string PrefabName)
        {
            return PoolDic.ContainsKey(PrefabName);
        }

        ////타입으로 관리
        //public void CreatePool<T>(string adressableName,int poolsize=10) 
        //{
        //    //string typename = obj.GetType().Name;
        //    ObjectPool pool = null;

        //    //이미 해당 타입의 풀이 있는지 확인하고 없으면 만들어 준다.
        //    PoolDic.TryGetValue(typeof(T).Name, out pool);
        //    Debug.Log(typeof(T).Name +"풀 생성 들어옴");

        //    if(pool==null)
        //    {
        //        Debug.Log(typeof(T).Name + "풀 생성 시도");
        //        pool = new ObjectPool(adressableName, typeof(T), poolsize);
        //        PoolDic.Add(typeof(T).Name, pool);
        //    }

        //}

        //어드레서블 네임으로 관리
        public void CreatePool<T>(string PrefabName, int poolsize = 10)
        {
            //string typename = obj.GetType().Name;
            ObjectPool pool = null;

            //이미 해당 타입의 풀이 있는지 확인하고 없으면 만들어 준다.
            PoolDic.TryGetValue(PrefabName, out pool);
            Debug.Log(PrefabName + "풀 생성 들어옴");

            if (pool == null)
            {
                Debug.Log(PrefabName + "풀 생성 시도");
                pool = new ObjectPool(PrefabName, typeof(T), poolsize);
                PoolDic.Add(PrefabName, pool);
            }

        }

        //public T GetObject<T>()
        //{
        //    ObjectPool pool = null;
        //    PoolDic.TryGetValue(typeof(T).Name, out pool);

        //    if(pool!=null)
        //    {
        //        return pool.GetObj().GetComponent<T>();
        //    }

        //    Debug.LogError("존재하지 않는 타입");
        //    return default(T);
        //}

        //프리팹의 이름으로 관리 한다.
        //만약 해당 프리팹이 풀에 등록되어있지 않다면 풀에 등록 해준다.
        public T GetObject<T>(string PrefabName) where T : class
        {
            ObjectPool pool = null;
            PoolDic.TryGetValue(PrefabName, out pool);

            //
            if (pool != null)
            {
                if (typeof(T) == typeof(GameObject))
                    return pool.GetObj() as T;
                else
                    return pool.GetObj().GetComponent<T>();
            }
            else
            {
                CreatePool<T>(PrefabName);
            }

            Debug.LogError("존재하지 않는 타입");
            return default(T);
        }

        //public void ReturnObject(System.Type _type, GameObject obj)
        //{
        //    ObjectPool pool = null;

        //    bool flag = PoolDic.ContainsKey(_type.Name);
        //    //

        //    if (flag)
        //    {
        //        PoolDic.TryGetValue(_type.Name, out pool);
        //        pool.ReturnObj(obj);
        //    }
        //}
        //네임으로 관리
        public void ReturnObject(string PrefabName, GameObject obj)
        {
            if (obj == null)
                return;

            ObjectPool pool = null;
            bool flag = PoolDic.ContainsKey(PrefabName);
            if (flag)
            {
                PoolDic.TryGetValue(PrefabName, out pool);
                pool.ReturnObj(obj);
            }
        }

    }


    public class ObjectPoolBase
    {



    }



    //풀은 게임오브젝트로 관리
    //객체는 기본적으로 10개 생성
    public class ObjectPool/*<T>:ObjectPoolBase*/
    {
        //T _poolObj;
        string _prefabName;
        System.Type _type;
        public Stack<GameObject> _stack;
        int _poolSize = 0;
        Transform _parent;
        GameObject prefabOBJ;

        public ObjectPool(string PrefabName, System.Type type, int poolsize)
        {
            _stack = new Stack<GameObject>();
            //System.Type _type = System.Type.GetType(_Name);
            _type = type;
            _prefabName = PrefabName;
            _poolSize = poolsize;

            prefabOBJ = Resources.Load<GameObject>(PrefabName);

            CreateObj(_poolSize);

            Debug.Log(_type.Name + "풀 생성 완료");



        }


        public void CreateObj(int count)
        {
            for (int i = 0; i < count; i++)
            {
                //var temp = Addressables.InstantiateAsync(_adressableName);
                //var result = temp.WaitForCompletion();

                var temp = GameObject.Instantiate<GameObject>(prefabOBJ);
                temp.SetActive(false);
                _stack.Push(temp);
            }
        }


        public GameObject GetObj()
        {
            GameObject temp = null;

            if (_stack.Count > 0)
                temp = _stack.Pop();

            if (temp == null)
            {
                CreateObj(1);
                temp = _stack.Pop();
            }
            temp.SetActive(true);
            temp.transform.SetParent(null);

            return temp;
        }

        public void ReturnObj(GameObject obj)
        {
            if (obj == null)
                return;

            if (_stack.Contains(obj))
                return;

            obj.SetActive(false);
            _stack.Push(obj);
        }


    }
}