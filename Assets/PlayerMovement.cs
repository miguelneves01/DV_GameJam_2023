using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2f;
    private Rigidbody2D rb;
    float moveHz, moveVt;
    Vector2 movement;

    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 4f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) return;

        moveHz = Input.GetAxis("Horizontal");
        moveVt = Input.GetAxis("Vertical");
        movement = new Vector2(moveHz, moveVt);
        rb.velocity = movement * playerSpeed;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        rb.velocity = movement * playerSpeed * dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
