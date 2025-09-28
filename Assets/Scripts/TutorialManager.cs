using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    private bool _wPressed = false;
    private bool _aPressed = false;
    private bool _sPressed = false;
    private bool _dPressed = false;

    private bool _movementComplete = false;
    //private bool _attackComplete = false;
    [SerializeField] EnemyShooting DummyShooting;
    [SerializeField] Enemy DummyControl;
    [SerializeField] private TextMeshProUGUI menuText;
    public enum TutorialState { Movement, Attack, Parry, Combat, Done}
    public TutorialState currentState;
    private void Start()
    {
        currentState = TutorialState.Movement;
        updateMovementText();
        menuText.enabled = false;
        DummyShooting.enabled = false;
    }


    private void Update()
    {
        switch (currentState)
        {
            case TutorialState.Movement:
                MovementInputCheck();
                break;
            case TutorialState.Attack:
                AttackInputCheck();
                break;
            case TutorialState.Parry:
                ParryInputCheck();
                break;
            case TutorialState.Combat:
                Combat();
                break;
            case TutorialState.Done:
                DoneTutorial();
                break;
            default:
                break;
        }


        if (!_movementComplete)
        {
            MovementInputCheck();
        }
    }
    void MovementInputCheck()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _wPressed = true;
            updateMovementText();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _aPressed = true;
            updateMovementText();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _sPressed = true;
            updateMovementText();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _dPressed = true;
            updateMovementText();
        }

        if (_wPressed & _aPressed & _sPressed & _dPressed)
        {
            _movementComplete = true;
            currentState = TutorialState.Attack;
            tutorialText.text = "Usa <color=white>Click Izquierdo</color> para atacar";
        }
    }
    void AttackInputCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //_attackComplete = true;
            currentState = TutorialState.Parry;
            tutorialText.text = "Ahora usa <color=white>Click Derecho</color> para hacer parry";
        }
    }
    void ParryInputCheck()
    {
        if (Input.GetMouseButtonDown(1))
        {
            tutorialText.text = "¡Bien!, ¡Solo los proyectiles verdes se pueden desviar!";
            Invoke("ActivateDummy", 3f);
        }
    }

    void Combat()
    {
        tutorialText.text = "¡Golpea al dummy!";
        if (DummyControl.Health <= 0)
        {
            tutorialText.text = "Tutorial Completado";
            menuText.enabled = true;
            
            Invoke("DoneTutorial", 5f);
        }
    }
    void DoneTutorial()
    {
        currentState = TutorialState.Done;
        tutorialText.gameObject.SetActive(false);
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.ChangeScene("CascaNuecesBoss");
        }
    }
    void ActivateDummy()
    {
        DummyShooting.enabled = true;
        currentState = TutorialState.Combat;
    }

    
    void updateMovementText()
    {
        //Changes the color "white" to Green depending on the boolean value
        string wColor = _wPressed ? "green" : "white";
        string aColor = _aPressed ? "green" : "white";
        string sColor = _sPressed ? "green" : "white";
        string dColor = _dPressed ? "green" : "white";

        tutorialText.text = $"Usa <color={wColor}>W</color><color={aColor}>A</color><color={sColor}>S</color><color={dColor}>D</color> para moverte";
    }


}
