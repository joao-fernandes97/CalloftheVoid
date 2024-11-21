
using UnityEngine;

public class EventRedirector : MonoBehaviour
{
    private EyeFollow _eyeFollow;
    [SerializeField]private EyeHandler _eyeHandler;
    private void Awake()
    {
        _eyeFollow = GetComponentInChildren<EyeFollow>(true);
        if (_eyeFollow != null) { Debug.Log("Event Redirector Linked"); }
        else { Debug.Log("Couldn't find script"); }
    }
    public void OpenStart() => _eyeFollow.EyeIsOpening();
    
    public void OpenStop() => _eyeFollow.EyeDoneOpening();

    


}