using System;
using UnityEngine;
using UnityEngine.UIElements;

public class StartAndStop : MonoBehaviour
{
    //reference to the eyes
    //all we will do is implicitly turn them on and off at random intervals
    //during those intervals the player isint being tracked
    //reference to the player
  

    [Header("Eyes STOP AND GO")]
    [SerializeField] private EyeHandler[] _eyes;
    [Header ("Puzzle Params")]
    //At this speed, the fail timer starts counting down
    [SerializeField] private float _maxSpeedTolerance = 1.5f;
    [SerializeField] private float _failureCountDown = 5;
    private float _failureCountDownMax;
    [SerializeField] private float _failureCountDownSpeed = 0.1f;
    
    private PlayerMovement _playerMov = null;
    private CharacterController _playerCharacterCtrl = null;
    private float _playerVelocity;
    
    GameState eyeGameState;
    
    // Game play logic:
    private enum GameState
    {
        NotStarted,
        StartedAndGoing,
        FailedAndStoped
    }
    private bool _playerInside = false;
    private bool _playerPlaying = false;
    private bool _playerFailed = false;

    
   

    private void Start()
    {
       eyeGameState = GameState.NotStarted;
        _failureCountDownMax = _failureCountDown;
       
        if (_eyes.Length == 0)
            Debug.LogWarning("No eyes inserted into \"TriggerEyeArea\" Script Array");
    }


    //Use the SetGameState to check and change game states
    //Fixed Update is then mostly used to run game logic set in each game state

    private void FixedUpdate() 
    {
        SetGameState();

        //Stop and go logic
        if (eyeGameState == GameState.NotStarted)
        {
            DisableEyes();
            Debug.Log("GameState set to : NotStarted");

        }
        if (eyeGameState == GameState.StartedAndGoing)
        {
            StartAndStopGame();
            Debug.Log("GameState set to : StartedAndGoing");
        }
        if (eyeGameState == GameState.FailedAndStoped)
        {
            DisableEyes();
            Debug.Log("GameState set to : FailedAndStoped");
        }
        

    }

    private void StartAndStopGame()
    {
        //Track speed:
        _playerVelocity = _playerCharacterCtrl.velocity.magnitude;
        Debug.Log($"Player speed : {_playerVelocity}");

        //Just track velocity and if he's going too fast or moving while eyes are
        //open then decrease value
        if (_failureCountDown > 0.0f) _playerFailed = true;
        
        //reset failureCountdown when :
        // player fails then exits
        if (!_playerFailed)
        {
            if (_playerVelocity >= _maxSpeedTolerance)
            {
                CountToFailure();
                Debug.Log($"Counting down. Time left = {_failureCountDown}");
            }
            else
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
        else
        {
            Debug.Log("Player failed");
            //Run Failure sequence
            //  Push player out
            // Door closes
            // playerFailed = false;
            //
            //Then at the end change the GameState to failure or handle
            //the kicking out and reset behaviour in the gamestate itself
            //And only change states here.


        }
        
    }
    
    private void CountToFailure()
    {
        if (_failureCountDown > 0.0f)
        {
            _failureCountDown = -_failureCountDownSpeed * Time.fixedDeltaTime;
            
        }
        
    
    }
    private void ReplenishCountDown(float rate = 1f)
    {
        _failureCountDown = +_failureCountDownSpeed * rate * Time.fixedDeltaTime;
    }
    private void SetGameState()
    {
        if (_playerInside && !_playerPlaying) _playerPlaying = true;
        else if (_playerInside && _playerPlaying)
        {
            eyeGameState = GameState.StartedAndGoing;
            
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        _playerMov = other.gameObject.GetComponent<PlayerMovement>();
        if (_playerMov != null)
        {
            _playerCharacterCtrl = other.gameObject.GetComponent<CharacterController>();
            //EnableEyes();
            _playerInside = true;
            Debug.Log("Player entered Eye perimeter");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_playerMov != null)
        {
            //Dont set this to null since you're still tracking the player's speed.
            _playerCharacterCtrl = null;
            _playerMov = null;
            _playerVelocity = 0f;
            _playerInside = false;
            //DisableEyes();
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

}
