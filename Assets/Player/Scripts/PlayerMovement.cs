using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public bool CanDash { get; private set; }
    public bool IsDashing { get; private set; }

    [SerializeField] private TrailRenderer tr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        CanDash = true;
        IsDashing = false;
    }

    public void Move(float playerSpeed, Vector2 movement)
    {
        if (IsDashing) return;

        float angleAdd = movement.Equals(Vector2.zero) ? 0 : -90;

        float angle = (Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg) + angleAdd;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = targetRotation;

        rb.velocity = movement.normalized * playerSpeed;

    }

    public IEnumerator Dash(float playerSpeed, Vector2 movement, float dashingPower, float dashingTime, float dashingCooldown)
    {
        CanDash = false;
        IsDashing = true;
        rb.velocity = movement * playerSpeed * dashingPower;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        IsDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        CanDash = true;
    }
}
