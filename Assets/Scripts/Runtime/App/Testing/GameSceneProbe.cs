using UnityEngine;
using Unity.Netcode;

public class GameSceneProbe : MonoBehaviour
{
    private void Start()
	{
		var nm = NetworkManager.Singleton;
		if (nm == null)
		{
			Debug.LogError("No NetworkManager.Singleton in game scene.");
			return;
		}

		Debug.Log($"Game loaded. IsHost={nm.IsHost}, IsClient={nm.IsClient}, IsServer={nm.IsServer}");
	}
}
