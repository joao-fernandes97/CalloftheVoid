using UnityEngine;

public class PanelButtons : MonoBehaviour, IInteractable
{
    [SerializeField] private string animationToTrigger;
    private Animator panelAnimator;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panelAnimator = GetComponentInParent<Animator>();
    }
    
    public void Interact()
    {
        if(panelAnimator != null)
        {
            panelAnimator.SetTrigger(animationToTrigger);
            Debug.Log("Triggered animation " + animationToTrigger);
        }
        else
        {
            Debug.Log("No animator found");
        }
    }
}
