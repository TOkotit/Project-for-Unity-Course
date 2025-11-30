using UnityEngine;

namespace Scripts.GamePlar.Root
{
    public class GameplayEnrtyPoint :  MonoBehaviour
    {
        [SerializeField] private GameObject _sceneRootBinder;

        public void Run()
        {
            Debug.Log("GameplayEnrtyPoint");
        }
    }
}