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
        [SerializeField] GameObject helix, finishLine, text;
        bool colorBump, star, passes;
        GameObject[] walls2, walls1;
        private int wallCount, colorBumpZ, score;
        void Awake()
        {
            instance = this;
            GenerateColor();
        }

        void Start()
        {
            if (PlayerPrefs.GetInt("Level") == 0)
                PlayerPrefs.SetInt("Level", 1);
            GenerateLevel();
        }

        void Update()
        {
            SetScore();
        }

        public void GenerateLevel()
        {
            GenerateColor();

            if (PlayerPrefs.GetInt("Level") >= 1 && PlayerPrefs.GetInt("Level") <= 3)
                wallCount = 8;
            else if (PlayerPrefs.GetInt("Level") >= 4 && PlayerPrefs.GetInt("Level") <= 6)
                wallCount = 10;
            else if (PlayerPrefs.GetInt("Level") >= 7 && PlayerPrefs.GetInt("Level") <= 10)
                wallCount = 12;

            wallSpawnPos = wallDistance;
            DeleteWall();
            colorBump = false;
            SpawnWall();
        }

        void SetScore()
        {
            walls1 = GameObject.FindGameObjectsWithTag("Collectable");
            if (walls1.Length > wallCount)
                wallCount = walls1.Length;
            if (wallCount > walls1.Length)
            {
                if (passes)
                {
                    passes = false;
                    if (star)
                    {
                        star = false;
                        score += PlayerPrefs.GetInt("Level") * 2;
                        text.GetComponent<TextMesh>().text = $"Perfect {PlayerPrefs.GetInt("Level") * 2}";
                        text.GetComponent<Animator>().SetTrigger("Popup");
                    }
                    else
                    {
                        score += PlayerPrefs.GetInt("Level");
                        text.GetComponent<TextMesh>().text = $"{PlayerPrefs.GetInt("Level")}";
                        text.GetComponent<Animator>().SetTrigger("Popup");
                    }
                        
                }
            }
        }

        public void GenerateColor()
        {
            passesColor = colors[Random.Range(0, colors.Length)];
            cancelColor = colors[Random.Range(0, colors.Length)];

            while (passesColor == cancelColor)
                cancelColor = colors[Random.Range(0, colors.Length)];

            Ball._CurrentColor = passesColor;
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
            if (PlayerPrefs.GetInt("Level") > 4)
                Destroy(GameObject.FindGameObjectWithTag("ColorBump"));
        }

        public Color _PassesColor { get => passesColor; set { passesColor = value; } }
        public Color _CancelColor { get => cancelColor; set { cancelColor = value; } }
        public Color[] _Colors { get => colors; }
        public int _WallSpawnPos { get => wallSpawnPos; }
        public int _ColorBumpZ { get => colorBumpZ; }
        public bool _isStar { get => star; set { star = value; } }
        public bool _IsPasses { get => passes; set { passes = value; } }
        public int _Score { get => score; set { score = value; } }
    }

}
