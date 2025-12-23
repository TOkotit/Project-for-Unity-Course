using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    [SerializeField] private BuffsSO defaultBuffsDatabase; 
    
    private BuffSystemModel buffSystemModel;

    private void Start()
    {
        // Применяем баффы при старте
        buffSystemModel.ApplyAllBuffs();
    }
    
    private void OnApplicationQuit()
    {
        buffSystemModel.SaveBuffs(); 
    }
}
