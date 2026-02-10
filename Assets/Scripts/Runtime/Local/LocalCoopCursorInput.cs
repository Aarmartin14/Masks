using UnityEngine;
using UnityEngine.InputSystem;
using Masks.Cursor;
using Masks.Layouts.Church;

public sealed class LocalCoopCursorInput : MonoBehaviour
{
    [SerializeField] private RoomSelectionController selection;

	private Vector2 _move;
	private bool _stickEngaged;

	public void OnNavigate(InputAction.CallbackContext ctx)
	{
		_move = ctx.ReadValue<Vector2>();
	}

	private void Update()
	{
		if (selection == null) return;

		var dirOpt = ToDirection(_move);
		if (!dirOpt.HasValue) {
			_stickEngaged = false;
			return;
		}

		if (_stickEngaged) return;
		_stickEngaged = true;

		selection.Move(dirOpt.Value);
	}

	public void SetSelection(RoomSelectionController selectionController)
	{
		selection = selectionController;
	}

	private static NavigationDirection? ToDirection(Vector2 move)
	{
		const float dead = 0.5f;
		if (move.sqrMagnitude < dead * dead) return null;

		if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
		{
			return move.x > 0 ? NavigationDirection.Right : NavigationDirection.Left;
		}
		else
		{
			return move.y > 0 ? NavigationDirection.Down : NavigationDirection.Up;
		}
	}
}
