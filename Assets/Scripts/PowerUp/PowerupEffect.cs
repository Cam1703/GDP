using System.Collections;
using UnityEngine;

public abstract class PowerupEffect : ScriptableObject
{
    public abstract void ApplyEffect(GameObject player);
    public abstract void RemoveEffect(GameObject player);

}
