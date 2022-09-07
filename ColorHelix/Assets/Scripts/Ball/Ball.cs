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
        bool isChange, gameOver;
        [SerializeField] SpriteRenderer splash;


        void Start() => move = false;
        void Update()
        {
            // check pressing the scrren
            if (Touch.isPressing && !gameOver)
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
                GameManager.instance._IsPasses = true;
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
            if(other.CompareTag("Star"))
            {
                GameManager.instance._isStar = true;
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
            Camera.main.GetComponent<CameraFollow>().SetFlash();
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
            gameOver = true;
            Camera.main.GetComponent<CameraFollow>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
            splash.transform.localPosition = new Vector3(0, 1, 0.2f);

            splash.enabled = true;
            splash.color = currentColor + new Color(0, 0, 0, 255);
            move = false;
            Helix.instance.isMoveable = false;
            yield return new WaitForSeconds(1.5f);
            Camera.main.GetComponent<CameraFollow>().SetFlash();
            GameManager.instance.GenerateLevel();
            GameManager.instance._Score = 0;
            splash.enabled = false;
            posZ = -8f;
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<SphereCollider>().enabled = true;
            gameOver = false;
            Helix.instance.isMoveable = true;
            Camera.main.GetComponent<CameraFollow>().enabled = true;
        }
        public static bool _Move { get => move; set { move = value; } }
        public static float _PosZ { get => posZ; set { posZ = value; } }
        public static Color _CurrentColor { get => currentColor; set { currentColor = value; } }
    }

}
