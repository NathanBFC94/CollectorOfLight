﻿using UnityEngine;
using System.Collections;

public class scrPowerup : scrPoolable
{
	const float LEVITATE_HEIGHT = 4.0f;	// Height above the ground the light bobs up and down.
	const float BOB_HEIGHT = 1.0f;	// Maximum height of a bob.
	const float BOB_RATE = 0.5f;	// Rate at which the light bobs up and down.

	public enum Powerup
	{
		Speed = 0,
		Ghost = 1,
		Magnet = 2,
		MoreLight = 3
	}

	public Powerup Effect { get; private set; }

	private Vector3 spawnPosition;

	protected override void Update ()
	{
		// Bob up and down.
		transform.position = new Vector3(spawnPosition.x, spawnPosition.y + LEVITATE_HEIGHT + BOB_HEIGHT * Mathf.Sin (spawnPosition.x + spawnPosition.y + spawnPosition.z + Time.time * BOB_RATE), spawnPosition.z);


		base.Update ();
	}

	private void SetPowerup(Powerup effect)
	{
		Effect = effect;

		// Change appearance based on effect.
		switch(Effect)
		{
		case Powerup.Speed:
			// Make red.

			break;
		case Powerup.Ghost:
			// Make blue.

			break;
		case Powerup.Magnet:
			// Make pink.

			break;
		case Powerup.MoreLight:
			// Make yellow.

			break;
		}
	}

	
	void OnTriggerEnter(Collider other)
	{		
		// Deactivate.
		Expired = true;
	}


	// ---- scrPoolable ----
	
	/// <param name="initParams">float x, float z, Powerup effect</param>
	public override void Init(params object[] initParams)
	{
		Expired = false;
		
		float x = (float)initParams[0];
		float z = (float)initParams[1];
		transform.position = new Vector3(x, scrLandscape.Instance.GetHeight(x, z), z);
		spawnPosition = transform.position;

		// Get the powerup.
		SetPowerup((Powerup)initParams[2]);

		// Orient to the ground.
		transform.up = scrLandscape.Instance.GetNormal(x, z);

		// Push away from the ground.
		transform.position += transform.up * transform.localScale.y;
	}
}
