using UnityEngine;
using Masks.Layouts.Church;

namespace Masks.Views.Church
{
	public interface IChurchView
	{
		bool TryGetSlot(int slotIndex, out RoomSlot slot);
		Transform GetCursorAnchor(int slotIndex);
		void SetSelectedSlot(int slotIndex);
		void SetTargetedSlot(int slotIndex);
		void ClearTargeting();
	}

}