using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dimension;

public class PlayerManagerController : MonoBehaviour
{
    PlayerMoveController playermove;
    public bool isStop { get; set; }
    public bool IsRight { get { return transform.localPosition.x <= 0; }}

    public GameController GController { get; private set; }
    public GameObject ClearLabel;
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
    //-----------------------------------------------------
    //  ゲームコントローラー受け取り
    //-----------------------------------------------------
    public void SetGameController(GameController gCon)
    {
        if (GController == null) GController = gCon;
    }
}
