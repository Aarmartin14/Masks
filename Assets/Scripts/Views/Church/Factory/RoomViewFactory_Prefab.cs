using UnityEngine;
using Masks.Layouts.Church;

namespace Masks.Views.Church.Factory
{
	public sealed class RoomViewFactory_Prefab : MonoBehaviour, IRoomViewFactory
	{
		[SerializeField] private RoomView _prefab;

		public IRoomView Create(RoomState state, RoomSlot slot, RoomViewContext ctx)
		{
			var instance = Instantiate(_prefab, slot.Anchor);
			instance.Bind(state, ctx);
			return instance;
		}

		public void Destroy(IRoomView view)
		{
			if (view is MonoBehaviour mb)
			{
				Destroy(mb.gameObject);
			}
		}
	}
}