using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GameObject on;
    public GameObject off;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Goal")
        {
            Debug.Log("GOAL");
            Debug.Log(transform.position.x + "x" + transform.position.y);
            on.SetActive(true);
            off.SetActive(false);
        }
        else if (other.tag == "Empty")
        {
            Debug.Log("Empty");
            Debug.Log(transform.position.x + "x" + transform.position.y);
            on.SetActive(false);
            off.SetActive(true);
        }
    }

    void OnBTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Goal")
        {
            Debug.Log("Exit");
            on.SetActive(false);
            off.SetActive(true);
        }
    }
}