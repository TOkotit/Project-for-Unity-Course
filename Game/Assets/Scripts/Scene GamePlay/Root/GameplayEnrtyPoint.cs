using UnityEngine;

namespace Scripts.GamePlar.Root
{
    public class GameplayEnrtyPoint :  MonoBehaviour
    {
        
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