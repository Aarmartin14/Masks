using UnityEngine;
using System.Collections.Generic;
using Masks.Layouts.Church;
using Masks.Views.Church;
using Masks.Views.Church.Factory;

public class ChurchView : MonoBehaviour
{
    [SerializeField] private RoomSlotProvider_FromChildren _slotProvider;
	[SerializeField] private RoomViewFactory_Prefab _roomViewFactory;

	private readonly Dictionary<int, IRoomView> _roomViewsBySlot = new();
	private int _selectedSlot = -1;
	private int _targetedSlot = -1;

	private void Awake()
	{
		if (_slotProvider == null) _slotProvider = GetComponentInChildren<RoomSlotProvider_FromChildren>();
		if (_roomViewFactory == null) _roomViewFactory = GetComponentInChildren<RoomViewFactory_Prefab>();
	}

	public void Bind(ChurchState churchState)
	{
		foreach (var kv in _roomViewsBySlot)
		{
			_roomViewFactory.Destroy(kv.Value);
		}
		_roomViewsBySlot.Clear();

		var ctx = new RoomViewContext();

		foreach (RoomState room in churchState.rooms)
		{
			if (!_slotProvider.TryGetSlot(room.SlotIndex, out var slot))
			{
				Debug.LogWarning($"No slot found for room with slot index {room.SlotIndex}");
				continue;
			}

			var view = _roomViewFactory.Create(room, slot, ctx);
			_roomViewsBySlot[room.SlotIndex] = view;
		}
	}

	public bool TryGetSlot(int slotIndex, out RoomSlot slot)
	{
		return _slotProvider.TryGetSlot(slotIndex, out slot);
	}

	public Transform GetSlotCursorAnchor(int slotIndex)
	{
		if (_roomViewsBySlot.TryGetValue(slotIndex, out var view) && view.CursorAnchor != null)
		{
			return view.CursorAnchor;
		}
		return _slotProvider.TryGetSlot(slotIndex, out var slot) ? slot.Anchor : transform;
	}

	public void SetSelectedSlot(int slotIndex)
	{
		if (_selectedSlot == slotIndex) return;

		if (_roomViewsBySlot.TryGetValue(_selectedSlot, out var prevSelectedView))
		{
			prevSelectedView.SetSelected(false);
		}
		_selectedSlot = slotIndex;
		if (_roomViewsBySlot.TryGetValue(_selectedSlot, out var newSelectedView))
		{
			newSelectedView.SetSelected(true);
		}
	}

	public void SetTargetedSlot(int slotIndex)
	{
		if (_targetedSlot == slotIndex) return;

		if (_roomViewsBySlot.TryGetValue(_targetedSlot, out var prevTargetedView))
		{
			prevTargetedView.SetTargeted(false);
		}
		_targetedSlot = slotIndex;
		if (_roomViewsBySlot.TryGetValue(_targetedSlot, out var newTargetedView))
		{
			newTargetedView.SetTargeted(true);
		}
	}

	public void ClearTargeting()
	{
		if (_roomViewsBySlot.TryGetValue(_targetedSlot, out var prevTargetedView))
		{
			prevTargetedView.SetTargeted(false);
		}
		_targetedSlot = -1;
	}
}
