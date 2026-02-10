using Masks.Layouts.Church;
using UnityEngine;

namespace Masks.Cursor
{
	public class RoomSelectionController : MonoBehaviour
	{
		[SerializeField] private ChurchView _churchView;
		[SerializeField] private GridSlotNavigator _navigator;
		[SerializeField] private SelectionCursorView _cursorView;

		[SerializeField] private int _startSlotIndex = 0;
		private int CurrentSlotIndex { get; set; }

		private void Start()
		{
			CurrentSlotIndex = _startSlotIndex;
			if (_churchView == null || _navigator == null || _cursorView == null) return;
			ApplySelectionVisualsAndCursor();
		}

		public void Move(NavigationDirection direction)
		{
			var next = _navigator.GetNextSlotClamped(CurrentSlotIndex, direction);
			if (next == CurrentSlotIndex) return;

			CurrentSlotIndex = next;
			ApplySelectionVisualsAndCursor();
		}

		private void ApplySelectionVisualsAndCursor()
		{
			if (_churchView == null || _cursorView == null) return;
			
			_churchView.SetSelectedSlot(CurrentSlotIndex);
			var anchor = _churchView.GetSlotCursorAnchor(CurrentSlotIndex);
			_cursorView.SetTarget(anchor);
		}

		public void Configure(ChurchView churchView, GridSlotNavigator navigator, int startSlotIndex = 0)
		{
			_churchView = churchView;
			_navigator = navigator;
			_startSlotIndex = startSlotIndex;
		}

		public void SetDependencies(ChurchView churchView, GridSlotNavigator navigator, SelectionCursorView cursorView, int startSlotIndex)
		{
			_churchView = churchView;
			_navigator = navigator;
			_cursorView = cursorView;
			_startSlotIndex = startSlotIndex;

			ApplySelectionVisualsAndCursor();
		}
	}
}