using UnityEngine;

public enum GameMode
{
	Online,
	LocalCoop
}

public class GameModeState : MonoBehaviour
{
    public GameMode Mode { get; private set; } = GameMode.Online;
	public void SetMode(int dropdownIndex) => Mode = (GameMode)dropdownIndex;
	public void SetLocalCoop(bool on) => Mode = on ? GameMode.LocalCoop : GameMode.Online;
}
