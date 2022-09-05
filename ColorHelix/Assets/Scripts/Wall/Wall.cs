using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace rhcodepi
{
    public class Wall : MonoBehaviour
    {
        ///<WallFragment>
        /// wallFragment is a circle wall
        /// this circle wall left semicircle is wall1
        /// another area is wall2
        ///</WallFragment>
        private GameObject wallFragment;
        private GameObject wall1, wall2; 
        [SerializeField] float wallFragmentCount;
        [SerializeField] Vector3 offset;
        float rotationZ;
        const float rotationZMax = 180;
        private void Awake() => wallFragment = Resources.Load("WallFragment") as GameObject;
        void Start()
        {
            SpawnWall();   
        }
        void SpawnWall()
        {
            wall1 = new GameObject();
            wall2 = new GameObject();

            wall1.name = "LeftSemiCircle";
            wall2.name = "RightSemiCircle";

            wall1.transform.SetParent(transform);
            wall2.transform.SetParent(transform);

            for (int i = 0; i < wallFragmentCount; i++)
            {
                GameObject wallPiece = Instantiate(wallFragment, transform.position + offset, Quaternion.Euler(0,0,rotationZ));
                rotationZ += (360 / wallFragmentCount);
                if(rotationZ < rotationZMax)
                {
                    wallPiece.transform.SetParent(wall1.transform);
                    wallPiece.GetComponent<Renderer>().material.color = Color.red;
                }
                else
                    wallPiece.transform.SetParent(wall2.transform);
            }
        }
    }

}
