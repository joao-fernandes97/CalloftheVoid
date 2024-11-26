using System;
using System.Linq;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CodeChecker : MonoBehaviour
{

    [SerializeField]
    private string availableChars;
    [SerializeField]
    private TMP_Text CodePrompt;
    private int currentLock = 1;
    private char[] firstCode = new char[3] {'A','I','Q'};
    private char[] secondCode = new char[3] {'X','A','D'};
    private char[] thirdCode = new char[3] {'Q','D','I'};
    private char[] currentCode;
    private GameObject currentButton;
    private CinemachineCamera UICamera;

    private char[] enteredCode = new char[3] {'A','A','A'};
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCode = firstCode;
        CodePrompt.text = "Galaxy Identifier";
    }

    void Update()
    {
        //Resets interaction camera priority
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            UICamera = (CinemachineCamera) CinemachineBrain.GetActiveBrain(0).ActiveVirtualCamera;
            if (UICamera.tag != "MainCamera")
            {
                UICamera.Priority=1;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    
    public void CycleInput()
    {
        currentButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        TMP_Text currentText = currentButton.GetComponentInChildren<TMP_Text>();
        currentText.text = GetNextChar(currentText.text);

        UpdateCode(Int32.Parse(currentButton.name), currentText.text);

        ValidateCode();
    }

    private string GetNextChar(string currentChar)
    {
        char[] charArray = availableChars.ToCharArray();

        for (int i = 0; i<charArray.Length; i++)
        {
            if (currentChar[0]==availableChars[i])
            {
                if(i+1==charArray.Length)
                {
                    return availableChars[0].ToString();
                }
                return availableChars[i+1].ToString();
            }
        }

        return "a";
    }

    private void UpdateCode(int position, string value)
    {
        enteredCode[position] = value[0];
    }

    private void ValidateCode()
    {
        Debug.Log($"{enteredCode[0]},{enteredCode[1]},{enteredCode[2]}");
        Debug.Log($"{currentCode[0]},{currentCode[1]},{currentCode[2]}");
        if(enteredCode.SequenceEqual(currentCode))
        {
            Debug.Log("Correct");
            NextLock();
        }
    }

    private void NextLock()
    {
        if(currentLock==1)
        {
            currentLock++;
            currentCode = secondCode;
            CodePrompt.text = "Star Identifier";
        }else if(currentLock==2)
        {
            currentLock++;
            currentCode = thirdCode;
            CodePrompt.text = "Planet Identifier";
        }else
        {
            CodePrompt.text = "Coordinates Set";
            ResetCamera();
            Debug.Log("Congrats you're done");
        }
    }

    private void ResetCamera()
    {
        UICamera = (CinemachineCamera) CinemachineBrain.GetActiveBrain(0).ActiveVirtualCamera;
        Debug.Log(UICamera.name);
        UICamera.Priority = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
