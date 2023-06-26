using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class comm : MonoBehaviour
{
    public byte torizq = 0x00;
    public byte tordere = 0x00;
    public byte pechoizq = 0x00;
    public byte pechodere = 0x00;
    public byte hombroizq = 0x00;
    public byte hombrodere = 0x00;

    public byte torizqB = 0x00;
    public byte tordereB = 0x00;
    public byte pechoizqB = 0x00;
    public byte pechodereB = 0x00;
    public byte hombroizqB = 0x00;
    public byte hombrodereB = 0x00;

    private static comm instance;
    private Thread sendThread;
    private UdpClient Client;
    private IPEndPoint sendEndPoint;
    string ip = "192.168.1.17";
    int sendPort = 50005; // = 50001;local port
    private bool isInitialized;

    private Queue sendQueue;
    private void Awake()
    {
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        instance = this;
        sendEndPoint = new IPEndPoint(IPAddress.Parse(ip), sendPort);
        Client = new UdpClient(sendPort);

        sendQueue = Queue.Synchronized(new Queue());

        sendThread = new Thread(new ThreadStart(SendData));
        sendThread.IsBackground = true;
        sendThread.Start();
        isInitialized = true;
    }

    private void SendData()
    {
        while (true)
        {
            try
            {
                byte[] message = { pechodere, pechodereB, pechoizq, pechoizqB, tordere, tordereB, torizq, torizqB, hombrodere, hombrodereB, hombroizq, hombroizqB };
                Client.Send(message, message.Length, sendEndPoint);
                Debug.Log("enviado");
                Debug.Log(message[0].ToString() + message[1].ToString() + message[2].ToString() + message[3].ToString() + 
                    message[4].ToString() + message[5].ToString() + message[6].ToString() + message[7].ToString() + message[8].ToString() + 
                    message[9].ToString() + message[10].ToString() + message[11].ToString());
                //sendQueue.Enqueue(temperature);
                //SerializeMessage(text);
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.ToString());
            }
        }
    }

    private void SerializeMessage(string message)
    {
        try
        {
            string[] chain = message.Split(' ');
            string key = chain[0];
            float value = 0;
            if (float.TryParse(chain[1], out value))
            {
                sendQueue.Enqueue(value);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void OnDestroy()
    {
        TryKillThread();
    }

    private void OnApplicationQuit()
    {
        TryKillThread();
    }

    private void TryKillThread()
    {
        if (isInitialized)
        {
            sendThread.Abort();
            sendThread = null;
            Client.Close();
            Client = null;
            Debug.Log("Thread killed");
            isInitialized = false;
        }
    }

    void Update()
    {
        if (isInitialized)
        {
            
              
        }

    }

    
}
