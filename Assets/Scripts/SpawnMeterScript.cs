using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpawnMeterScript : MonoBehaviour {
	Slider slider;
	public float spawnRate;
	internal bool isGrowing;
	public GameObject enemy;
	EnemyScript enemyScript;
	internal bool isSpawning;
	internal GameObject clone;
	//public Text text;
	public Text spawnButtonText;
	public float scoutCost, soldierCost, queenCost;

	void Start () {
		slider = GetComponent<Slider>();
		slider.value = 0;
		isGrowing = true;	
		spawnButtonText.text = "Stand by";
	//	text.text = slider.value.ToString();
	}
	float sliderVal;
	void Update () {
		if (isGrowing)
		{
		slider.value += spawnRate * Time.deltaTime;

			sliderVal = slider.value;
			if (sliderVal < scoutCost)
			{
				spawnButtonText.text = "Stand by";
			}
			else if (sliderVal >= scoutCost && sliderVal < soldierCost)
			{
				spawnButtonText.text = "Spawn scout";
			}
			else if (sliderVal >= soldierCost && sliderVal < queenCost)
			{
				spawnButtonText.text = "Spawn soldier";
			}
			else if (sliderVal >= queenCost)
			{
				spawnButtonText.text = "Spawn queen";
			}
		}
	
	}
	float val = 0;
	public void OnSpawnButtonClick()
	{
		if (!isSpawning && slider.value > scoutCost)
		{
		isGrowing = false;
			val = slider.value;
			slider.value = 0;
		CreateSpider();
		isSpawning = true;	
		spawnButtonText.text = "Stand by";
		}
	}

	public void CreateSpider()
	{
		clone = Instantiate(enemy, Input.mousePosition, Quaternion.identity) as GameObject;
		enemyScript = clone.GetComponent<EnemyScript>();
		Transform trans = clone.transform;

		//enemyScript.isSpawning = true;
		
		Slider slider = clone.GetComponentInChildren<Slider>();
		Vector3 sliderScale = slider.transform.localScale;
		Vector3[] scales = new Vector3[]{trans.localScale * 0.7f, trans.localScale,  trans.localScale * 2f};
		float[] startingHealths = new float[]{10f, 100f, 300f};
		float[] speeds = {2f, 1.5f, 1f};
		float[] damages = {5f, 10f, 20f};
		float[] healingRates = {0f, 10f, 30f};
		float[] animSpeeds = {5f, 1.5f, 0.6f};
		int[] points = {1, 2, 5};
		int[] masses = {1, 2, 5};
		float[] stunTimes = {1f, 0.3f, 0.05f};

		int size = 0;

		if (val > scoutCost && val <= soldierCost)
		size = 0;
		else if (val >= soldierCost && val < queenCost)
			size = 1;
		else if (val >= 100)
			size = 2;

		trans.localScale = scales[size];
		slider.transform.localScale = sliderScale;
		enemyScript.startingHealth = startingHealths[size]; 
		
		enemyScript.walkSpeed = speeds[size];
		enemyScript.runSpeed = speeds[size] * 3f;
		clone.GetComponent<AIPath>().speed = speeds[size];
		
		enemyScript.walkAnimSpeed = animSpeeds[size];
		enemyScript.runAnimSpeed = animSpeeds[size];
		
		enemyScript.damage = damages[size];	
		//enemyScript.slider.maxValue = startingHealths[size];
	//	enemyScript.slider.minValue = 0;
	//	enemyScript.slider.value = startingHealths[size];
		enemyScript.currentHealth = startingHealths[size];
		enemyScript.healingRate = healingRates[size];
	//	enemyScript.rigidbody.mass = masses[size];
		enemyScript.stunTime = stunTimes[size];

		clone.collider.enabled = false;
		clone.layer = 2;

		GameObject mesh = enemyScript.mesh;
		Color color = mesh.renderer.material.color;
		color.a = 0.3f;
		mesh.renderer.material.color = new Color(color.r, color.g, color.b, color.a);

	}
}
