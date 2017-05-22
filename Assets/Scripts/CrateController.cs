using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateController : MoveController {
    public GameObject on;
    public GameObject off;
    public bool onGoal;

    void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Goal")
		{
            onGoal = true;
            on.SetActive(true);
            off.SetActive(false);
        }
		else if (other.tag == "Empty")
		{
            onGoal = false;
			on.SetActive(false);
            off.SetActive(true);
		}
    }

	protected override void MoveEnded()
	{
        GameManager.instance.CheckWin();
        base.MoveEnded();
    }
}
