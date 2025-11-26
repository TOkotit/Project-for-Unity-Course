using UnityEngine;
using UnityEngine.UI;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;
    private void Awake()
    {
        if (optionsButton != null)
            optionsButton.onClick.AddListener(OnClickOptions);
        if (exitButton != null)
            exitButton.onClick.AddListener(OnClickExit);
    }

    private void OnClickOptions()
    {
        animator.SetTrigger("OptionsButtonClicked");
    }
    private void OnClickExit()
    {
        animator.SetTrigger("ExitButtonClicked");
    }
}
