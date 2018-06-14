using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerController : MonoBehaviour
{
    PlayerMoveController playermove;
    public bool isStop { get; set; }

	// Use this for initialization
	void Start ()
    {
        isStop = false;
        PlayerMoveChange<Player3DController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isStop) return;
        playermove.Movement();
    }

    public void PlayerMoveChange<PM>() where PM : PlayerMoveController
    {
        Destroy(playermove);
        playermove = gameObject.AddComponent<PM>();
    }
}
