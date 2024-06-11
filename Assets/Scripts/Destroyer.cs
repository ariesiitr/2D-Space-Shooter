using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}