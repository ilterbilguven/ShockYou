using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public List<GameObject> Players;
        [HideInInspector]
        public LevelController LevelController;
        public SpellController SpellController;
        public PanelController PanelController;

        public Action LevelStarted;
        public Action LevelEnded;

        [SerializeField]
        public Dictionary<PlayerTypes, Sprite> playerPics = new Dictionary<PlayerTypes, Sprite>();

        void Awake()
        {
            if (_instance != null && _instance != this)
                Destroy(gameObject);
            else
                _instance = this;

            //Extra start functions/statements.
        }

        private void Start()
        {
            PanelController.OpenPanel(PanelName.MainMenuPanel);
        }

        #region Singleton
        private static GameController _instance;
        public static GameController Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = FindObjectOfType<GameController>();
                if (_instance != null)
                    return _instance;

                var obj = new GameObject("ObjectPool");
                _instance = obj.AddComponent<GameController>();
                return _instance;
            }
        }
        #endregion
    }
}
