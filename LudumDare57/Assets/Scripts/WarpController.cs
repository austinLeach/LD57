﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class WarpButton : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image mask;
    public Button warpButton;
    public Toggle keyToggle;
    public PlayerController playerController;

    public Camera mainCamera;
    public float fovIncreaseAmount = 20f; // How much to increase the FOV by
    public float effectDuration = 1.5f; // Duration of the FOV effect

    private float originalFOV;
    private Coroutine fovCoroutine;

    public AudioClip warpSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
    
    public void ActivateWarp()
    {
        if (fovCoroutine == null)
        {
            fovCoroutine = StartCoroutine(WarpFOVEffect());
        }
    }
    private IEnumerator WarpFOVEffect()
    {
        audioSource.PlayOneShot(warpSound);
        playerController.speed = 5;
        playerController.health = 5;
        originalFOV = mainCamera.fieldOfView;
        float targetFOV = originalFOV + fovIncreaseAmount;
        float elapsed = 0f;

        while (elapsed < effectDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / effectDuration;
            mainCamera.fieldOfView = Mathf.Lerp(originalFOV, targetFOV, t);
            yield return null;
        }

        // Reset FOV after reaching peak
        mainCamera.fieldOfView = originalFOV;
        fovCoroutine = null;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }

}
