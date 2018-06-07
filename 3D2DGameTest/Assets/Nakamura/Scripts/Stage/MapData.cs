using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dimension.Stage
{
    public enum Dir
    {
        None    = 0,
        Forward = 1,
        RIght   = 2,
        Back    = 3,
        Left    = 4
    }
    public class MapData : MonoBehaviour
    {
        StageObject[,,] mapDataArr3D = new StageObject[10, 10, 10];   


    }
}