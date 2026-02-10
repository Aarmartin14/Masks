using UnityEngine;
using Unity.Netcode;
using Masks.Layouts.Church;

namespace Masks.Networking.Cursor
{
	public sealed class NetworkCursorPresenter : NetworkBehaviour
	{
		[Header("Scene References (Assign in Player Prefab or Find at Runtime)")]
		[SerializeField] private Transform _selectionCursorVisual;
		[SerializeField] private Transform _targetingCursorVisual;

		private ChurchView _playerChurchView;
		private ChurchView _enemyChurchView;

		private NetworkPlayerCursor _cursor;

		private void Awake()
		{
			_cursor = GetComponent<NetworkPlayerCursor>();
		}

		public void Initialize(ChurchView playerChurchView, ChurchView enemyChurchView)
		{
			
			_playerChurchView = playerChurchView;
			_enemyChurchView = enemyChurchView;
			
			if (_playerChurchView == null || _enemyChurchView == null)
			{
				Debug.LogError("[NetworkCursorPresenter] ChurchView references must be provided for initialization.");
				return;
			}

			_cursor.selectedSlotIndex.OnValueChanged += (_, __) => Render();
			_cursor.isTargeting.OnValueChanged += (_, __) => Render();
			_cursor.targetedSlotIndex.OnValueChanged += (_, __) => Render();

			if (_selectionCursorVisual != null) _selectionCursorVisual.gameObject.SetActive(true);

			Render();
		}

		public override void OnNetworkSpawn()
		{
			
		}

		public override void OnDestroy()
		{
			// if (_cursor == null) return;
			// _cursor.selectedSlotIndex.OnValueChanged -= (_, __) => Render();
			// _cursor.isTargeting.OnValueChanged -= (_, __) => Render();
			// _cursor.targetedSlotIndex.OnValueChanged -= (_, __) => Render();

			base.OnDestroy();
		}

		private void OnAnyCursorValueChanged<T>(T _, T __) => Render();

		private void Render()
		{
			if (_playerChurchView == null || _enemyChurchView == null) return;

			var ownerId = OwnerClientId;

			var myChurch = (ownerId == 0) ? _playerChurchView : _enemyChurchView;
			var enemyChurch = (ownerId == 0) ? _enemyChurchView : _playerChurchView;

			int sel = _cursor.selectedSlotIndex.Value;
			myChurch.SetSelectedSlot(sel);

			if (_selectionCursorVisual != null) _selectionCursorVisual.position = myChurch.GetSlotCursorAnchor(sel).position;

			bool targeting = _cursor.isTargeting.Value;
			
			if (_targetingCursorVisual != null) _targetingCursorVisual.gameObject.SetActive(targeting);

			if (targeting)
			{
				int target = _cursor.targetedSlotIndex.Value;
				enemyChurch.SetTargetedSlot(target);

				if (_targetingCursorVisual != null) _targetingCursorVisual.position = enemyChurch.GetSlotCursorAnchor(target).position;
			} else
			{
				enemyChurch.ClearTargeting();
			}
		}
	}
}