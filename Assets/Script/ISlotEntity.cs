/* Created by and for usage of FF Studios (2021). */
using UnityEngine;

public interface ISlotEntity
{
	void SpawnClock( int level );
	Vector3 GetPosition();
	bool IsClockPresent(); // Returns -1 if no clock is present
	int CurrentClockLevel(); // Return current clock level
	void OnCurrentClockDeparted();
	void HandleIncomingClock( Clock clock );
}