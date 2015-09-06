using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WelcomePageScript : MonoBehaviour {

	public RectTransform credits, instructions, welcome;
	public float scrollSpeed = 1f;
	public Vector2 creditsPos, instructionsPos, welcomePos;

	void Start () {

		creditsPos = credits.anchoredPosition;
		instructionsPos = instructions.anchoredPosition;
		welcomePos = welcome.anchoredPosition;
		welcome.anchoredPosition = new Vector2(0f, 0f);
	}

	void Update () 
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void LaunchGame()
	{
		Application.LoadLevel("wpa level one");
	}

	public void ShowInstructions()
	{
		//Application.LoadLevel("instructions level");
		StartCoroutine(Instructions());
	}

	public void ShowCredits()
	{
		//Application.LoadLevel("credits level");
		StartCoroutine(Credits());
	}

	public void Quit()
	{
		Debug.Log("QUIT");
		Application.Quit();
	}

	public void InstructionsReturn()
	{
		StartCoroutine(IReturn());
	}

	private IEnumerator IReturn()
	{
		while(instructions.anchoredPosition.x > instructionsPos.x)
		{
			instructions.anchoredPosition =  new Vector2(
				instructions.anchoredPosition.x - scrollSpeed * Time.deltaTime, instructions.anchoredPosition.y);
			yield return null;
		}
		while (welcome.anchoredPosition.y > 0)
		{
			welcome.anchoredPosition = new Vector2(
				welcome.anchoredPosition.x, welcome.anchoredPosition.y - scrollSpeed * Time.deltaTime);
			yield return null;
		}
	}

	public void CreditsReturn()
	{
		StartCoroutine(CReturn());
	}

	private IEnumerator CReturn()
	{
		while(credits.anchoredPosition.x < creditsPos.x)
		{
			credits.anchoredPosition =  new Vector2(
				credits.anchoredPosition.x + scrollSpeed * Time.deltaTime, credits.anchoredPosition.y);
			yield return null;
		}
		while (welcome.anchoredPosition.y > 0)
		{
			welcome.anchoredPosition = new Vector2(
				welcome.anchoredPosition.x, welcome.anchoredPosition.y - scrollSpeed * Time.deltaTime);
			yield return null;
		}
	}

	private IEnumerator Credits()
	{
		while (welcome.anchoredPosition.y < welcomePos.y)
		{
			welcome.anchoredPosition = new Vector2(
				welcome.anchoredPosition.x, welcome.anchoredPosition.y + scrollSpeed * Time.deltaTime);
			yield return null;
		}

		while(credits.anchoredPosition.x > 0)
		{
			credits.anchoredPosition =  new Vector2(
				credits.anchoredPosition.x - scrollSpeed * Time.deltaTime, credits.anchoredPosition.y);
			yield return null;
		}
	}

	private IEnumerator Instructions()
	{
		while (welcome.anchoredPosition.y < welcomePos.y)
		{
			welcome.anchoredPosition = new Vector2(
				welcome.anchoredPosition.x, welcome.anchoredPosition.y + scrollSpeed * Time.deltaTime);
			yield return null;
		}

		while(instructions.anchoredPosition.x < 0)
		{
			instructions.anchoredPosition =  new Vector2(
				instructions.anchoredPosition.x + scrollSpeed * Time.deltaTime, instructions.anchoredPosition.y);
			yield return null;
		}
	}
}