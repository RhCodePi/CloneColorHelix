using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rhcodepi
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] int eulerBound;
        void Update() => transform.eulerAngles = new Vector3(0, 0, Helix.instance.GetAnglesZ(eulerBound));
    }
}

