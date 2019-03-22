using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterStatus : MonoBehaviour {

    public AudioClip dmgSound;
    [SerializeField] public int healthPoints;
    [SerializeField] private int maxHealth;
    public TMPro.TextMeshProUGUI healthText;
    private float invulnerability;

        // Use this for initialization
        void Start () {

        if (healthPoints == 0)
        {
            healthPoints = 3;
        }
        if (maxHealth == 0)
        {
            maxHealth = 3;
        }

        invulnerability = 0;

        if (this.tag == "Player")
        {
            healthText.text = "Health: " + healthPoints.ToString();
        }

    }
	
	public void UpdateHealth(int healthUpdateValue)
    {
        if (invulnerability <= 0||healthUpdateValue>=0)
        {

            healthPoints += healthUpdateValue;

            if (healthPoints > maxHealth) { healthPoints = maxHealth; }

            if (healthUpdateValue < 0) { invulnerability = 2; }

            if (healthPoints <= 0)
            {
                if(gameObject.CompareTag("Player"))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }

        }

    }

    void FixedUpdate()
    {

        //print(healthPoints+" "+invulnerability);

        if (invulnerability > 0)
        {

            invulnerability -= Time.deltaTime;
            SoundManager.instance.PlayDmg(dmgSound);

        }

        if (invulnerability < 0)
        {
            invulnerability = 0;
        }

        if (this.tag == "Player")
        {
            healthText.text = "Health: " + healthPoints.ToString();
        }

    }

}
