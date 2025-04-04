using System;
using UnityEngine;

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
    float invincibleTime = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        defaultLayer = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("in invincible");
            if (isInvincible == false)
            {
                StartCoroutine(Invincibility());
            }
        }

        GlobalVariables.Timer(ref isInvincible, ref invincibleTime);

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
        }
    }

    private System.Collections.IEnumerator Invincibility()
    {
        gameObject.layer = LayerMask.NameToLayer(noCollisionLayerName);
        yield return new WaitForSeconds(invincibleTime);
        gameObject.layer = defaultLayer;
    }
}
