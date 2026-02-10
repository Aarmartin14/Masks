using UnityEngine;
using System.Collections.Generic;

namespace Masks.Layouts.Church
{
	public sealed class GridSlotNavigator : MonoBehaviour, ISlotNavigator
	{
		[SerializeField] private RoomSlotProvider_FromChildren _slotProvider;

		private readonly Dictionary<(int r, int c), int> _coordToSlot = new();
		private readonly Dictionary<int, (int r, int c)> _slotToCoord = new();

		private void Awake()
		{
			if (_slotProvider == null) _slotProvider = GetComponentInChildren<RoomSlotProvider_FromChildren>();

			Rebuild();
		}

		[ContextMenu("Rebuild Navigator")]
		public void Rebuild()
		{
			_coordToSlot.Clear();
			_slotToCoord.Clear();

			if (_slotProvider == null)
			{
				Debug.LogWarning("No IRoomSlotProvider assigned to GridSlotNavigator", this);
				return;
			}

			foreach (var slot in _slotProvider.Slots)
			{
				var coord = (slot.Row, slot.Column);
				_coordToSlot[coord] = slot.slotIndex;
				_slotToCoord[slot.slotIndex] = coord;
			}
		}

		public int GetNextSlotClamped(int currentSlotIndex, NavigationDirection direction)
		{
			if (!_slotToCoord.TryGetValue(currentSlotIndex, out var coord))
			{
				Debug.LogWarning($"Current slot index {currentSlotIndex} not found in navigator", this);
				return currentSlotIndex;
			}

			var (r, c) = coord;
			(int dr, int dc) delta = direction switch
			{
				NavigationDirection.Up => (-1, 0),
				NavigationDirection.Down => (1, 0),
				NavigationDirection.Left => (0, -1),
				NavigationDirection.Right => (0, 1),
				_ => (0, 0)
			};

			var target = (r + delta.dr, c + delta.dc);
			return _coordToSlot.TryGetValue(target, out var nextSlot) ? nextSlot : currentSlotIndex;
		}
	}
}