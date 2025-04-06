using UnityEngine;
using UnityEngine.EventSystems;

public class UILever : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float rotationSpeed = 0.2f;
    public float smoothSpeed = 10f;
    public float[] anchorAngles = { -45f, 45f };  // Only -45° and 45° as anchor points

    private RectTransform rectTransform;
    private Vector2 lastMousePosition;
    private float targetAngle;
    private bool isDragging = false;
    private float goalAngle;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        // Set the initial rotation to 45 degrees
        targetAngle = -45f;
        rectTransform.localEulerAngles = new Vector3(0, 0, targetAngle);
    }

    private void Start()
    {
        SetGoalAngle();
    }

    void SetGoalAngle()
    {
        float current = GetNearestAnchorAngle(GetCurrentAngle());

        goalAngle = (current == 45f) ? -45f : 45f;
        Debug.Log("Goal Angle: " + goalAngle);
    }

    public void CheckWin()
    {
        float current = GetNearestAnchorAngle(GetCurrentAngle());
        if (current == goalAngle)
        {
            Debug.Log("Lever Goal Accomplished");
            goalAngle = 180;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - lastMousePosition;
        lastMousePosition = eventData.position;

        // Calculate the rotation delta based on mouse movement
        float rotationDelta = -delta.y * rotationSpeed;
        targetAngle = Mathf.Clamp(GetCurrentAngle() + rotationDelta, anchorAngles[0], anchorAngles[anchorAngles.Length - 1]);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        targetAngle = GetNearestAnchorAngle(GetCurrentAngle());
    }

    void Update()
    {
        float currentZ = GetCurrentAngle();

        // Smoothly rotate towards the target angle
        float smoothed = Mathf.LerpAngle(currentZ, targetAngle, Time.deltaTime * smoothSpeed);

        // Apply the smooth angle to the rotation
        rectTransform.localEulerAngles = new Vector3(0, 0, smoothed);

        CheckWin();
    }

    private float GetCurrentAngle()
    {
        float z = rectTransform.localEulerAngles.z;
        // Convert to -180 to 180 range for correct angle wrapping
        return (z > 180f) ? z - 360f : z;
    }

    private float GetNearestAnchorAngle(float angle)
    {
        // Get the nearest anchor angle from the list
        float closest = anchorAngles[0];
        float minDiff = Mathf.Abs(angle - closest);

        foreach (float anchor in anchorAngles)
        {
            float diff = Mathf.Abs(angle - anchor);
            if (diff < minDiff)
            {
                minDiff = diff;
                closest = anchor;
            }
        }

        if (closest == 0)
        {
            if (angle < 0)
            {
                closest = -45;
            } else
            {
                closest = 45;
            }
        }
        return closest;
    }
}
