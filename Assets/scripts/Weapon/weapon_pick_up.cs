using UnityEngine;
using TMPro;
using Mtscoptor.Weapons.Components;

public class PickUpWeapon : MonoBehaviour
{
    public GameObject dialogBox;  
    public TextMeshProUGUI dialogText;  
    public string message = "You've unlocked the throwing knife!";
    private bool isPlayerNearby = false;  
    public string weaponName;

    private void Start()
    {
        dialogBox.SetActive(false);  
    }

    private void Update()
    {
        if (isPlayerNearby)
        {
            PickupItem();
        }
        
    }

    private void PickupItem()
    {
        FindObjectOfType<PlayerStats>().GetWeapon(weaponName);
        dialogBox.SetActive(true);
        dialogText.text = message;
        
        Invoke("HideDialog", 2f);
        
        gameObject.SetActive(false);  

        Invoke("DestroyPickup", 3f);
    }

    private void HideDialog()
    {
        Debug.Log("Hiding dialog box");
        dialogBox.SetActive(false);  
        dialogText.text = "";  
    }

    private void DestroyPickup()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}

