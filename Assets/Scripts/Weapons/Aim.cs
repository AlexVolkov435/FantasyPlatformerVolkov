using System;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private float offset;

    private void Update()
    {
        CalculateRotationWeapon();
    }

    private void CalculateRotationWeapon()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationsZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationsZ + offset);
    }
}

