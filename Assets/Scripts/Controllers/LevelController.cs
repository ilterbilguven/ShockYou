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

        private void OnDisable()
        {
            StopCoroutine(LevelTimer());
        }

        IEnumerator LevelTimer()
        {
            LevelTimeText.color = new Color(1, 1, 1, 0.25f);

            int localLevelTime = LevelTime;

            while(localLevelTime >= 10)
            {
                SetLevelTimerText(--localLevelTime);
                yield return new WaitForSeconds(1f);
            }

            Debug.Log("Last 10 seconds");
            LevelTimeText.color = new Color(1,0,0,0.25f);

            while (localLevelTime > 0)
            {
                SetLevelTimerText(--localLevelTime);
                yield return new WaitForSeconds(1f);
            }

            TimeEnded();
        }

        private void SetLevelTimerText(int timeLeftAsSeconds)
        {
            LevelTimeText.text = timeLeftAsSeconds.ToString();
        }

        private void TimeEnded()
        {
            GameController.Instance.PanelController.OpenPanel(PanelName.VictoryPanel);
        }
    }
}
