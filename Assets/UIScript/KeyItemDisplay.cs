using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemDisplay : MonoBehaviour
{
    public GameObject goldenKeyDisplay;
    public GameObject redKeyDisplay;
    public PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        goldenKeyDisplay.SetActive(false);
        redKeyDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.haveGoldenKey)
        {
            goldenKeyDisplay.SetActive(true);
        }
        else
        {
            goldenKeyDisplay.SetActive(false);
        }
        if (playerStats.haveRedKey)
        {
            redKeyDisplay.SetActive(true);
        }
        else
        {
            redKeyDisplay.SetActive(false);
        }
    }
}
