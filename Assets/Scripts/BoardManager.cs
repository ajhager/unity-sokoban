using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private string[] level1 = {
        "#########",
        "#....***#",
        "#.#.#*#*#",
        "#.#..***#",
        "#.ooo.#.#",
        "#.o@o...#",
        "#.ooo##.#",
        "#.......#",
        "#########"
    };

    public GameObject crate;
    public GameObject floor;
    public GameObject goal;
    public GameObject player;
    public GameObject wall;

    private Transform board;
    private float scale = 2;

    void SetupBoard()
    {
        board = new GameObject("Board").transform;
        string[] level = level1;
        float maxY = level.Length;
        float maxX = 0;

        for (int y = 0; y < level.Length; y++)
		{
            string row = level[y];
            maxX = Mathf.Max(row.Length, maxX);
            for (int x = 0; x < row.Length; x++)
			{
                GameObject tile = null;
				switch(row[x])
				{
                    case '#':
                        tile = wall;
                        break;
                    case '@':
                        tile = player;
                        break;
                    case 'o':
                        tile = crate;
                        break;
                    case '*':
                        tile = goal;
                        break;
					case '.':
                        tile = floor;
                        break;

                }

                GameObject instance = Instantiate(tile, new Vector3(x/scale, (level.Length - y)/scale, 0), Quaternion.identity);
                instance.transform.SetParent(board);

                if (tile != floor)
                {
                    instance = Instantiate(floor, new Vector3(x/scale, (level.Length - y)/scale, 0), Quaternion.identity);
                    instance.transform.SetParent(board);
                }
            }
        }

        board.position = new Vector3(-(maxX / 2)/scale, -(maxY / 2)/scale, 0);
    }

    void Start()
    {
        SetupBoard();
    }

    void Update()
    {

    }
}