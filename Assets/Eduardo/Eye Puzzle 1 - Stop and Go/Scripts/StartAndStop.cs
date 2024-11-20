using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class StartAndStop : MonoBehaviour
{
    [Header("Eyes STOP AND GO")]
    [SerializeField] private EyeHandler[] _eyes;
    [Header ("Puzzle Params")]
    //At this speed, the fail timer starts counting down
    [SerializeField] private float _maxSpeedTolerance = 1f;
    [SerializeField] private float _failureCountDown = 10f;
    private float _failureCountDownMax;
    [SerializeField] private float _failureCountDownSpeed = 0.1f;
    private Expulsion expel;
    private PlayerMovement _playerMov = null;
    private CharacterController _playerCharacterCtrl = null;
    private float _playerVelocity;

    [SerializeField] private StateChecker sc_;
    GameState eyeGameState;
    
    private enum GameState
    {
        NotStarted,
        StartedAndGoing,
        FailedAndStoped
    }
    private bool _playerInside = false;
    private bool _playerPlaying = false;
    private bool _playerFailed = false;
    private bool NotStartedFlagDirty = false;
    private bool StartedAndGoingDirty = false;
    private bool FailedAndStopedDirty = false;

    private void Start()
    {
       eyeGameState = GameState.NotStarted;
        _failureCountDownMax = _failureCountDown;
        expel = GetComponent<Expulsion>();
        if (_eyes.Length == 0)
            Debug.LogWarning("No eyes inserted into \"TriggerEyeArea\" Script Array");
    }
    private void FixedUpdate() 
    {
        SetGameState();
        
        if (eyeGameState == GameState.NotStarted)
        {
            if (!NotStartedFlagDirty)
            {
                sc_.SwapColour(Color.blue);
                //Move this logic to the State switcher
                //Dirty flag? Well this only happens once
                DisableEyes();
                Debug.Log("GameState set to : NotStarted");
                NotStartedFlagDirty = true;
            }
        }
        if (eyeGameState == GameState.StartedAndGoing)
        {
            if (!StartedAndGoingDirty)
            {
                sc_.SwapColour(Color.green);
                 Debug.Log("GameState set to : StartedAndGoing");
                StartedAndGoingDirty = true;
            }
            else 
            {
                StartAndStopGame();
            } 
        }
        if (eyeGameState == GameState.FailedAndStoped)
        {
            if (!FailedAndStopedDirty)
            {
                sc_.SwapColour(Color.red);
                Failure();
                //Dirty flag? Well this only happens once
                DisableEyes();
                
                Debug.Log("GameState set to : FailedAndStoped");
                FailedAndStopedDirty = true;
            }
        }

    }
    

    private void StartAndStopGame()
    {
        //Track speed:
        _playerVelocity = _playerCharacterCtrl.velocity.magnitude;
        
        //Debug.Log($"Player speed : {_playerVelocity}");
        if (_failureCountDown <= 0.0f) _playerFailed = true;
        
        if (_playerFailed == false)
        {
            if (_playerVelocity >= _maxSpeedTolerance)
            {
                CountToFailure();
                //Debug.Log($"Counting down. Time left = {_failureCountDown}");
            }
            else //Slowly replenish countdown while not moving
            {
                if (_failureCountDownMax > _failureCountDown)
                {
                    ReplenishCountDown(.5f);
                    if(_failureCountDown >_failureCountDownMax)
                    {
                        _failureCountDown = _failureCountDownMax;
                    } 

                }
                
            }
        }
        
    }
    


    private void Failure()
    {
        //Debug.Log("You will be expelled in 2 seconds");
        expel.Expel(_playerMov.gameObject);
        //Reset 
        _failureCountDown = _failureCountDownMax;
        _playerFailed = false;
        _playerInside = false;
    }

    private void CountToFailure()
    {
        if (_failureCountDown > 0.0f) _failureCountDown -=_failureCountDownSpeed * Time.fixedDeltaTime;   
    }
    private void ReplenishCountDown(float rate = 1f)
    {

        _failureCountDown +=_failureCountDownSpeed * rate * Time.fixedDeltaTime;
    }
    private void SetGameState()
    {

        if (!_playerInside && _playerPlaying) _playerPlaying = false; //turn off if not inside
        if (_playerInside && !_playerPlaying) _playerPlaying = true; //turn on if inside

        if (!_playerPlaying)
        {
            //player failed + not inside 
            //reset vars
            eyeGameState = GameState.NotStarted;
            FailedAndStopedDirty = false;
        }
        if (_playerPlaying && !_playerFailed)
        {
            //Player inside + player Playing + player not failed
            eyeGameState = GameState.StartedAndGoing;
            NotStartedFlagDirty = false;
        }
        if (_playerPlaying && _playerFailed)
        {
            //Player inside + player failed
            //Player entered. But then left. He didn't loose but we set the state to failure/stop
            eyeGameState = GameState.FailedAndStoped;
            StartedAndGoingDirty = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerMov = other.gameObject.GetComponent<PlayerMovement>();
        if (_playerMov != null)
        {
            _playerCharacterCtrl = other.gameObject.GetComponent<CharacterController>();
            EnableEyes();
            _playerInside = true;
            Debug.Log("Player entered Eye perimeter");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //if player entered once and activated the puzzle
        if (_playerMov != null)
        {
            DisableEyes();
            Debug.Log("Player exited Eye perimeter");
        }
    }
    private void EnableEyes()
    {
        if (_eyes.Length != 0)
            foreach (EyeHandler eye in _eyes)
                eye.EnableEye();
    }
    private void DisableEyes()
    {
        if (_eyes.Length != 0)
            foreach (EyeHandler eye in _eyes)
                eye.DisableEye();

    }
    //If player gets the thing
    private void PlayerWins()
    {
        _playerCharacterCtrl = null;
         _playerMov = null;
       
    }

}
