using UnityEngine;

public class PlayerController : MoveController
{
    public LayerMask pushingLayers;

    Animator animator;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
    }

    bool Push(float xDir, float yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        collider.enabled = false;
        hit = Physics2D.Linecast(start, end, pushingLayers);
        collider.enabled = true;

        if (hit.transform == null)
        {
            return true;
        }

        return hit.transform.GetComponent<MoveController>().Move(xDir, yDir, out hit);
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
            if (horizontal > 0)
            {
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
            if (Push(horizontal, vertical, out hit))
            {
                Move(horizontal, vertical, out hit);
            }
        }
    }
}