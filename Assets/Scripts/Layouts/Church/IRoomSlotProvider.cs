using UnityEngine;
using System.Collections.Generic;

namespace Masks.Layouts.Church
{
	public interface IRoomSlotProvider
	{
		IReadOnlyList<RoomSlot> Slots { get; }
		bool TryGetSlot(int slotIndex, out RoomSlot slot);
	}
}