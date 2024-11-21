using UnityEngine;

public class StopAndGoPuzzle : MonoBehaviour
{
    [Header("Eyes STOP AND GO")]
    [SerializeField] private EyeHandler[] _eyes;

    [Header("Player Reference")]
    [SerializeField] private GameObject _player;
    private PlayerMovement _playerMov ;
    private CharacterController _playerCharacterCtrl;

    [Header("Puzzle Params")]
    [SerializeField] private SwapTimer _swapTimer;
    [SerializeField] private float _maxSpeedTolerance = 1f;
    [SerializeField] private float _failureCountDownSpeed = 0.1f;
    [SerializeField] private float _failureCountDown = 10f;
    private float _failureCountDownMax;
    private float _playerVelocity;
    private bool _playerPlaying = false;
    private bool _playerFailed = false;

    
    //Conditions to check
    private bool _playerInside = false;

    //Flags
    private bool _gameStart = false;
    private bool _eyesCloseFlag = false;

    
    private void Awake()
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
                //Start game
                if (!_gameStart)
                {
                    //init game variables
                    _failureCountDown = _failureCountDownMax;
                    _gameStart = true;
                    //Init timer vars
                    _swapTimer.ResetTimer();
                    _swapTimer.TimeStart();

                }
                //Run game code loop here:
                StartAndStopGame();

            }
            else
            {
                DisableEyes();

                
                
                //Failure scenario
                //player is kicked out sequence

                //then reset values for reentry
            }


        }
        else
        {
            //if player not inside
            //do nothing
        }

    }
    //The  only ouput is "playerFailed"
    //add the timer thing
    private void StartAndStopGame()
    {
        _playerVelocity = _playerCharacterCtrl.velocity.magnitude; //track player speed

        if(_swapTimer.MyTurn)
        {
            EnableEyes();
            
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
            DisableEyes();
            //reset vars
            //no tracking
            _failureCountDown = _failureCountDownMax;
           
            //reference to the pp effect to turn off
            //open door
        }


    }
   
    private void CountToFailure()
    {
        if (_failureCountDown > 0.0f)
            _failureCountDown -= _failureCountDownSpeed * Time.fixedDeltaTime;

    }
    private void ReplenishCountDown(float rate = 1f)
    {

        _failureCountDown += _failureCountDownSpeed * rate * Time.fixedDeltaTime;
    }

    //Check if player inside trigger
    private void OnTriggerEnter(Collider other)
    {
        _playerMov = other.gameObject.GetComponent<PlayerMovement>();
        if (_playerMov != null)
        {
            _playerInside = true;
            Debug.Log("Player entered Eye perimeter");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _playerMov = other.gameObject.GetComponent<PlayerMovement>();
        if (_playerMov != null)
        {
            Debug.Log("Player exited Eye perimeter");
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
