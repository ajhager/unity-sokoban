using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static float scale = 2;
    public BoardManager boardScript;
    public int level;

    private int goals = 0;

    void Awake()
    {
		if (instance == null)
		{
            instance = this;
        }
		else if (instance != this)
		{
            Destroy(this);
        }

        DontDestroyOnLoad(this);
        boardScript = GetComponent<BoardManager>();
        goals = boardScript.SetupBoard(level);
    }

	public void CheckWin()
	{
        int currentGoals = 0;
        GameObject[] crates = GameObject.FindGameObjectsWithTag("Crate");
		foreach (GameObject crate in crates)
		{
            if (crate.GetComponent<CrateController>().onGoal)
			{
                currentGoals += 1;
            }
        }

		if (currentGoals == goals)
		{
            Debug.Log("YOU WIN!");
        }
    }
}
