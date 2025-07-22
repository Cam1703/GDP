using UnityEngine;


[CreateAssetMenu(fileName = "TripleshotBuff", menuName = "PowerUp/TripleshotBuff")]
public class TripleshotBuff : PowerupEffect
{
    public override void ApplyEffect(GameObject player)
    {
        // Assuming the player has a Shooter component that handles shooting logic
        PlayerAimAndShoot shooter = player.GetComponent<PlayerAimAndShoot>();
        if (shooter != null)
        {
            shooter.HasTrippleShot = true; // Enable triple shot

        }
        else
        {
            Debug.LogWarning("Shooter component not found on player.");
        }
    }

    public override void RemoveEffect(GameObject player)
    {
        // Assuming the player has a Shooter component that handles shooting logic
        PlayerAimAndShoot shooter = player.GetComponent<PlayerAimAndShoot>();
        if (shooter != null)
        {
            shooter.HasTrippleShot = false; // Disable triple shot
            Debug.Log("Tripleshot effect removed from player: " + player.name);
        }
        else
        {
            Debug.LogWarning("Shooter component not found on player.");
        }
    }


}
