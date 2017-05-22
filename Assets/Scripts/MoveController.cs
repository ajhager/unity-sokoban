using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {
    public float moveTime = 0.5f;
    public LayerMask blockingLayers;

    protected new BoxCollider2D collider;
    protected new Rigidbody2D rigidbody;
    protected bool moving = false;

    protected virtual void Start()
	{
        collider = GetComponent<BoxCollider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

	public bool Move(float xDir, float yDir, out RaycastHit2D hit)
	{
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

		// Remove yourself from any blocklayers during linecast so you don't block yourself.
		// Disabling the boxcollider typically work, but it causes triggers to hit twice.
        int layer = gameObject.layer;
        gameObject.layer = 0;
        hit = Physics2D.Linecast(start, end, blockingLayers);
        gameObject.layer = layer;

        if (hit.transform == null)
		{
            moving = true;

            StartCoroutine(SmoothMovement(end));
            return true;
        }

        return false;
    }

	public IEnumerator SmoothMovement(Vector3 end)
	{
        float remainingDistance = (transform.position - end).sqrMagnitude;

		while (remainingDistance > float.Epsilon)
		{
            Vector3 newPosition = Vector3.MoveTowards(rigidbody.position, end, (1 / moveTime) * Time.deltaTime);
            rigidbody.MovePosition(newPosition);
            remainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

        MoveEnded();
    }

	protected virtual void MoveEnded()
	{
        moving = false;
	}
}