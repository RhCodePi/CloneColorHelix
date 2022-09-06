using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace rhcodepi
{
    public class Ball : MonoBehaviour
    {
        private static float posZ = -8f;
        private float height = 0.58f;
        private static bool move;
        [SerializeField] float speed, accleration;
        private static Color currentColor;
        float lerpAmount;
        bool isChange;
        void Start() => move = false;

        void Update()
        {
            // check pressing the scrren
            if (Touch.isPressing)
                move = true;
            // then if pressing the scrren, ball has moving
            if (move)
                Ball.posZ += speed * accleration;
            transform.position = new Vector3(0, height, Ball.posZ);

            UpdateColor();
        }

        void UpdateColor()
        {
            transform.GetComponent<MeshRenderer>().material.color = currentColor;
            // if passing the colorbump then, the ball color chnage one second, what is color the colorBump. 
            if (isChange && PlayerPrefs.GetInt("Level") > 4)
            {
                currentColor = Color.Lerp(currentColor, GameObject.Find("ColorBump(Clone)").GetComponent<ColorBump>()._Color, lerpAmount);
                lerpAmount += Time.deltaTime;
            }
            if (lerpAmount > 1)
                isChange = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pass"))
            {
                Destroy(other.transform.gameObject);
            }
            if (other.CompareTag("ColorBump"))
            {
                lerpAmount = 0;
                isChange = true;
            }

            if (other.CompareTag("Cancel"))
            {
                GameOver();
            }

            if (other.CompareTag("Finish"))
            {
                NextLevel();
            }
        }

        void NextLevel()
        {
            StartCoroutine(CoNextLevel());
        }

        public IEnumerator CoNextLevel()
        {
            Camera.main.GetComponent<CameraFollow>().enabled = false;
            yield return new WaitForSeconds(1.5f);

            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            GameManager.instance.GenerateColor();
            move = false;
            posZ = -8f;
            GameManager.instance.GenerateLevel();
            Camera.main.GetComponent<CameraFollow>().enabled = true;
        }

        void GameOver()
        {
            StartCoroutine(CoGameOver());
        }

        public IEnumerator CoGameOver()
        {
            GameManager.instance.GenerateLevel();
            move = false;
            posZ = -8;
            yield break;
        }
        public static bool _Move { get => move; set { move = value; } }
        public static float _PosZ { get => posZ; set { posZ = value; } }
        public static Color _CurrentColor { get => currentColor; set { currentColor = value; } }
    }

}
