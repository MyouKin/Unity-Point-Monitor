using System;  
using System.Collections;  
using System.Net;  
using System.Net.Sockets;  
using System.Text;  
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;  

public class SocketManager : MonoBehaviour  
{  
    public const int numofpoints=17;
    public float[,] pointsdata;
    public string ipAddress = "127.0.0.1";  
    public int port = 7788;  
    private Socket clientSocket;  
    byte[] _data = new byte[1024];  
    private Thread _thread;  
    private string _message;  
    // private Color[] colors = { Color.red, Color.green, Color.green, Color.green, Color.yellow };
    private Color colors = Color.red;

    public GameObject[] objs=new GameObject[17];
    public Pointbehave[] scripts=new Pointbehave[17];
    // public Dynatext txtsc;
    private void Start()  
    {  
        objs[0] = GameObject.Find("Point1");
        objs[1] = GameObject.Find("Point2");
        objs[2] = GameObject.Find("Point3");
        objs[3] = GameObject.Find("Point4");
        objs[4] = GameObject.Find("Point5");
        objs[5] = GameObject.Find("Point6");
        objs[6] = GameObject.Find("Point7");
        objs[7] = GameObject.Find("Point8");
        objs[8] = GameObject.Find("Point9");
        objs[9] = GameObject.Find("Point10");
        objs[10] = GameObject.Find("Point11");
        objs[11] = GameObject.Find("Point12");
        objs[12] = GameObject.Find("Point13");
        objs[13] = GameObject.Find("Point14");
        objs[14] = GameObject.Find("Point15");
        objs[15] = GameObject.Find("Point16");
        objs[16] = GameObject.Find("Point17");
        // txtobj = GameObject.Find("Dynatext");

        scripts[0]=objs[0].GetComponent<Pointbehave>();
        scripts[1]=objs[1].GetComponent<Pointbehave>();
        scripts[2]=objs[2].GetComponent<Pointbehave>();
        scripts[3]=objs[3].GetComponent<Pointbehave>();
        scripts[4]=objs[4].GetComponent<Pointbehave>();
        scripts[5]=objs[5].GetComponent<Pointbehave>();
        scripts[6]=objs[6].GetComponent<Pointbehave>();
        scripts[7]=objs[7].GetComponent<Pointbehave>();
        scripts[8]=objs[8].GetComponent<Pointbehave>();
        scripts[9]=objs[9].GetComponent<Pointbehave>();
        scripts[10]=objs[10].GetComponent<Pointbehave>();
        scripts[11]=objs[11].GetComponent<Pointbehave>();
        scripts[12]=objs[12].GetComponent<Pointbehave>();
        scripts[13]=objs[13].GetComponent<Pointbehave>();
        scripts[14]=objs[14].GetComponent<Pointbehave>();
        scripts[15]=objs[15].GetComponent<Pointbehave>();
        scripts[16]=objs[16].GetComponent<Pointbehave>();
        // txtsc=txtobj.GetComponent<Dynatext>();
        ConnectToServer();
    }  

    private float[,] Converting(string raw)
    {
        string[] splited=raw.Split('N');
        float[,] nums = new float[numofpoints,3];
        for(int i =0;i<numofpoints;i++)
        {
            nums[i,0] = float.Parse(splited[3*i]);
            nums[i,1] = float.Parse(splited[3*i+1]);
            nums[i,2] = float.Parse(splited[3*i+2]);
        }
        return nums;
    }
    
    void ConnectToServer()  
    {  
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);  
        clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ipAddress), port));  
        _thread = new Thread(ReceiveMessage);  
        _thread.Start();
    }  

    // 启动线程, 持续接收消息  
    void ReceiveMessage()  
    {  
        while (true)  
        {  
            if (clientSocket.Connected == false)  
                break;  
            int length = clientSocket.Receive(_data);  
  
            _message = Encoding.UTF8.GetString(_data, 0, length);  
            Debug.Log(_message);
            pointsdata = Converting(_message);
            for(int i =0;i<numofpoints;i++)
            {
                scripts[i].updatepos(pointsdata[i,0],pointsdata[i,1],pointsdata[i,2],colors);
            }
        }  
    }  

    void OnDestroy()  
    {  
        _thread.Abort();  
        clientSocket.Shutdown(SocketShutdown.Both);  //既不接受也不发送  
        clientSocket.Close();  //关闭连接  
    }
} 