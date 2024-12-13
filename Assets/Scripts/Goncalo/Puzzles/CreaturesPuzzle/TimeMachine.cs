using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeMachine : MonoBehaviour, IInteractable
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //remove this later
        ActivateCreatures();
    }

    public void ActivateCreatures()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void Interact()
    {
        if (transform.childCount == 0)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            audioSource.Play();
        }
    }
}
