using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlowController : MonoBehaviour
{
	[SerializeField] private string mainMenuSceneName = "MainMenu";
	[SerializeField] private string gameSceneName = "Game";

    private void Start()
	{
		if (SceneManager.GetActiveScene().name != mainMenuSceneName)
		{
			SceneManager.LoadScene(mainMenuSceneName);
		}
	}

	public void LoadMainMenu()
	{
		SceneManager.LoadScene(mainMenuSceneName);
	}

	public void LoadGame()
	{
		SceneManager.LoadScene(gameSceneName);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
