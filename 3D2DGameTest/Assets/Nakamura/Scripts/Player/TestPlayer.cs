using UnityEngine;

namespace Dimension.Player
{
    public class TestPlayer : MonoBehaviour
    {
        PlayerMover playerMovement;

        public bool IsStop { get; set; }

        //=====================================================
        void Start()
        {
            IsStop = false;
            // playerMovement = gameObject.AddComponent<PlayerMover3D>();
        }
        void Update()
        {
            if (IsStop) return;
            playerMovement.Mover();
        }
        //-----------------------------------------------------
        //  操作の変更
        //-----------------------------------------------------
        public void ChangeMover<PM>() where PM : PlayerMover
        {
            Destroy(playerMovement);
            playerMovement = gameObject.AddComponent<PM>();
        }
    }
}