using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
public class NetworkBootstrap : MonoBehaviour
{
	[SerializeField] private NetworkManager networkManager;
	[SerializeField] private SceneFlowController sceneFlow;
	[SerializeField] private string gameSceneName = "Game";

	private UnityTransport Transport => networkManager.GetComponent<UnityTransport>();

	private void Awake()
	{
		if (networkManager == null) networkManager = NetworkManager.Singleton;
		if (sceneFlow == null) sceneFlow = GetComponent<SceneFlowController>();

		networkManager.OnClientConnectedCallback += OnClientConnected;
		networkManager.OnClientDisconnectCallback += OnClientDisconnected;
	}

	private void OnDestroy()
	{
		if (networkManager == null) return;
		networkManager.OnClientConnectedCallback -= OnClientConnected;
		networkManager.OnClientDisconnectCallback -= OnClientDisconnected;
	}

	public void StartHostAndLoadGame()
	{
		if (!networkManager.StartHost())
		{
			Debug.LogError("Failed to start Host.");
			return;
		}

		networkManager.SceneManager.LoadScene(gameSceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
	}

	public void StartClient(string address = "127.0.0.1", ushort port = 7777)
	{
		if (Transport != null)
		{
			Transport.ConnectionData.Address = address;
			Transport.ConnectionData.Port = port;
		}

		if (!networkManager.StartClient())
		{
			Debug.LogError("Failed to start Client.");
			return;
		}
	}

	public void ShutdownToMenu()
	{
		if (networkManager.IsListening)
		{
			networkManager.Shutdown();
		}

		sceneFlow.LoadMainMenu();
	}

	private void OnClientConnected(ulong clientId)
	{
		Debug.Log($"Client connected: {clientId} (local={networkManager.LocalClientId})");
	}

	private void OnClientDisconnected(ulong clientId)
	{
		Debug.Log($"Client disconnected: {clientId}");
	}
}
