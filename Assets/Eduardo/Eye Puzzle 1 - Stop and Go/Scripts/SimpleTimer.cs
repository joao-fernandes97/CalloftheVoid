using UnityEngine;

public class SimpleTimer : MonoBehaviour, IInteractable
{
    private Material mat;
    private float _timer;
    [SerializeField] private float _maxTime = 10f;
    private bool _timerON = false;
    private bool _On = false;
    private bool _myTurn = true;
    private void Awake()
    {
        _timer = _maxTime;
        mat = GetComponent<Renderer>().material;
        mat.color = Color.black;
        _timerON = false;
    }

    public void SwapColour(Color colour) => mat.color = colour;

    public void Interact()
    {
        TimeStart();
    }
    private void ResetTimer()
    {
        _timerON = true;
        _timer = _maxTime;
    }
    private void TimeStart()
    {
        _On = true;
        _timerON = true;
    }
    private bool RunTimer()
    {
        if (_timer > 0)
        {
            _timer -= Time.fixedDeltaTime;
            return true;
        }
        else return false;
    }
    private void Swap() => _myTurn = !_myTurn;
    public void SwapTurns()
    {
       if (_timerON)
        {
           _timerON = RunTimer();

        }
        else
        {
            Swap();
            ResetTimer();     

        } 

    }
    private void FixedUpdate() 
    {
        if (_On)
        {
            SwapTurns();
            if (_myTurn) mat.color = Color.blue;
            else mat.color = Color.red;
        }
        
    }
}

