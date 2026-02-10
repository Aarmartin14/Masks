using UnityEngine;
//using Masks.Runtime.Gameplay.Rooms;

namespace Masks.Views.Church
{
	public interface IRoomView
	{
		int SlotIndex { get; }
		void Bind(RoomState state, RoomViewContext ctx);
		void Unbind();

		void SetSelected(bool selected);
		void SetTargeted(bool targeted);

		Transform CursorAnchor { get; }
	}
}