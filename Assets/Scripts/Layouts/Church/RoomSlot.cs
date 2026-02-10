using UnityEngine;

namespace Masks.Layouts.Church
{
	public sealed class RoomSlot : MonoBehaviour
	{
		[Header("Identity")]
		[Min(0)] public int slotIndex;

		[Header("Grid Position")]
		public int Row;
		public int Column;

		[Header("Placement")]
		public Transform Anchor;

		private void Reset()
		{
			Anchor = transform;
		}

		private void OnValidate()
		{
			if (Anchor == null) Anchor = transform;
		}
	}
}