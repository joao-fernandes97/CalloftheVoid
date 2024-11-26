using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class StopAndGoPuzzle : MonoBehaviour
{
    [Header("Eyes STOP AND GO")]
    [SerializeField] private EyeHandler[] _eyes;
    [SerializeField] private Animator _doorAnimCtrl;

    [Header("Player Reference")]
    [SerializeField] private GameObject _player;
    private PlayerMovement _playerMov ;
    private CharacterController _playerCharacterCtrl;

    [Header("Puzzle Params")]
    [SerializeField] private Volume vol;
    [SerializeField] private Expulsion _expel;
    [SerializeField] private SwapTimer _swapTimer;
    [SerializeField] private float _maxSpeedTolerance = 1f;
    [SerializeField] private float _failureCountDownSpeed = 0.1f;
    [SerializeField] private float _failureCountDown = 10f;
    private float _failureCountDownMax;
    private float _playerVelocity;
    private bool _playerPlaying = false;
    private bool _playerFailed = false;
    private bool gameActive = true;
 
    
    //Conditions to check
    private bool _playerInside = false;

    //Flags
    private bool _gameStart = false;
    private bool resetGame = false;
    private bool _gameEndFlag = false;
    private bool _eyesOpenFlag = false;

    
    private void Start()
    {
        _failureCountDownMax = _failureCountDown;
        _playerMov = _player.GetComponent<PlayerMovement>();
        _playerCharacterCtrl = _player.GetComponent<CharacterController>();
        DisableEyes();
    }

    private void FixedUpdate()
    {
        //if player enters = trigger eyes open and then just keep it that way
        if (_playerInside)
            {

                //player 
                if (!_playerFailed)
                {
                    //guarantees resetGame is reset:
                    if (resetGame) resetGame = false;
                    //Start game
                    if (!_gameStart)
                    {
                        //init game variables
                        _failureCountDown = _failureCountDownMax;
                        _gameStart = true;
                        _gameEndFlag = false;
                        //Init timer vars
                        _swapTimer.ResetTimer();
                        _swapTimer.TimeStart();
                        _doorAnimCtrl.SetBool("Open", true);

                    }
                    //Run game code loop here:
                    StartAndStopGame();

                }
                else
                {

                    if (!_gameEndFlag)
                    {
                        PlayerFailed();
                        _gameStart = false;
                        _gameEndFlag = true;
                    }
                }
            }
            else
            {
                if (!resetGame)
                {
                    Reset();
                    resetGame = true;

                }//if he was inside then stop the game and reset emediatly
                 //trigger once
            }

    }
    
    
    private void Reset()
    {
        DisableEyes();
        //Stop timer        
        _eyesOpenFlag = false;        
        _playerFailed = false;
        _swapTimer.StopTimer();
    }

    private void PlayerFailed()
    {
        StartCoroutine(DoorReset(3));
        //Player failed kick out and set variables
        Reset();   
        _playerInside = false;     
        _expel.Expel(_player);

    }
    private IEnumerator DoorReset(float time)
    {
        _doorAnimCtrl.SetBool("Open", false);
        yield return new WaitForSeconds(time);
        _doorAnimCtrl.SetBool("Open", true);
    }
    //The  only ouput is "playerFailed"
    //add the timer thing
    private void StartAndStopGame()
    {
        _playerVelocity = _playerCharacterCtrl.velocity.magnitude; //track player speed

        if(_swapTimer.MyTurn)
        {
            vol.weight = 1;
            if (!_eyesOpenFlag)
            {
                EnableEyes();
                //Debug.Log("Eyes where open");
                _eyesOpenFlag = true;
            }
            //Debug.Log("My turn");
            //Tracking and counting behavior (game on)
            if (_failureCountDown <= 0.0f) _playerFailed = true;
            if (_playerVelocity >= _maxSpeedTolerance)
            {
                CountToFailure();
            }
            else //Slowly replenish countdown while not moving
            {
                if (_failureCountDownMax > _failureCountDown)
                {
                    ReplenishCountDown(.5f);
                    if (_failureCountDown > _failureCountDownMax)
                    {
                        _failureCountDown = _failureCountDownMax;
                    }

                }

            }
        }
        else
        {
            vol.weight = 0;
            //Debug.Log("His turn");
            if (_eyesOpenFlag)
            {
                DisableEyes();
                //Debug.Log("Eyes where closed");
                _eyesOpenFlag = false;
                _failureCountDown = _failureCountDownMax;

            }
            
            //reset vars
            //no tracking
            
           
            //reference to the pp effect to turn off
            //open door
        }


    }
    
   
    private void CountToFailure()
    {
         //Debug.Log($"CountToFailure {_failureCountDown}");
        if (_failureCountDown > 0.0f)
            _failureCountDown -= _failureCountDownSpeed * Time.fixedDeltaTime;

    }
    private void ReplenishCountDown(float rate = 1f)
    {

        _failureCountDown += _failureCountDownSpeed * rate * Time.fixedDeltaTime;
    }

    //End game
    [Button]
    public void GameToggle() => gameActive = !gameActive;
    
    //Check if player inside trigger
    private void OnTriggerEnter(Collider other)
    {
        _playerMov = other.gameObject.GetComponent<PlayerMovement>();
        if (_playerMov != null)
        {
            _playerInside = true;
            //Debug.Log("Player entered Eye perimeter");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _playerMov = other.gameObject.GetComponent<PlayerMovement>();
        if (_playerMov != null)
        {
            _playerInside = false;
            //Debug.Log("Player exited Eye perimeter");
        }
    }

    //Enable and disable eyes:
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
