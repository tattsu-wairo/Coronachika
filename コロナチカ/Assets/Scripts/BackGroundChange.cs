using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundChange : MonoBehaviour
{
	public Sprite image1;
	public Sprite image2;
	public Sprite image3;
	public Sprite image4;
	GameManagement risky;
	// Start is called before the first frame update
	void Start()
	{
		GetComponent<Image>().sprite = image1;
		risky = FindObjectOfType<GameManagement>();
	}

	// Update is called once per frame
	void Update()
	{
		if (risky.risk < 10)
		{
			GetComponent<Image>().sprite = image1;
		}
		else if (risky.risk < 30)
		{
			GetComponent<Image>().sprite = image2;
		}
		else if (risky.risk < 40)
		{
			GetComponent<Image>().sprite = image3;
		}
		else
		{
			GetComponent<Image>().sprite = image4;
		}
	}
}
