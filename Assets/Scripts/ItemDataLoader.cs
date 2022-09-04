using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ItemDataLoader : Singleton<ItemDataLoader>
{
    public List<string> originalstr = null;
    public string FilePath;

    //public List<BaseNode> itemnodes = new List<BaseNode>();
    public Sprite[] sprites = null;

    public Dictionary<int, Dictionary<int, string>> iteminfos;

    //csv ���Ͽ��� �����͸� �о�� ��ųʸ� ���·� �����ϰ� �ִ´�.
    public Dictionary<int,Dictionary<int, string>> ItemFileOpen(string filepath)
    {
        originalstr.Clear();
        Dictionary<int, Dictionary<int, string>> data = new Dictionary<int, Dictionary<int, string>>();
        string FilePath = filepath;
        Debug.Log($"road {FilePath}");

        if (FilePath == null)
        {
            Debug.Log("���⼭ ����1");
            return null;
        }


        FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);//������ �б���� ����

        StreamReader sr = new StreamReader(fs);//��Ʈ�� ������ �̿��� �ؽ�Ʈ�� ���پ� �о���δ�.

        while (true)
        {
            string str = sr.ReadLine();
            originalstr.Add(str);

            if (str == null || str.Length == 0)
            {
                break;
            }
            char[] temp = new char[str.Length];
            bool flag = false;

            Dictionary<int, string> columsdic = new Dictionary<int, string>();
            int Dicindex = 0;
            int index = 0;
            //string temp;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '"'&&flag ==false)
                {
                    flag = true;//�����߰�ȣ�� ������ �ݴ��߰�ȣ�� ���ö����� ������ ������ , �� �׳� ����ִ´�.  
                    continue;
                }
                else if (str[i] == ',')//,�� ������ �ش� �ε������� , string���� ��ȯ�ؼ� columsdic �� �־��ش�.
                {
                    if (!flag)
                    {
                        temp[index] = '\0';
                        string tt = string.Join("", temp);//
                        columsdic.Add(Dicindex++, tt.Split('\0')[0]);//�׳� new string() �����ڸ� �̿��ϸ� char�迭�� ũ�⸸ŭ �ڿ� \0���� �ʱ�ȭ�� ���ڿ��� �������������.

                        
                        temp = new char[str.Length];
                        index = 0;
                        continue;
                    }
                }
                else if (i == str.Length - 1)//�޾ƿ� �� ���� ������ �����϶��� ��ǥ�� ���� ������ ���ش�.
                {
                    if (str[i] != '"')//���� �������� " �� ���������� "���ڴ� �־����� �ʴ´�.
                        temp[index++] = str[i];

                    temp[index] = '\0';
                    string tt = string.Join("", temp);
                    columsdic.Add(Dicindex++, tt.Split('\0')[0]);//�׳� new string() �����ڸ� �̿��ϸ� char�迭�� ũ�⸸ŭ �ڿ� \0���� �ʱ�ȭ�� ���ڿ��� �������������.

                    temp = new char[str.Length];
                    index = 0;
                    break;
                }
                else if (str[i] == '"'&&flag==true)
                {
                    flag = false;
                    continue;
                }


                temp[index++] = str[i];

            }


            int code = 0;

            if (int.TryParse(columsdic[0], out code))//ù��°�� ���ڰ� �ƴϸ� �ش� ���� ������ ���̴�. ����Ʈ�� ���� �ʴ´�.
            {
                data.Add(code, columsdic);
            }
            else
            {
                //classname = columsdic[0].Split('_')[0];
            }

        }
        int a = 10;
        //Debug.Log("���⼭ ����2");
        iteminfos = data;
        return data;
    }

    string temp;
    void Start()
    {
        string temppath = Application.streamingAssetsPath + FilePath;//��Ÿ���� �б⸸ ����
        ItemFileOpen(temppath);
        //Debug.Log(iteminfos[1001][(int)EnumTypes.ItemCollums.Damage]);
        //ItemInfo_Road(FilePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
