using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private NetworkBootstrap net;
	private NetworkStatusUI status;
	private SceneFlowController sceneFlow;
	private GameModeState mode;

	private void Start()
	{
		net = FindFirstObjectByType<NetworkBootstrap>();
		status = GetComponent<NetworkStatusUI>();
		sceneFlow = FindFirstObjectByType<SceneFlowController>();
		mode = FindFirstObjectByType<GameModeState>();
		if (net == null) Debug.LogError("NetworkBootstrap not found. Did you start from Bootstrap Scene?");
	}

	public void Host() {

		if (mode.Mode == GameMode.LocalCoop)
		{
			sceneFlow.LoadGame();
			return;
		}

		status.Set("Starting Host...");
		net.StartHostAndLoadGame();
	}
	public void Join() {
		if (mode.Mode == GameMode.LocalCoop)
		{
			return;
		}
		status.Set("Connecting...");
		net.StartClient();
	}
	public void Quit() => Application.Quit();
}
