using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Masks.Layouts.Church
{
	public class RoomSlotProvider_FromChildren : MonoBehaviour
	{
		private Transform _root;

		private readonly List<RoomSlot> _slots = new();
		private Dictionary<int, RoomSlot> _byIndex = new();

		public IReadOnlyList<RoomSlot> Slots => _slots;

		private void Awake()
		{
			Rebuild();
		}

		public void Rebuild()
		{
			if (_root == null) _root = transform;

			_slots.Clear();
			_slots.AddRange(_root.GetComponentsInChildren<RoomSlot>(true));

			_slots.Sort((a, b) => a.slotIndex.CompareTo(b.slotIndex));
			_byIndex = _slots
				.GroupBy(s => s.slotIndex)
				.ToDictionary(g => g.Key, g => g.First());

			var dupes = _slots
				.GroupBy(s => s.slotIndex)
				.Where(g => g.Count() > 1)
				.Select(g => g.Key)
				.ToList();
			if (dupes.Count > 0)
			{
				Debug.LogWarning($"Found duplicate RoomSlot indices: {string.Join(", ", dupes)}", this);
			}
		}

		public bool TryGetSlot(int slotIndex, out RoomSlot slot)
		{
			return _byIndex.TryGetValue(slotIndex, out slot);
		}
	}
}
