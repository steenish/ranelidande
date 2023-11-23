using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float destroyTimer;

    private void Start() {
        Invoke("DestroyTimer", destroyTimer);
    }

    private void DestroyTimer() {
        Destroy(gameObject);
    }
}
