using UnityEngine;
using Masks.Layouts.Church;

namespace Masks.Views.Church.Factory
{
	public interface IRoomViewFactory
	{
		IRoomView Create(RoomState state, RoomSlot slot, RoomViewContext ctx);
		void Destroy(IRoomView view);
	}
}
