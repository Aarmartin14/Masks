using UnityEngine;
using UnityEngine.InputSystem;
using Masks.Cursor;
using Masks.Layouts.Church;

public sealed class LocalCoopBinder : MonoBehaviour
{
    [Header("Scene Churches")]
	[SerializeField] private ChurchView _playerChurchView;
	[SerializeField] private GridSlotNavigator _playerNavigator;
	
	[SerializeField] private ChurchView _enemyChurchView;
	[SerializeField] private GridSlotNavigator _enemyNavigator;

	[Header("Start slots")]
	[SerializeField] private int _p1StartSlotIndex = 0;
	[SerializeField] private int _p2StartSlotIndex = 0;

	private int _joinedCount = 0;

	public void BindNewLocalPlayer(GameObject playerObject)
	{
		if (_joinedCount >= 2)
		{
			Debug.LogWarning("More than 2 players joined in local coop mode. Extra players will not be bound.");
			return;
		}

		var selection = playerObject.GetComponentInChildren<RoomSelectionController>(true);
		var cursorView = playerObject.GetComponentInChildren<SelectionCursorView>(true);
		var input = playerObject.GetComponentInChildren<LocalCoopCursorInput>(true);

		if (selection == null || cursorView == null || input == null)
		{
			Debug.LogError("[LocalCoopBinder] Player object is missing required components (RoomSelectionController, SelectionCursorView, LocalCoopCursorInput).");
			return;
		}

		bool isP1 = _joinedCount == 0;

		var churchView = isP1 ? _playerChurchView : _enemyChurchView;
		var navigator = isP1 ? _playerNavigator : _enemyNavigator;
		int startSlot = isP1 ? _p1StartSlotIndex : _p2StartSlotIndex;

		selection.SetDependencies(churchView, navigator, cursorView, startSlot);
		// selection.ApplySelectionVisualsAndCursor();
		input.SetSelection(selection);

		_joinedCount++;
	}

	private void OnPlayerJoined(PlayerInput pi)
	{
		Debug.Log($"[LocalCoopBinder] Player joined: {pi.gameObject.name}");
		BindNewLocalPlayer(pi.gameObject);
	}
}
