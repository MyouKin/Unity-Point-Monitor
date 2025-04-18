using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;

public class Pointbehave : MonoBehaviour
{
    public int pointnumber=0;
    // Start is called before the first frame update
    void Start()
    {

    }
    float xx,yy,zz;
    Color color_i;
    public void updatepos(float x,float y,float z,Color color)
    {
        xx=x;
        yy=y;
        zz=z; 
        color_i = color;
    }
    // Update is called once per frame
    void Update()
    {
        //更新位置
        this.transform.position = new Vector3(xx, yy, zz);
        //更新颜色
        this.GetComponent<Renderer>().material.color =color_i;
    }

}