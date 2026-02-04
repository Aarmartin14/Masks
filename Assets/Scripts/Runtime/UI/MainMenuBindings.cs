using UnityEngine;
using UnityEngine.UI;

public class MainMenuBindings : MonoBehaviour
{
	[SerializeField] private Button joinButton;
	private GameModeState mode;

	private void Start()
	{
		mode = FindFirstObjectByType<GameModeState>();
		if (mode == null)
		{
			Debug.LogError("GameModeState not found. Did you start from Bootstrap?");
		}

		RefreshButtons();
	}

	public void SetLocalCoop(bool on)
	{
		if (mode == null) return;
		mode.SetLocalCoop(on);
		RefreshButtons();
	}

	private void RefreshButtons()
	{
		if (joinButton != null)
		{
			joinButton.interactable = (mode != null && mode.Mode != GameMode.LocalCoop);
		}
	}
}
