using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private NetworkBootstrap net;
	private SceneFlowController sceneFlow;

	private void Start()
	{
		net = FindFirstObjectByType<NetworkBootstrap>();
		if (net == null) Debug.LogError("NetworkBootstrap not found. Did you start from Bootstrap Scene?");

		sceneFlow = FindFirstObjectByType<SceneFlowController>();
		if (sceneFlow == null) Debug.LogError("SceneFlowController not found. Did you start from Bootstrap Scene?");
	}

	public void Host() => net.StartHostAndLoadGame();
	public void Join() => net.StartClient();
	public void Quit() => sceneFlow.Quit();
}
