using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dimension.Player
{
    public class PlayerMoverNull : PlayerMoveController
    {
        //-----------------------------------------------------
        //  初期化
        //-----------------------------------------------------
        public override void PositionInitialization()
        {
            if (rig == null) rig = GetComponent<Rigidbody>();
            rig.useGravity = false;
            rig.velocity = new Vector3(0, 0, 0);
        }
        //-----------------------------------------------------
        //  行動
        //-----------------------------------------------------
        public override void Movement() { return; }

        void OnDestroy()
        {
            rig.useGravity = true;
        }
    }
}