using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
	[Header("Refs")]
    private NetworkBootstrap net;
	private NetworkStatusUI status;
	private SceneFlowController sceneFlow;
	private GameModeState mode;

	[Header("Panels")]
	[SerializeField] private GameObject panelModeSelect;
	[SerializeField] private GameObject panelOnline;

	private void Awake()
	{
		net = FindFirstObjectByType<NetworkBootstrap>();
		status = GetComponent<NetworkStatusUI>();
		sceneFlow = FindFirstObjectByType<SceneFlowController>();
		mode = FindFirstObjectByType<GameModeState>();

		ShowModeSelect();
	}

	public void ChooseLocalCoop()
	{
		mode.SetLocalCoop(true);
		sceneFlow.LoadGame();
	}

	public void ChooseOnline()
	{
		mode.SetLocalCoop(false);
		ShowOnline();
	}

	public void HostOnline()
	{
		mode.SetLocalCoop(false);
		net.StartHostAndLoadGame();
	}

	public void JoinOnline()
	{
		mode.SetLocalCoop(false);
		net.StartClient();
	}

	public void Back()
	{
		ShowModeSelect();
	}

	public void Quit()
	{
		Application.Quit();
	}

	private void ShowModeSelect()
	{
		if (panelModeSelect) panelModeSelect.SetActive(true);
		if (panelOnline) panelOnline.SetActive(false);
	}

	private void ShowOnline()
	{
		if (panelModeSelect) panelModeSelect.SetActive(false);
		if (panelOnline) panelOnline.SetActive(true);
	}

	// private void Start()
	// {
	// 	net = FindFirstObjectByType<NetworkBootstrap>();
	// 	status = GetComponent<NetworkStatusUI>();
	// 	sceneFlow = FindFirstObjectByType<SceneFlowController>();
	// 	mode = FindFirstObjectByType<GameModeState>();
	// 	if (net == null) Debug.LogError("NetworkBootstrap not found. Did you start from Bootstrap Scene?");
	// }

	// public void Host() {

	// 	if (mode.Mode == GameMode.LocalCoop)
	// 	{
	// 		sceneFlow.LoadGame();
	// 		return;
	// 	}

	// 	status.Set("Starting Host...");
	// 	net.StartHostAndLoadGame();
	// }
	// public void Join() {
	// 	if (mode.Mode == GameMode.LocalCoop)
	// 	{
	// 		return;
	// 	}
	// 	status.Set("Connecting...");
	// 	net.StartClient();
	// }
	// public void Quit() => Application.Quit();
}
