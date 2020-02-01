﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BreakOffPieces : MonoBehaviour
{
    public float damageCooldown = .5f;
    private float nextDamage = 0f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {
            Break();
        }
    }

    public void Break()
    {
        if (nextDamage <= Time.time)
        {
            var availableComponents = GetComponentsInChildren<BreakableComponent>().OrderBy(c => c.lossPriority).ToList();
            if (availableComponents.Count() == 0)
                throw new UnityException("You lose!");

            availableComponents = availableComponents.Where(c => c.lossPriority == availableComponents[0].lossPriority).ToList();
            var component = availableComponents.Random();
            //Debug.Log($"Just lost {component.name}");
            component.Break();

            nextDamage = Time.time + damageCooldown;
        }
    }
}
