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

    //csv 파일에서 데이터를 읽어와 딕셔너리 형태로 보관하고 있는다.
    public Dictionary<int,Dictionary<int, string>> ItemFileOpen(string filepath)
    {
        originalstr.Clear();
        Dictionary<int, Dictionary<int, string>> data = new Dictionary<int, Dictionary<int, string>>();
        string FilePath = filepath;
        Debug.Log($"road {FilePath}");

        if (FilePath == null)
        {
            Debug.Log("여기서 나감1");
            return null;
        }


        FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);//파일을 읽기모드로 열고

        StreamReader sr = new StreamReader(fs);//스트림 리더를 이용해 텍스트로 한줄씩 읽어들인다.

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
                    flag = true;//여는중괄호가 나오면 닫는중괄호가 나올때까지 앞으로 나오는 , 는 그냥 집어넣는다.  
                    continue;
                }
                else if (str[i] == ',')//,가 나오면 해당 인덱스에는 , string으로 변환해서 columsdic 에 넣어준다.
                {
                    if (!flag)
                    {
                        temp[index] = '\0';
                        string tt = string.Join("", temp);//
                        columsdic.Add(Dicindex++, tt.Split('\0')[0]);//그냥 new string() 생성자를 이용하면 char배열의 크기만큼 뒤에 \0으로 초기화된 문자열이 만들어져버린다.

                        
                        temp = new char[str.Length];
                        index = 0;
                        continue;
                    }
                }
                else if (i == str.Length - 1)//받아온 한 줄의 마지막 문자일때도 쉼표과 같은 동작을 해준다.
                {
                    if (str[i] != '"')//만약 마지막이 " 로 끝났을때는 "문자는 넣어주지 않는다.
                        temp[index++] = str[i];

                    temp[index] = '\0';
                    string tt = string.Join("", temp);
                    columsdic.Add(Dicindex++, tt.Split('\0')[0]);//그냥 new string() 생성자를 이용하면 char배열의 크기만큼 뒤에 \0으로 초기화된 문자열이 만들어져버린다.

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

            if (int.TryParse(columsdic[0], out code))//첫번째가 숫자가 아니면 해당 행은 열제목 행이다. 리스트에 넣지 않는다.
            {
                data.Add(code, columsdic);
            }
            else
            {
                //classname = columsdic[0].Split('_')[0];
            }

        }
        int a = 10;
        //Debug.Log("여기서 나감2");
        iteminfos = data;
        return data;
    }

    string temp;
    void Start()
    {
        string temppath = Application.streamingAssetsPath + FilePath;//런타임중 읽기만 가능
        ItemFileOpen(temppath);
        //Debug.Log(iteminfos[1001][(int)EnumTypes.ItemCollums.Damage]);
        //ItemInfo_Road(FilePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
