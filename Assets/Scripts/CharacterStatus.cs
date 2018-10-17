using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour {

    [SerializeField] private int healthPoints;
    [SerializeField] private int maxHealth;
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
                Destroy(this.gameObject);
            }

        }

    }

    void FixedUpdate()
    {

        //print(healthPoints+" "+invulnerability);

        if (invulnerability > 0)
        {

            invulnerability -= Time.deltaTime;

        }

        if (invulnerability < 0)
        {
            invulnerability = 0;
        }

    }

}
