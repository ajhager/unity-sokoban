using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public BoardManager boardScript;

    public int level;

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
        boardScript.SetupBoard(level);
    }

    void Update()
    {
    }
}
