using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
	public Text highScoreText;//ハイスコア表示用のText
	private int hightScore;//ハイスコア用の変数
	private string hightScore_key = "HIGH SCORE";//ハイスコアの保存先キー
	public Text ScoreText;//スコア表示用のText
	private int Score;//スコア用の変数
	private string Score_key = "SCORE";//スコアの保存先キー
	void Start()
	{
		hightScore = PlayerPrefs.GetInt(hightScore_key, 0);
		highScoreText.text = "ハイスコア：" + hightScore.ToString();
		Score = PlayerPrefs.GetInt(Score_key, 0);
		ScoreText.text = "今回のスコア：" + Score.ToString();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
