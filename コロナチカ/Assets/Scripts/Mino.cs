using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mino : MonoBehaviour
{
	public float previousTime;
	public float fallTime = 0.05f;
	public Vector3 rotationPoint;
	static int width = 10;
	static int height = 20;
	static Transform[,] grid = new Transform[width, height];
	float inputCount = 0f;
	bool longInputFlag = false;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		MinoMovement();
	}
	void MinoMovement()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			while (true)
			{
				transform.position += new Vector3(0, -1, 0);
				if (!isHit())
				{
					transform.position -= new Vector3(0, -1, 0);
					AddToGrid();
					CheckLines();
					//スクリプトのoff
					this.enabled = false;
					FindObjectOfType<SpawnMino>().NewMino();
					break;
				}
				previousTime = Time.time;
			}
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Invoke("LongInput", 0.3f);
			transform.position += new Vector3(-1, 0, 0);
			if (!isHit())
			{
				transform.position -= new Vector3(-1, 0, 0);
			}
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			Invoke("LongInput", 0.3f);
			transform.position += new Vector3(1, 0, 0);
			if (!isHit())
			{
				transform.position -= new Vector3(1, 0, 0);
			}
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - previousTime >= fallTime)
		{
			transform.position += new Vector3(0, -1, 0);
			if (!isHit())
			{
				transform.position -= new Vector3(0, -1, 0);
				AddToGrid();
				CheckLines();
				//スクリプトのoff
				this.enabled = false;
				FindObjectOfType<SpawnMino>().NewMino();
			}
			previousTime = Time.time;
		}
		else if (Input.GetKeyDown(KeyCode.A))
		{
			transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
			if (!isHit())
			{
				transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, -1), 90);
			}
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, -1), 90);
			if (!isHit())
			{
				transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
			}
		}
		else if (Input.GetKey(KeyCode.LeftArrow) && longInputFlag)
		{
			inputCount += Time.deltaTime;
			if (inputCount > 0.05f)
			{
				transform.position += new Vector3(-1, 0, 0);
				if (!isHit())
				{
					transform.position -= new Vector3(-1, 0, 0);
				}
				inputCount = 0;
			}
		}
		else if (Input.GetKey(KeyCode.RightArrow) && longInputFlag)
		{
			inputCount += Time.deltaTime;
			if (inputCount > 0.05f)
			{
				transform.position += new Vector3(1, 0, 0);
				if (!isHit())
				{
					transform.position -= new Vector3(1, 0, 0);
				}
				inputCount = 0;
			}
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			inputCount += Time.deltaTime;
			if (inputCount > 0.07f)
			{
				transform.position += new Vector3(0, -1, 0);
				if (!isHit())
				{
					transform.position -= new Vector3(0, -1, 0);
					AddToGrid();
					CheckLines();
					//スクリプトのoff
					this.enabled = false;
					FindObjectOfType<SpawnMino>().NewMino();
				}
				previousTime = Time.time;
				inputCount = 0;
			}
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow))
		{
			CancelInvoke();
			inputCount = 0;
			longInputFlag = false;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//スクリプトのoff
			this.enabled = false;
			FindObjectOfType<SpawnMino>().Hold();
			Destroy(gameObject);
		}
	}
	void LongInput()
	{
		longInputFlag = true;
	}

	public void CheckLines()
	{
		int Lines = 0;
		for (int i = height - 1; i >= 0; i--)
		{
			if (HasLine(i))
			{
				//Score加算
				DeleteLine(i);
				RowDown(i);
				Lines++;
			}
		}
		CheckTag();
		FindObjectOfType<GameManagement>().AddScore(Lines);
		FindObjectOfType<GameManagement>().RiskManger(Lines);
	}

	bool HasLine(int i)
	{
		for (int j = 0; j < width; j++)
		{
			if (grid[j, i] == null)
			{
				return false;
			}
		}
		return true;
	}

	void DeleteLine(int i)
	{
		for (int j = 0; j < width; j++)
		{
			Destroy(grid[j, i].gameObject);
			grid[j, i] = null;
		}
	}

	void RowDown(int i)
	{
		for (int h = i; h < height; h++)
		{
			for (int w = 0; w < width; w++)
			{
				if (grid[w, h] != null)
				{
					grid[w, h - 1] = grid[w, h];
					grid[w, h] = null;
					grid[w, h - 1].transform.position -= new Vector3(0, 1, 0);
				}
			}
		}
	}
	void AddToGrid()
	{
		foreach (Transform children in transform)
		{
			int x = Mathf.RoundToInt(children.transform.position.x);
			int y = Mathf.RoundToInt(children.transform.position.y);
			if (y >= height - 1)
			{
				//GameOverシーンに移動
				FindObjectOfType<GameManagement>().GameOver();
			}
			grid[x, y] = children;
		}
	}
	void CheckTag()
	{
		GameManagement game = FindObjectOfType<GameManagement>();
		game.rate = 1;
		for (int i = 0; i < width - 1; i++)
		{
			for (int j = 0; j < height - 1; j++)
			{
				if (grid[i, j] != null && grid[i, j].gameObject.tag == "head")
				{
					if ((grid[i + 1, j] != null && grid[i + 1, j].gameObject.tag == "head") || (grid[i, j + 1] != null && grid[i, j + 1].gameObject.tag == "head"))
					{
						game.rate += 1f;
					}
				}
			}
		}
	}

	bool isHit()
	{
		foreach (Transform children in transform)
		{
			int x = Mathf.RoundToInt(children.transform.position.x);
			int y = Mathf.RoundToInt(children.transform.position.y);
			if (x < 0 || x >= width || y < 0 || y >= height)
			{
				return false;
			}
			if (grid[x, y] != null)
			{
				return false;
			}
		}
		return true;
	}
}
