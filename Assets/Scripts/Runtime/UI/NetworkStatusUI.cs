using UnityEngine;
using Unity.Netcode;
using TMPro;

public class NetworkStatusUI : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;

	private void Awake()
	{
		if (statusText == null) Debug.LogError("Assign statustext in Inspector");
	}

	private void OnEnable()
	{
		if (NetworkManager.Singleton == null) return;

		NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
		NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
		Set("Idle");
	}

	private void OnDisable()
	{
		if (NetworkManager.Singleton == null) return;

		NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
		NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
	}

	public void Set(string msg)
	{
		if (statusText != null) statusText.text = msg;
	}

	private void OnClientConnected(ulong clientId)
	{
		if (NetworkManager.Singleton.IsHost) Set("Hosting (Client Connected)");
		else Set("Connected");
	}

	private void OnClientDisconnected(ulong clientId)
	{
		if (NetworkManager.Singleton.LocalClientId == clientId)
		{
			Set("Disconnected / Failed");
		}
		else
		{
			Set("A Client Disconnected");
		}
	}
}
