using UnityEngine;

public class PromptMaker : MonoBehaviour
{
    public UILever uiLever;
    public DialRandomize dial;
    public WarpButton warpButton;
    public DropdownPuzzle dropdown;
    public MultiScrollbarController sliders;
    public buttonMatrixController buttonMatrix;
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


    private void Start()
    {
        MakePrompt();
        panicTimer = Random.Range(10, 30);
    }

    private void Update()
    {
        if (!activePrompt)
        {
            MakePrompt();
        }
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
                isMatrixPrompt = false;
                activePrompt = false;
                warpButton.current++;
            }
        }
        GlobalVariables.Timer(ref isNotPanicing, ref panicTimer);
        if (!isNotPanicing)
        {
            PanicTime();
            isNotPanicing = true;
            panicTimer = Random.Range(15, 30);
        }
        if (panicingActivated)
        {
            if (panicButton.PanicWin())
            {
                panicingActivated = false;
                warpButton.current++;
            }
            
        }
    }

    void MakePrompt()
    {
        int random = 10;
        while (random ==  lastRandom) {
             random = Random.Range(0, 5);
        }
        lastRandom = random;

        Debug.Log(random);
        random = 4;
        if (random == 0 && warpButton.current < warpButton.maximum)
        {
            uiLever.SetGoalAngle();
            isUiLeverPrompt = true;
        }

        if (random == 1 && warpButton.current < warpButton.maximum)
        {
            dial.SetRandomGoal();
            isDialPrompt = true;
        }

        if (random == 2 && warpButton.current < warpButton.maximum)
        {
            dropdown.GenerateRandomWinCondition();
            isDropdownPrompt = true;
        }

        if (random == 3 && warpButton.current < warpButton.maximum)
        {
            sliders.GenerateTargetSequence();
            isSliderPrompt = true;
        }
        
        if (random == 4 && warpButton.current < warpButton.maximum)
        {
            buttonMatrix.GenerateTargetPattern();
            isMatrixPrompt = true;
        }

        activePrompt = true;
    }

    void PanicTime()
    {
        panicButton.SetPanicState();
        
    }
}
