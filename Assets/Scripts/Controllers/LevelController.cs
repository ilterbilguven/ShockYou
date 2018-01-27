using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class LevelController : MonoBehaviour {

        public List<Transform> SpellPoints;
        public List<Transform> SpawnPoints;

        void OnEnable()
        {
            // Set some things here
            GameController.Instance.LevelController = this;
            GameController.Instance.LevelStarted.Invoke();
        }
        
    }
}
