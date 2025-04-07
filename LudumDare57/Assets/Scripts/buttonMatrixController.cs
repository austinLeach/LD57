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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateTargetPattern()
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

    public bool CheckForSuccess()
    {
        for (int i = 0; i < 9; i++)
        {
            if (toggleArr[i].isOn != promptArr[i].isOn)
                return false;
        }

        Debug.Log("Puzzle Solved!");
        return true;
    }
}
