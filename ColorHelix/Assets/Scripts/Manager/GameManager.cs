using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rhcodepi
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Color[] colors;
        private Color passesColor, cancelColor;
        public static GameManager instance;
        [SerializeField] private int wallDistance = 6, wallSpawnPos = 6;
        [SerializeField] GameObject helix, finishLine;
        bool colorBump;
        GameObject[] walls2;
        private int wallCount, colorBumpZ;
        void Awake()
        {
            instance = this;
            GenerateColor();
        }

        void Start()
        {
            PlayerPrefs.GetInt("Level", 1);
            GenerateLevel();
        }

        void Update()
        {
            Debug.Log(PlayerPrefs.GetInt("Level"));
        }

        public void GenerateLevel()
        {
            DeleteWall();
            if (PlayerPrefs.GetInt("Level") >= 0 && PlayerPrefs.GetInt("Level") <= 3)
                wallCount = 8;
            else if (PlayerPrefs.GetInt("Level") >= 4 && PlayerPrefs.GetInt("Level") <= 6)
                wallCount = 10;
            else if (PlayerPrefs.GetInt("Level") >= 7 && PlayerPrefs.GetInt("Level") <= 10)
                wallCount = 12;

            wallSpawnPos = wallDistance;
            colorBump = false;
            SpawnWall();
        }

        public void GenerateColor()
        {
            passesColor = colors[Random.Range(0, colors.Length)];
            cancelColor = colors[Random.Range(0, colors.Length)];

            while (passesColor == cancelColor)
                cancelColor = colors[Random.Range(0, colors.Length)];

            Ball._CurrentColor = passesColor;
            Debug.Log("rese");
        }

        void SpawnWall()
        {
            float random = Random.Range(1, wallCount - 1);
            for (int i = 0; i < wallCount; i++)
            {
                GameObject wall;
                if (random == i && !colorBump && PlayerPrefs.GetInt("Level") > 4)
                {
                    wall = Instantiate(Resources.Load("ColorBump") as GameObject, transform.position, Quaternion.identity);
                    colorBumpZ = wallSpawnPos;
                    colorBump = true;
                }
                else if (Random.value <= 0.1f && PlayerPrefs.GetInt("Level") > 9)
                {
                    wall = Instantiate(Resources.Load("Walls") as GameObject, transform.position, Quaternion.identity);
                }
                else
                {
                    wall = Instantiate(Resources.Load("Wall") as GameObject, transform.position, Quaternion.identity);
                }

                wall.transform.SetParent(helix.transform);
                float randomRotate = Random.Range(0, 360);
                wall.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, randomRotate));
                wall.transform.localPosition = new Vector3(0, 0.0012f, wallSpawnPos);
                wallSpawnPos += wallDistance;

                if (i <= wallCount)
                    finishLine.transform.localPosition = new Vector3(0, 0.012f, wallSpawnPos);
            }
        }

        void DeleteWall()
        {
            walls2 = GameObject.FindGameObjectsWithTag("Cancel");
            if (walls2.Length > 0)
                for (int i = 0; i < walls2.Length; i++)
                {
                    Destroy(walls2[i].transform.parent.gameObject);
                }

            Destroy(GameObject.FindGameObjectWithTag("ColorBump"));
        }

        public Color _PassesColor { get => passesColor; set { passesColor = value; } }
        public Color _CancelColor { get => cancelColor; set { cancelColor = value; } }
        public int _WallSpawnPos { get => wallSpawnPos; }
        public int _ColorBumpZ { get => colorBumpZ; }
        public Color[] _Colors { get => colors; }
    }

}
