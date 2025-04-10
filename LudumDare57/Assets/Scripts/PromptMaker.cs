
using TMPro;
using UnityEngine;

public class PromptMaker : MonoBehaviour
{
    public UILever uiLever;
    public DialRandomize dial;
    public WarpButton warpButton;
    public DropdownPuzzle dropdown;
    public MultiScrollbarController sliders;
    public buttonMatrixController buttonMatrix;
    public GameObject buttonMatrixPrompt;
    public PanicButton panicButton;
    private bool activePrompt = false;
    private bool isUiLeverPrompt = false;
    private bool isDialPrompt = false;
    private bool isDropdownPrompt = false;
    private bool isSliderPrompt = false;
    private bool isMatrixPrompt = false;
    private int lastRandom = -1;
    private bool isNotPanicing = true;
    private bool panicingActivated = false;
    private float panicTimer;
    [SerializeField] public TextMeshProUGUI instructionsText;
    private string promptText;
    public bool localStarting = true;
    public bool isStarting = true;
    public float startTimer = 5f;
    private AudioSource audioSource;
    public AudioClip panicSound;
    public AudioClip panicResolved;

    private void Start()
    {
        if (localStarting)
        {
            ShowInstructions("Stay alive and do the prompts in order to activate the WARP");
        }
        panicTimer = Random.Range(10, 30);
        audioSource = GetComponent<AudioSource>();
        //buttonMatrixPrompt.SetActive(false);
    }

    private void Update()
    {
        if (localStarting)
        {
            GlobalVariables.Timer(ref isStarting, ref startTimer);
            GlobalVariables.Timer(ref isNotPanicing, ref panicTimer);
            if (!isStarting) {
                localStarting = false;
            }
            return;
        }
        if (!activePrompt)
        {
            MakePrompt();
        }
        if (!panicingActivated)
        {
            if (isUiLeverPrompt)
            {
                if (uiLever.CheckWin())
                {
                    isUiLeverPrompt = false;
                    activePrompt = false;
                    warpButton.current++;
                }
            }
            if (isDialPrompt)
            {
                if (dial.CheckGoalReached())
                {
                    isDialPrompt = false;
                    activePrompt = false;
                    warpButton.current++;
                }
            }
            if (isDropdownPrompt)
            {
                if (dropdown.CheckWin())
                {
                    isDropdownPrompt = false;
                    activePrompt = false;
                    warpButton.current++;
                }
            }
            if (isSliderPrompt)
            {
                if (sliders.CheckWinCondition())
                {
                    isSliderPrompt = false;
                    activePrompt = false;
                    warpButton.current++;
                }
            }
            if (isMatrixPrompt)
            {
                if (buttonMatrix.CheckForSuccess())
                {
                    instructionsText.gameObject.SetActive(true);
                    buttonMatrixPrompt.SetActive(false);
                    isMatrixPrompt = false;
                    activePrompt = false;
                    warpButton.current++;
                }
            }
        }
        GlobalVariables.Timer(ref isNotPanicing, ref panicTimer);

        if (!isNotPanicing)
        {
            PanicTime();
            isNotPanicing = true;
            panicTimer = Random.Range(15, 30);
        }
        Debug.Log(panicingActivated);
        if (panicingActivated)
        {
            Debug.Log(panicButton.PanicWin());
            if (panicButton.PanicWin())
            {
                if (isMatrixPrompt)
                {
                    instructionsText.gameObject.SetActive(false);
                    buttonMatrixPrompt.SetActive(true);
                } else
                {
                    instructionsText.gameObject.SetActive(true);
                    ShowInstructions(promptText);
                }
                audioSource.Stop();
                audioSource.PlayOneShot(panicResolved);
                panicingActivated = false;
                warpButton.current++;
            }
            
        }


    }

    void MakePrompt()
    {
        if (warpButton.current == warpButton.maximum)
        {
            promptText = "Turn key and activate WARP";
            ShowInstructions(promptText);
        }
        int random;
        do
        {
            random = Random.Range(0, 5);
        } while (random == lastRandom);
        lastRandom = random;

        if (random == 0 && warpButton.current < warpButton.maximum)
        {
            uiLever.SetGoalAngle();
            promptText = "Flip the lever";
            ShowInstructions(promptText);
            isUiLeverPrompt = true;
        }

        if (random == 1 && warpButton.current < warpButton.maximum)
        {
            string dialNumber = dial.SetRandomGoal();
            promptText = $"Set the dial to {dialNumber}";
            ShowInstructions(promptText);
            isDialPrompt = true;
        }

        if (random == 2 && warpButton.current < warpButton.maximum)
        {
            string winOption = dropdown.GenerateRandomWinCondition();
            promptText = $"Change the Sub Systems to {winOption}";
            ShowInstructions(promptText);
            isDropdownPrompt = true;
        }

        if (random == 3 && warpButton.current < warpButton.maximum)
        {
            string winNumber = sliders.GenerateTargetSequence();
            promptText = $"Move Sliders to {winNumber}";
            ShowInstructions(promptText);
            isSliderPrompt = true;
        }
        
        if (random == 4 && warpButton.current < warpButton.maximum)
        {
            instructionsText.gameObject.SetActive(false);
            buttonMatrixPrompt.SetActive(true);
            buttonMatrix.GenerateTargetPattern();
            isMatrixPrompt = true;
        }

        activePrompt = true;
    }

    void PanicTime()
    {
        if (isMatrixPrompt)
        {
            buttonMatrixPrompt.SetActive(false);
            instructionsText.gameObject.SetActive(true);
        }
        if (warpButton.current == warpButton.maximum)
        {
            return;
        }
        panicingActivated = true;
        panicButton.SetPanicState();
        string panicPrompt = "PRESS THE PANIC BUTTON";
        ShowInstructions(panicPrompt);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(panicSound);
        }
    }
    public void ShowInstructions(string instructions)
    {
        if (instructionsText != null)
        {
            instructionsText.text = instructions;
        }
    }
    
}
