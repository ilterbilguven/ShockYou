using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class LevelController : MonoBehaviour {

        public List<Transform> SpellPoints;
        public List<Transform> SpawnPoints;
        public Text LevelTimeText;
        public int LevelTime;

        void OnEnable()
        {
            // Set some things here
            GameController.Instance.LevelController = this;
            GameController.Instance.LevelStarted.Invoke();
            StartCoroutine(LevelTimer());
        }

        IEnumerator LevelTimer()
        {
            LevelTimeText.color = Color.white;
            int localLevelTime = LevelTime;

            while(localLevelTime >= 10)
            {
                localLevelTime--;
                yield return new WaitForSeconds(1f);
            }

            Debug.Log("Last 10 seconds");
            LevelTimeText.color = Color.red;

            while (localLevelTime > 0)
            {
                localLevelTime--;
                yield return new WaitForSeconds(1f);
            }
        }

        private void SetLevelTimerText(int timeLeftAsSeconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(timeLeftAsSeconds);
            LevelTimeText.text = TimeSpan.FromMinutes(timeLeftAsSeconds) + ":" + timeLeftAsSeconds % 60;
        }
    }
}
