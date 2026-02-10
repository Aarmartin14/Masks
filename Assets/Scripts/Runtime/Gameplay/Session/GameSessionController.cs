using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.Netcode;
using Masks.Layouts.Church;
using Masks.Cursor;
using Masks.Networking.Cursor;

public sealed class GameSessionController : MonoBehaviour
{
    [Header("Church References (Scene Instances)")]
	[SerializeField] private ChurchView _playerChurchView;
	[SerializeField] private GridSlotNavigator _playerNavigator;

	[SerializeField] private ChurchView _enemyChurchView;
	[SerializeField] private GridSlotNavigator _enemyNavigator;

	[Header("Optional Local Cursors (Scene Instances)")]
	[SerializeField] private RoomSelectionController _p1Selection;
	[SerializeField] private RoomSelectionController _p2Selection;

	[Header("Room Setup")]
	[SerializeField] private int _roomCount = 12;
	// [SerializeField] private int _p1startSlotIndex = 0;
	// [SerializeField] private int _p2startSlotIndex = 0;

	private ChurchState _playerChurchState;
	private ChurchState _enemyChurchState;

	private static readonly string[] DefaultRoomTypeIDs =
	{
		"room.sanctuary", "room.altar", "room.pews", "room.pews", "room.mission", "room.tithe", "room.workshop", "room.wrath", "room.wrath", "room.wrath", "room.wrath", "room.sacrifice"
	};

	private void Awake()
	{
		if (_playerChurchView == null || _playerNavigator == null ) Debug.LogError("Player church view and navigator references must be set in the inspector.");
		if (_enemyChurchView == null || _enemyNavigator == null) Debug.LogError("Enemy church view and navigator references must be set in the inspector.");
	}

	private void Start()
	{
		_playerChurchState = BuildChurchState(_playerChurchView, _roomCount);
		_enemyChurchState = BuildChurchState(_enemyChurchView, _roomCount);

		_playerChurchView.Bind(_playerChurchState);
		_enemyChurchView.Bind(_enemyChurchState);

		var mode = FindFirstObjectByType<GameModeState>();
		bool isLocalCoop = mode != null && mode.Mode == GameMode.LocalCoop;

		if (isLocalCoop)
		{
			if (_p1Selection != null) _p1Selection.gameObject.SetActive(true);
			if (_p2Selection != null) _p2Selection.gameObject.SetActive(true);
			return;
		}

		InitializeAllCursorPresenters();

		if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsHost)
		{
			NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
		}
	}

	private void OnDestroy()
	{
		if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsHost)
		{
			NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
		}
	}

	private void OnClientConnected(ulong clientId)
	{
		InitializeAllCursorPresenters();
	}

	private void InitializeAllCursorPresenters()
	{
		foreach (var presenter in FindObjectsByType<NetworkCursorPresenter>(FindObjectsSortMode.None))
		{
			presenter.Initialize(_playerChurchView, _enemyChurchView);
		}

		foreach (var input in FindObjectsByType<NetworkCursorInput>(FindObjectsSortMode.None))
		{
			input.Initialize(_playerChurchView, _playerNavigator, _enemyChurchView, _enemyNavigator);
		}
	}

	private static ChurchState BuildChurchState(ChurchView churchView, int roomCount)
	{
		var slotProvider = churchView.GetComponentInChildren<RoomSlotProvider_FromChildren>();
		int count = slotProvider.Slots.Count;

		var rooms = new List<RoomState>();

		for (int slotIndex = 0; slotIndex < count; slotIndex++)
		{
			string roomTypeId = DefaultRoomTypeIDs[slotIndex % DefaultRoomTypeIDs.Length];

			var room = new RoomState(roomTypeId, slotIndex);
			rooms.Add(room);
		}

		return new ChurchState(rooms);
	}
}
