
using UnityEngine;

public class SwapTimer : MonoBehaviour
{
    
    [Header("Timer Parameters")]
    [SerializeField] private float _onTime = 3f;
    [SerializeField] private float _offTime = 2f;
    [SerializeField] private bool _useRandom = false;
    [SerializeField] private float randomOffset = 0.0f;
    private float _timer;
    private bool _timerON = false;
    private bool _On = false;
    public bool _myTurn = true;
    private void Awake()
    {
        _timer = _onTime;        
        _timerON = false;
    }
    
    
    private void ResetTimer()
    {
        _timerON = true;
        if(_myTurn) 
        {
            if (_useRandom) _timer = _onTime + Random.Range(-randomOffset, randomOffset);
            else _timer = _onTime;
        }
        else
        {
            if (_useRandom) _timer = _offTime + Random.Range(-randomOffset, randomOffset);
            else _timer = _offTime;

        }
        
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
        }
        
    }
}
