using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMino : MonoBehaviour
{
	public GameObject[] Minos;
	int holdMinoType = -1;
	int MinoType;
	int Next1, Next2, Next3, Next4;
	GameObject next1, next2, next3, next4, holdMino;
	public GameObject NextObj1, NextObj2, NextObj3, NextObj4, HoldObj;
	GameObject[] objbox;
	int minoCopy = -1;
	List<int> MinoList = new List<int>();
	List<int> MinoListCopy = new List<int>();
	List<int> MinoListNext = new List<int>();
	// Start is called before the first frame update
	void Start()
	{
		for (int i = 0; i < 7; i++)
		{
			MinoList.Add(i);
		}
		MinoListCopy = new List<int>(MinoList);
		for (int j = 0; j < MinoListCopy.Count; j++)
		{
			int index = Random.Range(0, MinoList.Count);
			MinoListNext.Add(MinoList[index]);
			MinoList.Remove(MinoList[index]);
		}
		NewMino();
	}

	// Update is called once per frame
	void Update()
	{

	}
	public void Hold()
	{
		if (holdMinoType == -1)
		{
			holdMinoType = MinoType;
		}
		else
		{
			Destroy(holdMino);
			minoCopy = holdMinoType;
			holdMinoType = MinoType;
			MinoType = minoCopy;
		}
		holdMino = Instantiate(Minos[holdMinoType], HoldObj.transform.position, Quaternion.identity);
		Mino holdcs = holdMino.GetComponent<Mino>();
		holdcs.enabled = false;
		NewMino();
		minoCopy = -1;
	}
	public void NewMino()
	{
		if (minoCopy == -1)
		{
			if (objbox != null)
			{
				foreach (GameObject obj in objbox)
				{
					Destroy(obj);
				}
			}
			MinoType = MinoListNext[0];
			Next1 = MinoListNext[1];
			Next2 = MinoListNext[2];
			Next3 = MinoListNext[3];
			Next4 = MinoListNext[4];
			//ネクストの表示
			next1 = Instantiate(Minos[Next1], NextObj1.transform.position, Quaternion.identity);
			next1.transform.localScale+=new Vector3(0.05f,0.05f,0);
			Mino nextcs1 = next1.GetComponent<Mino>();
			nextcs1.enabled = false;
			next2 = Instantiate(Minos[Next2], NextObj2.transform.position, Quaternion.identity);
			Mino nextcs2 = next2.GetComponent<Mino>();
			nextcs2.enabled = false;
			next3 = Instantiate(Minos[Next3], NextObj3.transform.position, Quaternion.identity);
			Mino nextcs3 = next3.GetComponent<Mino>();
			nextcs3.enabled = false;
			next4 = Instantiate(Minos[Next4], NextObj4.transform.position, Quaternion.identity);
			Mino nextcs4 = next4.GetComponent<Mino>();
			nextcs4.enabled = false;
			objbox = new GameObject[] { next1, next2, next3, next4 };
		}
		if (MinoList.Count == 0)
		{
			List<int> Copy = new List<int>(MinoListCopy);
			for (int j = 0; j < MinoListCopy.Count; j++)
			{
				int index = Random.Range(0, Copy.Count);
				MinoList.Add(Copy[index]);
				Copy.Remove(Copy[index]);
			}
		}
		MinoListNext.Remove(MinoType);
		MinoListNext.Add(MinoList[0]);
		MinoList.Remove(MinoList[0]);
		if (MinoType == 0 || MinoType == 3)
		{
			transform.position = new Vector3(4.5f, 18.5f, 0);
		}
		else
		{
			transform.position = new Vector3(4, 19, 0);
		}
		Instantiate(Minos[MinoType], transform.position, Quaternion.identity);
	}
}
