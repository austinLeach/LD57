using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float speed = 10.0f;

    private int lane = 1; //0 left, 1 middle, 2 right
    public float laneDistance = 4;

    private int defaultLayer;
    public string noCollisionLayerName = "no collision";
    bool isInvincible = false;
    float invincibleTime = .5f;
    bool inLockout = false;
    float invincibleLockoutTime = 1f;
    public int health = 5;
    private SpriteRenderer sprite;
    public TextMeshProUGUI healthText;
    public GameObject gameOverPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        controller = GetComponent<CharacterController>();
        defaultLayer = gameObject.layer;
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthUI();
        direction.z = speed;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
            lane++;
            if (lane == 3)
            {
                lane = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
            lane--;
            if (lane == -1)
            {
                lane = 0;
            }
        }

        GlobalVariables.Timer(ref inLockout, ref invincibleLockoutTime);

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        } else if (lane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        if (transform.position == targetPosition) {
            return;
        }
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 40 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
        {
            controller.Move(moveDir);
        } else
        {
            controller.Move(diff);
        }
        if (health <= 0)
        {
            HandleGameOver();
        }
        
    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle" && !isInvincible)
        {
            Destroy(hit.gameObject);
            Debug.Log("collision detected");

            health--;

        }
        if (hit.transform.tag == "EndWall")
        {
            health = 0;
        }
    }

    public void invincibilityPress()
    {
        Debug.Log("in invincible");
        if (isInvincible == false && inLockout == false)
        {
            isInvincible = true;
            inLockout = true;
            invincibleLockoutTime = .75f;
            sprite.enabled = false;
            StartCoroutine(Invincibility());
        }
    }

    private System.Collections.IEnumerator Invincibility()
    {
        gameObject.layer = LayerMask.NameToLayer(noCollisionLayerName);
        yield return new WaitForSeconds(invincibleTime);
        gameObject.layer = defaultLayer;
        sprite.enabled = true;
        isInvincible = false;
    }


    private void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = "Health: " + health;
    }

    private void HandleGameOver()
    {
        Time.timeScale = 0f; // Pause the game
        gameOverPanel.SetActive(true); // Show the retry button
    }

    public void Retry()
    {
        Time.timeScale = 1f; // Unpause before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
