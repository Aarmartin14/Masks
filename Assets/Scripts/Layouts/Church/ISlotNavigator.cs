using UnityEngine;

namespace Masks.Layouts.Church
{
	public enum NavigationDirection
	{
		Up,
		Down,
		Left,
		Right
	}

	public interface ISlotNavigator
	{
		int GetNextSlotClamped(int currentSlotIndex, NavigationDirection direction);
	}
}