using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using System.IO; 


class people
{
    public string name;
    public int age;

    public people(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
}

public class SLManager : MonoBehaviour
{
    public Text tx;
    List<people> data = new List<people>();

    private void Start()
    {
        data.Add(new people("철수", 14));

    }

    public void _save()
    {
        string jdata = JsonConvert.SerializeObject(data);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);  // jdata를 이상한 숫자로 변형
        string format = System.Convert.ToBase64String(bytes);   // format에 스트링 형식의 이상한 내용으로 저장
        File.WriteAllText(Application.dataPath + "/TKstudio.json", format); 
    }

    public void _load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/TKstudio.json");
        byte[] bytes = System.Convert.FromBase64String(jdata); // 이상한 내용의 string 형식 jdata를 byte형식으로 변환
        string reformat = System.Text.Encoding.UTF8.GetString(bytes);  // byte 형식으로 변환한 이상한 내용을 정상적인 string 형식으로 변환

        tx.text = reformat;    
        data = JsonConvert.DeserializeObject<List<people>>(reformat);

        print(data[0].name); 
    }
}
