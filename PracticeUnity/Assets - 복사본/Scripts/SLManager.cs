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
        data.Add(new people("ö��", 14));

    }

    public void _save()
    {
        string jdata = JsonConvert.SerializeObject(data);
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);  // jdata�� �̻��� ���ڷ� ����
        string format = System.Convert.ToBase64String(bytes);   // format�� ��Ʈ�� ������ �̻��� �������� ����
        File.WriteAllText(Application.dataPath + "/TKstudio.json", format); 
    }

    public void _load()
    {
        string jdata = File.ReadAllText(Application.dataPath + "/TKstudio.json");
        byte[] bytes = System.Convert.FromBase64String(jdata); // �̻��� ������ string ���� jdata�� byte�������� ��ȯ
        string reformat = System.Text.Encoding.UTF8.GetString(bytes);  // byte �������� ��ȯ�� �̻��� ������ �������� string �������� ��ȯ

        tx.text = reformat;    
        data = JsonConvert.DeserializeObject<List<people>>(reformat);

        print(data[0].name); 
    }
}
