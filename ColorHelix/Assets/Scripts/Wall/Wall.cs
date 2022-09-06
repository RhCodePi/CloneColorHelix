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
        private GameObject wallFragment, star;
        private GameObject wall1, wall2; 
        [SerializeField] float wallFragmentCount;
        [SerializeField] Vector3 offset;
        float rotationZ;
        float rotationZMax = 180;
        private void Awake()
        {
            wallFragment = Resources.Load("WallFragment") as GameObject;
            star = Resources.Load("PerfectStar") as GameObject;
        }
        void Start()
        {
            GenerateWall();
        }
        void GenerateWall()
        {
            wall1 = new GameObject();
            wall2 = new GameObject();

            wall1.name = "LeftSemiCircle";
            wall2.name = "RightSemiCircle";

            wall1.transform.SetParent(transform);
            wall2.transform.SetParent(transform);

            if(Random.value <= 0.25f && PlayerPrefs.GetInt("Level") >= 9)
               rotationZMax = 90; 
            else
                rotationZMax = 180;

            wall1.tag = "Pass";
            wall1.AddComponent<BoxCollider>();
            wall1.GetComponent<BoxCollider>().isTrigger = true;
            wall1.GetComponent<BoxCollider>().center = new Vector3(-0.57f, -0.008f, wall2.transform.position.z);
            wall1.GetComponent<BoxCollider>().size = new Vector3(0.9f, 1.9f, 0.15f);

            wall2.tag = "Cancel";
            wall2.AddComponent<BoxCollider>();
            wall2.GetComponent<BoxCollider>().isTrigger = true;
            wall2.GetComponent<BoxCollider>().center = new Vector3(0.47f, -0.008f, wall2.transform.position.z);
            wall2.GetComponent<BoxCollider>().size = new Vector3(0.9f, 1.9f, 0.15f);

            for (int i = 0; i < wallFragmentCount; i++)
            {
                GameObject wallPiece = Instantiate(wallFragment, transform.position + offset, Quaternion.Euler(0,0,rotationZ));
                wallPiece.transform.localPosition = Vector3.zero;
                rotationZ += (360 / wallFragmentCount);
                if(rotationZ < rotationZMax)
                {
                    wallPiece.transform.SetParent(wall1.transform);
                    wallPiece.tag = "Passes";
                }
                else
                    wallPiece.transform.SetParent(wall2.transform);
            }

            wall1.transform.localPosition = Vector3.zero;
            wall2.transform.localPosition = Vector3.zero;

            wall1.transform.localRotation = Quaternion.Euler(Vector3.zero);
            wall2.transform.localRotation = Quaternion.Euler(Vector3.zero);

            if(rotationZMax == 180)
                AddStar(wall1.transform.GetChild(25).gameObject);
            else if(rotationZMax == 90)
                AddStar(wall1.transform.GetChild(13).gameObject);
        }

        void AddStar(GameObject fragment)
        {
            GameObject starObj = Instantiate(star, transform.position, Quaternion.identity);

            starObj.transform.SetParent(fragment.transform);
            starObj.transform.localPosition = new Vector3(0, 0.8f, -0.1f);
        }
    }

}
