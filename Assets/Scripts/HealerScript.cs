using UnityEngine;
using System.Collections;

public class HealerScript : MonoBehaviour
{
		public float healingRate;
		BatteryScript batteryScript;
		bool hasBattery;
		public GameObject ally;
	Mover player;

		void Start ()
		{
			player = GetComponent<Mover>();
		}

		void OnTriggerEnter (Collider other)
		{
				if (other.tag == "battery") {
						batteryScript = other.GetComponent<BatteryScript> ();
			batteryScript.FoW.enabled = true;
						if (batteryScript.currentHealth < 100) {
								batteryScript.spanner.renderer.enabled = true;
						}
				}
				if (other.gameObject == ally)
				{
			Mover m = other.GetComponent<Mover>();
			if (m.isFrozen)
			{
			m.isFrozen = false;
				other.GetComponent<CharacterController>().enabled = true;
			m.electricity.enableEmission = false;
				ally.GetComponentInChildren<ShooterScript>().isFrozen = false;
				m.audio.Stop();
				}
		}
		}

		void OnTriggerStay (Collider other)
		{
				if (other.tag == "battery" && !player.isFrozen) {
						if (!hasBattery) {
								batteryScript = other.GetComponent<BatteryScript> ();	
								hasBattery = true;
						}
						if (batteryScript.currentHealth < 100) {
								batteryScript.currentHealth += (healingRate * Time.deltaTime);

								batteryScript.healthHasChanged = true;
						} else {
								batteryScript.spanner.renderer.enabled = false;
						}
						if (batteryScript.isDestroyed) {
								hasBattery = false;
						}

				}

		
		}

		void OnTriggerExit (Collider other)
		{
				if (other.tag == "battery") {
						batteryScript.spanner.renderer.enabled = false;
						hasBattery = false;
				}
		}

}
