using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class BatteryScript : MonoBehaviour
{

    public float startingHealth;
    public float currentHealth;
    public Slider slider;
    internal bool healthHasChanged;
    public ParticleEmitter sparkles1, sparkles2;
    public GameObject spanner;
    public GameObject scoreText;
    ScoreScript scoreScript;
    public GameObject explosion;
    internal bool isDestroyed;
    Transform trans;
    AudioSource[] audio;
    //	public AudioSource repaired;
    internal FoWPlayerUnit FoW;
    Canvas canvas;
    public int maxView = 15;

    Image fill;
    float r, g;

    void Start()
    {
        currentHealth = 0f;
        slider.maxValue = startingHealth;
        slider.value = currentHealth;
        spanner.renderer.enabled = false;

        sparkles1.emit = false;
        sparkles2.emit = false;
        scoreScript = scoreText.GetComponent<ScoreScript>();
        trans = transform;
        audio = GetComponents<AudioSource>();
        FoW = GetComponent<FoWPlayerUnit>();
        fill = slider.GetComponentsInChildren<UnityEngine.UI.Image>()
            .FirstOrDefault(t => t.name == "Fill");
        canvas = GetComponentInChildren<Canvas>();
    }



    void Update()
    {
        if (canvas && slider != null)
        {
            r = (startingHealth - currentHealth) * (255 / startingHealth);
            g = currentHealth * 255 / startingHealth;
            fill.color = new Color(r, g, 0f);
        }

        if (currentHealth <= 2 && FoW)
        {
            FoW.enabled = false;
            if (canvas)
                canvas.enabled = false;
        }

        if (healthHasChanged)
        {
            scoreScript.numFullBatteriesHasChanged = true;
            slider.value = currentHealth;
            healthHasChanged = false;
            if (currentHealth < startingHealth)
            {
                sparkles1.emit = false;
                sparkles2.emit = false;
                if (canvas)
                    canvas.enabled = true;

            }
            else
            {
                sparkles1.emit = true;
                sparkles2.emit = true;
            }
        }
        FoW.ViewRadiusInTiles = (byte)Mathf.Clamp(currentHealth / (startingHealth / maxView), 0, maxView);
    }


    IEnumerator LoadNewLevel(string level)
    {
        yield return new WaitForSeconds(2f);
        Application.LoadLevel(level);
    }

    void OnMouseDown()
    {
        GameManagerScript.selectedSpider.GetComponent<AIPath>().target = transform;
        GameManagerScript.selectedSpider.GetComponent<EnemyScript>().OnTargetAcquired();
    }
    bool dying = false;
    public void Die()
    {
        if (!dying)
        {
            audio[Random.Range(0, audio.Length)].Play();

            GameObject clone = Instantiate(explosion, new Vector3(trans.position.x, trans.position.y + 5, trans.position.z), trans.rotation) as GameObject;
            isDestroyed = true; // so healerScript can switch to !hasBattery as G
            Destroy(clone, 3f);
            Destroy(gameObject, 2f);

            if (GameObject.FindGameObjectsWithTag("battery").Length == 0)
            {
                StartCoroutine(LoadNewLevel("game over level"));
            }
            dying = true;
        }
    }
}