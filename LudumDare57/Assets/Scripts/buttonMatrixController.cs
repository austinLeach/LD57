using UnityEngine;
using UnityEngine.UI;

public class buttonMatrixController : MonoBehaviour
{
    public Toggle[] toggleArr;
    public Toggle[] promptArr;

    private bool[] targetPattern = new bool[9];


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateTargetPattern();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForSuccess();
    }

    void GenerateTargetPattern()
    {
        string patternDisplay = "Target Pattern:\n";
        for (int i = 0; i < 9; i++)
        {
            promptArr[i].isOn = Random.value > 0.5f;
            patternDisplay += promptArr[i].isOn ? "1 " : "0 ";

            // Add a newline every 3 elements to form the grid
            if ((i + 1) % 3 == 0)
                patternDisplay += "\n";

        }

        Debug.Log(patternDisplay);
    }

    void CheckForSuccess()
    {
        for (int i = 0; i < 9; i++)
        {
            if (toggleArr[i].isOn != promptArr[i].isOn)
                return;
        }

        Debug.Log("Puzzle Solved!");
        // You can trigger any success action here (e.g., open a door)
    }
}
