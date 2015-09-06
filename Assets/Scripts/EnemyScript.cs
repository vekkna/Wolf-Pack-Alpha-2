using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using Pathfinding;

public class EnemyScript : MonoBehaviour
{
    public float startingHealth;
    public float healingRate, Xoffset, Yoffset, Zoffset;
    internal float currentHealth;
    public Slider healthSlider, growthSlider;
    private Canvas canvas;
    GameObject[] batteries;
    public float damage;
    bool isFleeing;
    public float fleeingTime;
    public GameObject spider;
    public float walkSpeed, runSpeed;
    bool isDead;
    internal bool healthHasChanged;
    GameObject[] players;
    AIPath aiPath;
    bool isChasingBattery;
    public float offset;
    internal Animation anim;
    Transform trans;
    Transform healthSliderTransform, growthSliderTransform;
    private Vector3 scale;
    public EnemySpawnScript spawner;
    internal float walkAnimSpeed, runAnimSpeed;
    public GameObject mesh;
    internal string lastHit;
    internal int mass;
    internal float stunTime;
    internal bool isSelected;
    public float YOffset;
    AudioSource[] audio;
    AudioSource shriek, roar;
    Vector3 gSliderOffset, hSliderOffset;
    public ParticleSystem growthParticles;
    public GameObject soldier, queen;
    public int growthRate = 1000;
    Image fill;
    float r, g;
    public bool isScout, isSoldier, isQueen;

    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        currentHealth = startingHealth;
        healthSlider.maxValue = startingHealth;
        anim = spider.animation;
        anim["Death"].wrapMode = WrapMode.Once;
        anim["Walk"].wrapMode = WrapMode.Loop;
        anim["Run"].wrapMode = WrapMode.Loop;
        anim["Attack"].wrapMode = WrapMode.Loop;
        aiPath = GetComponent<AIPath>();
        players = GameObject.FindGameObjectsWithTag("player");
        StartCoroutine(SearchForTarget());
        trans = transform;
        healthSliderTransform = healthSlider.transform;
        healthSlider.value = currentHealth;
        growthSliderTransform = growthSlider.transform;
        growthSlider.maxValue = growthRate;
        growthSlider.value = 0;

        gSliderOffset = trans.position - growthSliderTransform.position;
        hSliderOffset = trans.position - healthSliderTransform.position;

        audio = GetComponents<AudioSource>();
        shriek = audio[0];
        roar = audio[1];

        if (isSoldier || isQueen)
        {
            roar.Play();
            growthParticles.Play();
        }

