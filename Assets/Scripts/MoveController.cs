using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {
    public float moveTime = 0.5f;
    public LayerMask blockingLayer;
    public LayerMask pushingLayer;

    private new BoxCollider2D collider;
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private bool moving = false;

    void Start()
	{
        collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

	bool Move(float xDir, float yDir, out RaycastHit2D hit)
	{
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        collider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        collider.enabled = true;

		if (hit.transform == null)
		{
            moving = true;

            StartCoroutine(SmoothMovement(end));
            return true;
        }

        return false;
    }

	IEnumerator SmoothMovement(Vector3 end)
	{
        float remainingDistance = (transform.position - end).sqrMagnitude;

		while (remainingDistance > float.Epsilon)
		{
            Vector3 newPosition = Vector3.MoveTowards(rigidbody.position, end, (1 / moveTime) * Time.deltaTime);
            rigidbody.MovePosition(newPosition);
            remainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

        moving = false;
    }

    public void Update()
	{
		if (moving)
		{
            return;
        }


        float horizontal = Input.GetAxisRaw("Horizontal") / GameManager.scale;
        float vertical = Input.GetAxisRaw("Vertical") / GameManager.scale;

		if (horizontal != 0)
		{
            vertical = 0;
        }

		animator.SetBool("walkLeft", false);
        animator.SetBool("walkRight", false);
        animator.SetBool("walkUp", false);
        animator.SetBool("walkDown", false);

        if (horizontal != 0 || vertical != 0)
		{
			if (horizontal > 0) {
                animator.SetTrigger("walkRight");
            }
            else if (horizontal < 0)
            {
                animator.SetTrigger("walkLeft");
            }
            else if (vertical > 0)
            {
                animator.SetTrigger("walkUp");
            }
            else if (vertical < 0)
            {
                animator.SetTrigger("walkDown");
            }

            RaycastHit2D hit = new RaycastHit2D();
            Move(horizontal, vertical, out hit);
        }
    }
}