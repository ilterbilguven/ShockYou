using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public PlayerController PlayerController;
        public LevelController LevelController;
        public SkillController SkillController;
        void Awake()
        {
            if (_instance != null && _instance != this)
                Destroy(gameObject);
            else
                _instance = this;

            //Extra start functions/statements.
            DontDestroyOnLoad(this);
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
