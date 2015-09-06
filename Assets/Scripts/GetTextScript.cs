using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetTextScript : MonoBehaviour {

	public Text inputText;
	public string teamName;
	public RectTransform GetNamePanel, ShowScorePanel;
	
	void Start () {

	}

	public void EnterName()
	{
		teamName = inputText.text;
		GetNamePanel.anchoredPosition = new Vector2 (1000f, 1000f);
		ShowScorePanel.anchoredPosition = new Vector2 (0f, 0f);

	}
}