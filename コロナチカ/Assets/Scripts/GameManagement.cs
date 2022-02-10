using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
	public Text highScoreText;//ハイスコア表示用のText
	private int hightScore;//ハイスコア用の変数
	private string hightScore_key = "HIGH SCORE";//ハイスコアの保存先キー
	private string Score_key = "SCORE";//スコアの保存先キー
	public Text scoreText;
	public Text timerText;
	public float gameTime = 10f;
	public float rate;
	int score;
	public int currentScore;
	public int clearScore = 600;
	public float risk = 0f;
	public Slider slider;
	// Start is called before the first frame update
	void Start()
	{
		Initialize();
		hightScore = PlayerPrefs.GetInt(hightScore_key, 0);
		highScoreText.text = "ハイスコア：" + hightScore.ToString();
	}

	// Update is called once per frame
	void Update()
	{
		TimeManager();
		ClearMino();
	}
	//ゲームを開始前の状態にする
	void Initialize()
	{
		{
			score = 0;
			rate=1;
		}
	}
	public void TimeManager()
	{
		risk += Time.deltaTime*rate;
		//gameTime -= Time.deltaTime;
		//seconds = (int)gameTime;
		//timerText.text = seconds.ToString();
		slider.value = risk;
		if (risk >= slider.maxValue)//時間制限を入れるならseconds==0を追加する
		{
			GameOver();
		}
	}
	public void RiskManger(int line)
	{
		if (line == 1)
		{
			risk -= 1;
		}
		else if (line == 2)
		{
			risk -= 10;
		}
		else if (line == 3)
		{
			risk -= 20;
		}
		else if (line == 4)
		{
			risk -= 40;
		}
		else
		{
			return;
		}
		if (risk <= 0)
		{
			risk = 0;
		}
		slider.value = risk;
	}
	public void AddScore(int line)
	{
		if (line == 1)
		{
			score += 10;
		}
		else if (line == 2)
		{
			score += 100;
		}
		else if (line == 3)
		{
			score += 200;
		}
		else if (line == 4)
		{
			score += 400;
		}
		else
		{
			return;
		}
		//currentScore += score;
		scoreText.text = "感染対策スコア：" + score.ToString();
	}

	public void GameOver()
	{
		if(score>hightScore)
		{
			hightScore=score;
			PlayerPrefs.SetInt(hightScore_key,hightScore);
		}
		PlayerPrefs.SetInt(Score_key,score);
		SceneManager.LoadScene("GameOverScene");
	}
	public void ClearMino()
	{
		Mino[] NoMino = FindObjectsOfType<Mino>();
		if (NoMino != null)
		{
			foreach (Mino mino in NoMino)
			{
				if (mino.gameObject.transform.childCount == 0)
				{
					Destroy(mino.gameObject);
				}
			}
		}
	}
}
