using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using Masks.Layouts.Church;

namespace Masks.Networking.Cursor
{
	public sealed class NetworkCursorInput : NetworkBehaviour
	{
		[Header("Repeat")]
		[SerializeField] private float _repeatDelay = 0.05f;
		private bool _stickEngaged;

		private NetworkPlayerCursor _cursor;

		private ChurchView _playerChurchView;
		private GridSlotNavigator _playerNavigator;

		private ChurchView _enemyChurchView;
		private GridSlotNavigator _enemyNavigator;

		private Vector2 _move;
		private float _nextMoveTime;

		private void Awake()
		{
			_cursor = GetComponent<NetworkPlayerCursor>();
		}

		public void Initialize(ChurchView playerChurchView, GridSlotNavigator playerNavigator, ChurchView enemyChurchView, GridSlotNavigator enemyNavigator)
		{
			_playerChurchView = playerChurchView;
			_playerNavigator = playerNavigator;
			_enemyChurchView = enemyChurchView;
			_enemyNavigator = enemyNavigator;

			if (_playerChurchView == null || _playerNavigator == null || _enemyChurchView == null || _enemyNavigator == null)
			{
				Debug.LogError("[NetworkCursorInput] ChurchView and GridSlotNavigator references must be provided for initialization.");
				return;
			}
		}

		public override void OnNetworkSpawn()
		{
			var pi = GetComponentInChildren<PlayerInput>(true);
			if (pi == null) return;

			if (IsOwner)
			{
				pi.enabled = true;
				pi.ActivateInput();
				pi.neverAutoSwitchControlSchemes = true;
			} else
			{
				pi.DeactivateInput();
				pi.enabled = false;
			}

			Debug.Log($"[CursorInput] local={NetworkManager.Singleton.LocalClientId} owner={OwnerClientId} IsOwner={IsOwner}");
		}

		// DELETE THIS: This is a temporary workaround to disable input when the application loses focus, since PlayerInput doesn't handle that automatically.
		void OnApplicationFocus(bool hasFocus)
		{
			if (!IsOwner) return;

			var pi = GetComponentInChildren<PlayerInput>(true);
			if (pi != null) pi.enabled = hasFocus;
		}
		// END DELETE

		public void OnNavigate(InputAction.CallbackContext ctx)
		{
			if (!IsOwner) return;

			_move = ctx.ReadValue<Vector2>();
		}

		public void OnToggleTargeting(InputAction.CallbackContext ctx)
		{
			if (!IsOwner) return;
			if (!ctx.performed) return;

			_cursor.isTargeting.Value = !_cursor.isTargeting.Value;
		}

		private void Update()
		{
			if (!IsOwner) return;
			if (_playerNavigator == null || _enemyNavigator == null) return;

			if (Time.unscaledTime < _nextMoveTime) return;

			var dirOpt = ToDirection(_move);
			if (!dirOpt.HasValue) {
				_stickEngaged = false;
				return;
			}

			if (_stickEngaged) return;

			_stickEngaged = true;

			_nextMoveTime = Time.unscaledTime + _repeatDelay;

			bool targeting = _cursor.isTargeting.Value;
			bool ownerIsP1 = OwnerClientId == 0;

			GridSlotNavigator nav;
			int current;

			if (!targeting)
			{
				nav = ownerIsP1 ? _playerNavigator : _enemyNavigator;
				current = _cursor.selectedSlotIndex.Value;
				_cursor.selectedSlotIndex.Value = nav.GetNextSlotClamped(current, dirOpt.Value);
			}
			else
			{
				nav = ownerIsP1 ? _enemyNavigator : _playerNavigator;
				current = _cursor.targetedSlotIndex.Value;
				_cursor.targetedSlotIndex.Value = nav.GetNextSlotClamped(current, dirOpt.Value);
			}
		}

		private static NavigationDirection? ToDirection(Vector2 move)
		{
			const float dead = 0.5f;
			if (move == Vector2.zero) return null;
			if (move.sqrMagnitude < dead * dead) return null;

			if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
			{
				return (move.x > 0) ? NavigationDirection.Right : NavigationDirection.Left;
			}
			else
			{
				return (move.y > 0) ? NavigationDirection.Down : NavigationDirection.Up;
			}
		}
	}
}