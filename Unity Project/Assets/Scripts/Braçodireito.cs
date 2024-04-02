using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.IO;
using System.IO.Ports;
public class Braçodireito : MonoBehaviour
{
    readonly SerialPort sensor1 = new SerialPort("COM13", 115200);
    public Vector3 rotação1;
    public float smooth = 0f;
    public int a = 0;
    public int b = 0;
    public GameObject retangulo;
    float x = 0;
    float y = 0;
    float z = 0;
    float xca = 0;
    float yca = 0;
    float zca = 0;

    public void sampleFunction()
    {
        while (true)
        {
            // execute operation
            // for example:
            string data = sensor1.ReadLine();
        }
    }
    void Start()
    {
        sensor1.Open();
        sensor1.ReadTimeout = 100;
        Thread sampleThread = new Thread(new ThreadStart(sampleFunction));
        sampleThread.IsBackground = true;

        sampleThread.Start();
    }    
    void Update()
    {
        if (sensor1.IsOpen)
        {

        sensor1.WriteLine("a");
        string value = sensor1.ReadLine();
        string[] vec3 = value.Split(',');

        float xraw = ((float.Parse(vec3[0])) / 100);
        float yraw = ((float.Parse(vec3[1])) / 100);
        float zraw = ((float.Parse(vec3[2])) / 100);

        x = (xraw) - xca;
        y = (yraw) - yca;
        z = (zraw) - zca;

            y = y +100;
            
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(y, z, x), smooth);
        if (a == 1)
            {
            sensor1.Close();
            }
        
        }

        if (sensor1.IsOpen == false)
        {
            sensor1.Open();
        }
    }
   
     
   

    public void Calibrar()
    {
        sensor1.WriteLine("a");
        string value = sensor1.ReadLine();
        string[] vec3 = value.Split(',');

        float xraw = ((float.Parse(vec3[0])) / 100);
        float yraw = ((float.Parse(vec3[1])) / 100);
        float zraw = ((float.Parse(vec3[2])) / 100);

         xca = (xraw);
         yca = (yraw);
         zca = (zraw);
       
    }

}
