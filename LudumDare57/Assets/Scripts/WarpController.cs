using UnityEngine;
using UnityEngine.UI;

public class WarpButton : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image mask;
    public Button warpButton;
    public Toggle keyToggle;

    private void Start()
    {
        UpdateUIState();
        keyToggle.onValueChanged.AddListener(delegate { UpdateUIState(); });
    }

    private void Update()
    {
        GetCurrentFill();
        UpdateUIState();
    }
    void GetCurrentFill()
    {
        float fillAmount = (float)current/ (float)maximum;
        mask.fillAmount = fillAmount;
    }

    void UpdateUIState()
    {
        bool isAtMax = current >= maximum;

        // Toggle becomes interactable only when current reaches maximum
        keyToggle.interactable = isAtMax;

        // Button is only interactable when current == maximum AND toggle is on
        if (warpButton != null)
        {
            warpButton.interactable = isAtMax && keyToggle.isOn;
        }
    }
}
