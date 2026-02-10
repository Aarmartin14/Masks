using UnityEngine;
using Unity.Netcode;

namespace Masks.Networking.Cursor
{
	public sealed class NetworkPlayerCursor : NetworkBehaviour
	{
		public NetworkVariable<int> selectedSlotIndex = new(
			0,
			NetworkVariableReadPermission.Everyone,
			NetworkVariableWritePermission.Owner
		);

		public NetworkVariable<bool> isTargeting = new(
			false,
			NetworkVariableReadPermission.Everyone,
			NetworkVariableWritePermission.Owner
		);

		public NetworkVariable<int> targetedSlotIndex = new(
			0,
			NetworkVariableReadPermission.Everyone,
			NetworkVariableWritePermission.Owner
		);
	}
}
