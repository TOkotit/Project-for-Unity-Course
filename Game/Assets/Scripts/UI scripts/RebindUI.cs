using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; 
using UnityEngine.UI;

public class RebindUI : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private InputActionReference actionReference;

    [Header("Для Vector/Composite (Пустое для обычных кнопок)")]
    [Tooltip("Имя части композита: 'left', 'right', 'up', 'down', 'negative', 'positive'")]
    [SerializeField] private string compositePartName; 

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI bindingText;
    [SerializeField] private Button rebindButton;
    [SerializeField] private GameObject waitingForInputObject;

    private InputActionRebindingExtensions.RebindingOperation _rebindOperation;
    private int _bindingIndex; 

    private void Start()
    {
        _bindingIndex = GetBindingIndex();

        rebindButton.onClick.AddListener(StartRebinding);
        UpdateBindingDisplay();
    }
    private int GetBindingIndex()
    {
        var action = actionReference.action;
        
        if (string.IsNullOrEmpty(compositePartName))
            return 0;

        for (var i = 0; i < action.bindings.Count; i++)
        {
            if (action.bindings[i].isPartOfComposite && 
                string.Compare(action.bindings[i].name, compositePartName, true) == 0)
            {
                return i;
            }
        }

        Debug.LogError($"Привязка с именем '{compositePartName}' не найдена в действии '{action.name}'");
        return -1;
    }

    private void UpdateBindingDisplay()
    {
        if (_bindingIndex == -1) return;

        var displayString = actionReference.action.GetBindingDisplayString(_bindingIndex);
        bindingText.text = displayString;
    }

    public void StartRebinding()
    {
        if (_bindingIndex == -1) return;

        rebindButton.interactable = false;
        waitingForInputObject.SetActive(true);

        actionReference.action.Disable();

        _rebindOperation = actionReference.action.PerformInteractiveRebinding(_bindingIndex)
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(op => RebindCompleted())
            .OnCancel(op => RebindCompleted())
            .Start();
    }

    private void RebindCompleted()
    {
        _rebindOperation.Dispose();
        _rebindOperation = null;
        
        actionReference.action.Enable();
        waitingForInputObject.SetActive(false);
        rebindButton.interactable = true;
        
        UpdateBindingDisplay();
    }
}
