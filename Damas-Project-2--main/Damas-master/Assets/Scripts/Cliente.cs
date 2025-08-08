using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TcpClientUnity : MonoBehaviour
{
    public InputField input;
    public Button sendButton;

    private TcpClient client;
    private NetworkStream stream;

    private string serverIP = "10.56.2.200"; // IP do servidor (PC destino)
    private int port = 8080;

    void Start()
    {
        sendButton.onClick.AddListener(SendMessageToServer);
        ConnectToServer();
    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverIP, port);
            stream = client.GetStream();
            Debug.Log("Conectado ao servidor.");
        }
        catch (SocketException ex)
        {
            Debug.LogError("Falha ao conectar: " + ex.Message);
        }
    }

    void SendMessageToServer()
    {
        if (client == null || !client.Connected)
        {
            Debug.LogWarning("Não conectado ao servidor.");
            return;
        }

        string mensagem = input.text;
        if (string.IsNullOrWhiteSpace(mensagem)) return;

        byte[] data = Encoding.UTF8.GetBytes(mensagem);

        try
        {
            stream.Write(data, 0, data.Length);
            Debug.Log("Mensagem enviada: " + mensagem);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Erro ao enviar: " + ex.Message);
        }
    }

    private void OnApplicationQuit()
    {
        stream?.Close();
        client?.Close();
    }
}