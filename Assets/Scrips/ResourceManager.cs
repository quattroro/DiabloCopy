using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class ResourceManager : Singleton<ResourceManager>
{
    MyObjectPool.ObjectPoolManager poolManager = new MyObjectPool.ObjectPoolManager();

    Dictionary<string, List<GameObject>> objlist;

    //��巹����� �ε� & ����

    //ȭ�鿡 3�� �̻� �����Ǵ� ������Ʈ�� ������Ʈ Ǯ���� ���

    //public T InstantiateObj<T>(string adressableName) where T : class
    //{
    //    //�ϴ� �ش�
    //    //Addressable ��� ����
    //    var temp = Addressables.LoadAssetAsync<GameObject>(adressableName);
    //    GameObject result = temp.WaitForCompletion();

    //    //Resources ��� ����
    //    var temp = Resources.Load<GameObject>(adressableName);
    //    GameObject result = temp;

    //    if (result == null)
    //    {
    //        Debug.LogError("��巹���� �ε� ����" + adressableName + "�������� ����");
    //        return default(T);
    //    }

    //    T resulttype = result.GetComponent<T>();
    //    Debug.Log("Ÿ��" + resulttype.GetType().ToString());

    //    if (poolManager.IsPooling(adressableName))//Ǯ���� �ϰ� �ִ� ��ü�� Ǯ������ ������ �ְ�
    //    {
    //        Debug.Log("Ǯ������");
    //        return poolManager.GetObject<T>(adressableName);
    //    }
    //    else//�ƴϸ� �׳� �������ش�.
    //    {
    //        Debug.Log("�׳� ����");

    //        //Addressable ��� ����
    //        temp = Addressables.InstantiateAsync(adressableName);
    //        result = temp.WaitForCompletion();

    //        ////Resources ��� ����
    //        ////result = GameObject.Instantiate<T>(result);


    //        if (typeof(T) == typeof(GameObject))
    //            return result as T;
    //        else
    //            return result.GetComponent<T>();

    //    }

    //}

    public T InstantiateObj<T>(string PrefabName) where T : class
    {
        //�ϴ� �ش�

        //Resources ��� ����
        var temp = Resources.Load<GameObject>(PrefabName);
        //GameObject result = temp;

        if (temp == null)
        {
            Debug.LogError("������ ����" + PrefabName + "�������� ����");
            return default(T);
        }

        //T resulttype = result.GetComponent<T>();
        //Debug.Log("Ÿ��" + resulttype.GetType().ToString());

        if (poolManager.IsPooling(PrefabName))//Ǯ���� �ϰ� �ִ� ��ü�� Ǯ������ ������ �ְ�
        {
            Debug.Log("Ǯ������");
            return poolManager.GetObject<T>(PrefabName);
        }
        else//�ƴϸ� �׳� �������ش�.
        {
            Debug.Log("�׳� ����");

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

        if (poolManager.IsPooling(PrefabName))//Ǯ���� �ϰ� �ִ� ��ü�� Ǯ������ ������ ����
        {
            Debug.Log("Ǯ�� ������");
            poolManager.ReturnObject(PrefabName, obj);
        }
        else
        {
            Debug.Log("�׳ɻ���");
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
    //�ش� Ÿ���� Ǯ���� ���� �����Ѵ�.
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

        ////Ÿ������ ����
        //public void CreatePool<T>(string adressableName,int poolsize=10) 
        //{
        //    //string typename = obj.GetType().Name;
        //    ObjectPool pool = null;

        //    //�̹� �ش� Ÿ���� Ǯ�� �ִ��� Ȯ���ϰ� ������ ����� �ش�.
        //    PoolDic.TryGetValue(typeof(T).Name, out pool);
        //    Debug.Log(typeof(T).Name +"Ǯ ���� ����");

        //    if(pool==null)
        //    {
        //        Debug.Log(typeof(T).Name + "Ǯ ���� �õ�");
        //        pool = new ObjectPool(adressableName, typeof(T), poolsize);
        //        PoolDic.Add(typeof(T).Name, pool);
        //    }

        //}

        //��巹���� �������� ����
        public void CreatePool<T>(string PrefabName, int poolsize = 10)
        {
            //string typename = obj.GetType().Name;
            ObjectPool pool = null;

            //�̹� �ش� Ÿ���� Ǯ�� �ִ��� Ȯ���ϰ� ������ ����� �ش�.
            PoolDic.TryGetValue(PrefabName, out pool);
            Debug.Log(PrefabName + "Ǯ ���� ����");

            if (pool == null)
            {
                Debug.Log(PrefabName + "Ǯ ���� �õ�");
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

        //    Debug.LogError("�������� �ʴ� Ÿ��");
        //    return default(T);
        //}

        //�������� �̸����� ���� �Ѵ�.
        //���� �ش� �������� Ǯ�� ��ϵǾ����� �ʴٸ� Ǯ�� ��� ���ش�.
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

            Debug.LogError("�������� �ʴ� Ÿ��");
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
        //�������� ����
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



    //Ǯ�� ���ӿ�����Ʈ�� ����
    //��ü�� �⺻������ 10�� ����
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

            Debug.Log(_type.Name + "Ǯ ���� �Ϸ�");



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