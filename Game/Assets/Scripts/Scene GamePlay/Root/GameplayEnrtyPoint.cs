using UnityEngine;

namespace Scripts.GamePlar.Root
{
    public class GameplayEnrtyPoint :  MonoBehaviour
    {
        [SerializeField] private GameObject _sceneRootBinder;
        
        private void Awake()
        {
            if (Game.Instance == null)
            {
                Game.Initialize();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void Run()
        {
            Debug.Log("GameplayEnrtyPoint");
        }
    }
}