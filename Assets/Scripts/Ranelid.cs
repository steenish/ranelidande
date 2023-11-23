using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ranelid : MonoBehaviour {
    public NavMeshAgent agent;
    public Player player;
    public Animator animator;
    public float minDistance = 2.0f;
    public float voiceInterval;
    public AudioClip[] voiceClips;
    public float reviveTimer = 6;
    public int maxHealth = 10;

    private AudioSource currentSource;
    private int health;
    private AudioSource[] voiceClipSources;
    private bool isDead = false;

    private void Start() {
        health = maxHealth;
        InvokeRepeating("PlayRandomVoiceClip", 0, voiceInterval);

        voiceClipSources = new AudioSource[voiceClips.Length];
        for (int i = 0; i < voiceClips.Length; ++i) {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = voiceClips[i];
            source.spatialBlend = 1.0f;
            source.playOnAwake = false;
            source.rolloffMode = AudioRolloffMode.Linear;
            voiceClipSources[i] = source;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Projectile") && !isDead) {
            health -= 1;
        }

        if (health <= 0 && !isDead) {
            isDead = true;
            animator.SetTrigger("Kill");
            Invoke("ReviveTimer", reviveTimer);
        }
    }

    private void Update() {
        Vector3 playerPosition = player.transform.position;
        float distance = Vector3.Distance(transform.position, playerPosition);
        animator.SetFloat("DistanceToPlayer", distance);
        
        if (!isDead) {
            agent.SetDestination(playerPosition);
        }
    }

    private void PlayRandomVoiceClip() {
        if (currentSource != null && currentSource.isPlaying) {
            currentSource.Stop();
        }

        if (!isDead) {
            currentSource = null;
            AudioSource newSource = voiceClipSources[Random.Range(0, voiceClipSources.Length)];
            currentSource = newSource;
            newSource.Play();
        }
    }

    private void ReviveTimer() {
        health = maxHealth;
        isDead = false;
        animator.SetTrigger("Revive");
    }
}
