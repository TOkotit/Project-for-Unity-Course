using UnityEngine;
using UnityEngine.InputSystem;

public class InputSaveManager : MonoBehaviour
{
    public InputActionAsset actionAsset; 

    private void OnEnable()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
        {
            actionAsset.LoadBindingOverridesFromJson(rebinds);
        }
    }

    public void SaveBindingOverrides()
    {
        var rebinds = actionAsset.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
        PlayerPrefs.Save();
    }

    public void ResetAllBindings()
    {
        foreach (var map in actionAsset.actionMaps)
            map.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey("rebinds");
    }
}