        fill = healthSlider.GetComponentsInChildren<UnityEngine.UI.Image>()
            .FirstOrDefault(t => t.name == "Fill");

    }

    int batteryIndex;
    float dist0, dist1;

    IEnumerator SearchForTarget()
    {
        while (true)
        {

            {
                if (CanSeePlayer(players[0]) && !CanSeePlayer(players[1]))
                {
                    TargetAPlayer(players[0]);

                }
                else if (CanSeePlayer(players[1]) && !CanSeePlayer(players[0]))
                {
                    TargetAPlayer(players[1]);

                }
                else if (CanSeePlayer(players[0]) && CanSeePlayer(players[1]))
                {
                    dist0 = (trans.position - players[0].transform.position).sqrMagnitude;
                    dist1 = (trans.position - players[1].transform.position).sqrMagnitude;
                    TargetAPlayer((dist0 < dist1) ? players[0] : players[1]);
                }
                else if (!CanSeePlayer(players[0]) && !CanSeePlayer(players[1]) && !isChasingBattery)
                {
                    batteries = GameObject.FindGameObjectsWithTag("battery");
                    var healthyBatteries =
(from battery in batteries
 where battery.GetComponent<BatteryScript>().currentHealth > 0
 select battery).ToArray();

                    if (healthyBatteries.Length > 0)
                    {
                        batteryIndex = Random.Range(0, healthyBatteries.Length);
                        aiPath.target = healthyBatteries[batteryIndex].transform;
                    }
                    else
                    {
                        batteryIndex = Random.Range(0, batteries.Length);
                        aiPath.target = batteries[batteryIndex].transform;
                    }

                    isChasingBattery = true;
                    anim["Walk"].speed = walkAnimSpeed;
                    anim.Play("Walk");
                }

                yield return new WaitForSeconds(1.0f);
            }

        }
    }

    public void OnTargetAcquired()
    {
        anim["Walk"].speed = walkAnimSpeed;
        anim.Play("Walk");
    }

    public void Deselect()
    {
        isSelected = false;

    }

    void TargetAPlayer(GameObject player)
    {
        if (!isDead)
        {
            aiPath.target = player.transform;
            isChasingBattery = false;
            anim.Play("Walk");
            aiPath.speed = walkSpeed;
        }
    }

    Vector3 pos = new Vector3();
    Vector3 dir = new Vector3();
    RaycastHit hit;

    bool CanSeePlayer(GameObject player)
    {
        //if (isScout) return false;
        if (!player.GetComponent<Mover>().isFrozen)
        {
            pos.x = transform.position.x;
            pos.y = transform.position.y + offset;
            pos.z = transform.position.z;
            dir = player.transform.position - pos;
            if (Physics.Raycast(pos, dir, out hit))
            {
                if (hit.transform.tag == "player")
                {
                    if (trans != null)
                    {
                        if (Vector3.Dot(trans.TransformDirection(Vector3.forward), dir) > 0)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public void Arrive(Vector3 pos)
    {
        gameObject.layer = 0;
        collider.enabled = true;
        //	isSpawning = false;
        transform.position = new Vector3(pos.x, 1.7f, pos.z);
        Color c = mesh.renderer.material.color;
        mesh.renderer.material.color = new Color(c.r, c.g, c.b, 1.0f);



    }

    bool isAttacking;
    public Transform centreOfScreen;

    void Update()/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    {
        if (healthSlider != null && fill != null)
        {
            r = (startingHealth - currentHealth) * (255 / startingHealth);
            g = currentHealth * 255 / startingHealth;
            fill.color = new Color(r, g, 0f);
        }

        Regenerate();


        if (!isAttacking)
        {
            if (!IsMoving())
            {
                anim["Walk"].speed = 0f;
                //StopCoroutine(SearchForTarget());
                //	StartCoroutine(SearchForTarget());
            }
            else
            {
                anim["Walk"].speed = walkSpeed;
                //	animation.Play("Walk");
            }
        }
        else
        {
            anim.Play("Attack");
        }

    }

    void Regenerate()
    {
        if (!isDead)
        {
            if (currentHealth > 0 && currentHealth <= startingHealth)
            {
                currentHealth += healingRate * Time.deltaTime;
                healthHasChanged = true;
            }
            else if (currentHealth < 0 && !isDead)
            {
                StartCoroutine(Die());
            }
            else
            {
                healthHasChanged = false;
            }

            if (healthHasChanged)
            {
                if (healthSlider.enabled)
                {
                    healthSlider.value = currentHealth;
                }
                healthHasChanged = false;
            }
        }
    }

    Vector3 lastPos;

    bool IsMoving()
    {
        if (trans.position == lastPos)
            return false;
        else
        {
            lastPos = trans.position;
            return true;
        }
    }

    void LateUpdate()
    {
        if (canvas && canvas.enabled)
        {
            if (healthSlider && healthSlider.enabled)
            {
                healthSliderTransform.rotation = Quaternion.Euler(90, 0, 90);
                healthSliderTransform.position = trans.position + hSliderOffset;
            }
            if (growthSlider && growthSlider.enabled)
            {
                growthSliderTransform.rotation = Quaternion.Euler(90, 0, 90);
                growthSliderTransform.position = trans.position + gSliderOffset;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "battery")
        {
            anim.Play("Attack");
        }
        if (other.tag == "player")
        {
            Mover m = other.GetComponent<Mover>();
            if (!m.isFrozen)
            {
                //	other.GetComponent<Mover> ().Die ();
                m.isFrozen = true;

                var frozenPlayers =
    (from p in GameObject.FindGameObjectsWithTag("player")
     where p.GetComponent<Mover>().isFrozen
     select p).ToArray();

                if (frozenPlayers.Length == 2)
                {
                    GameManagerScript.spiderWon = true;
                    StartCoroutine(LoadNewLevel("game over level"));
                }

                m.anim.Stop("move");
                other.GetComponentInChildren<ShooterScript>().isFrozen = true;
                if (m.electricity)
                {
                    m.electricity.enableEmission = true;
                }
                m.audio.Play();

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        anim.Play("Walk");
    }

    BatteryScript batteryScript;
    float x, y, z;
    int damageDealt = 0;

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "battery" && !isDead)
        {
            batteryScript = other.GetComponent<BatteryScript>();
            if (batteryScript.currentHealth > 0)
            {
                batteryScript.currentHealth -= (damage * Time.deltaTime);
                if (!isQueen)
                {
                    growthSlider.value += Time.deltaTime;

                    if (growthSlider.value == growthRate)
                    {
                        if (isScout)
                        {
                            Instantiate(soldier, new Vector3(trans.position.x, trans.position.y + 0.6f, trans.position.z), trans.rotation);
                            Destroy(gameObject);
                        }
                        else if (isSoldier)
                        {
                            Instantiate(queen, new Vector3(trans.position.x, trans.position.y + 0.6f, trans.position.z), trans.rotation);
                            Destroy(gameObject);
                        }

                        growthSlider.value = 0;
                    }
                }
                batteryScript.healthHasChanged = true;
            }
            else
            {

                if (isQueen)
                {
                    batteryScript.Die();
                }
                isChasingBattery = false;
                anim.Stop("Attack");
                anim.Play("Walk");
            }
        }
    }

    public void Flee()
    {
        StopCoroutine(SearchForTarget());
        StartCoroutine(FleeCoroutine());
    }

    IEnumerator FleeCoroutine()
    {
        yield return new WaitForSeconds(stunTime);
        if (!isDead)
        {
            GetComponent<CharacterController>().enabled = true;
            SearchForTarget();
            anim["Run"].speed = runAnimSpeed;
            anim.Play("Run");
            aiPath.speed = runSpeed;
            yield return new WaitForSeconds(fleeingTime);
            if (!isDead)
                StartCoroutine(SearchForTarget());

        }
    }

    IEnumerator Die()
    {
        anim.Play("Death");
        isDead = true;
        collider.enabled = false;
        GetComponent<CharacterController>().enabled = false;
        Destroy(gameObject.rigidbody);

        Destroy(healthSlider.gameObject);
        // set material to transparent diffuse!!!
        Color start = mesh.renderer.material.color;
        Color end = new Color(start.r, start.g, start.b, 0.0f);
        float t = 0.0f;
        float fadeRate = 0.5f;
        while (mesh.renderer.material.color.a >= 0.1f)
        {
            t += Time.deltaTime;
            mesh.renderer.material.color = Color.Lerp(start, end, t * fadeRate);
            yield return null;
        }
        Destroy(gameObject);
    }

    IEnumerator LoadNewLevel(string level)
    {
        yield return new WaitForSeconds(2f);
        Application.LoadLevel(level);
    }
}