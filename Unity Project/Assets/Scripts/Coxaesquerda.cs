using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


using System.IO.Ports;

using System.IO;

public class Coxaesquerda : MonoBehaviour
{
    readonly SerialPort sensor = new SerialPort("COM10", 115200);
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
            string data = sensor.ReadLine();
        }
    }

    void Start()
    {
        sensor.Open();
        sensor.ReadTimeout = 100;
        Thread sampleThread = new Thread(new ThreadStart(sampleFunction));
        sampleThread.IsBackground = true;

        sampleThread.Start();

    }

    void Update()
    {
        if (sensor.IsOpen)
        {
            sensor.WriteLine("a");
            string value = sensor.ReadLine();
            string[] vec3 = value.Split(',');

            float xraw = ((float.Parse(vec3[0])) / 100);
            float yraw = ((float.Parse(vec3[1])) / 100);
            float zraw = ((float.Parse(vec3[2])) / 100);

            x = ((xraw) - xca)+rotação1[0];
            y = ((yraw) - yca)+rotação1[1];
            z = ((zraw) - zca)+rotação1[2];


            x = x + 90;
            y = y - 180;


            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(y, x, z), smooth);
            if (a == 1)
            {
                sensor.Close();
            }

        }

        if (sensor.IsOpen == false)
        {
            sensor.Open();
        }
    }


    public void Calibrar12()
    {
        sensor.WriteLine("a");
        string value = sensor.ReadLine();
        string[] vec3 = value.Split(',');

        float xraw = ((float.Parse(vec3[0])) / 100);
        float yraw = ((float.Parse(vec3[1])) / 100);
        float zraw = ((float.Parse(vec3[2])) / 100);

        xca = (xraw);
        yca = (yraw);
        zca = (zraw);

    }
}