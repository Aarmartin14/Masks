using UnityEngine;

namespace Masks.Views.Church
{
	public sealed class RoomView : MonoBehaviour, IRoomView
	{
		[SerializeField] private Transform _cursorAnchor;
		[SerializeField] private GameObject _selectedVisual;
		[SerializeField] private GameObject _targetedVisual;

		public int SlotIndex { get; private set; }
		public Transform CursorAnchor => _cursorAnchor != null ? _cursorAnchor : transform;

		private RoomState _state;

		private void Reset()
		{
			_cursorAnchor = transform;
		}

		public void Bind(RoomState state, RoomViewContext ctx)
		{
			_state = state;
			SlotIndex = state.SlotIndex;

			Refresh();
		}

		public void Unbind()
		{
			_state = null;
		}

		public void SetSelected(bool selected)
		{
			if (_selectedVisual != null) _selectedVisual.SetActive(selected);
		}

		public void SetTargeted(bool targeted)
		{
			if (_targetedVisual != null) _targetedVisual.SetActive(targeted);
		}

		private void Refresh()
		{
			//SetSelected(_state.IsSelected);
			//SetTargeted(_state.IsTargeted);
		}
	}
}