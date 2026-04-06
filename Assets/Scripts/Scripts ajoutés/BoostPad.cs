using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BoostPad : MonoBehaviour
{
    [Header("Boost Parameters")]
    [SerializeField] private float boostForce = 20f;
    [SerializeField] private float boostDuration = 2f;

    [Header("Audio")]
    [SerializeField] private AudioSource boostAudio;

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie que c'est une voiture
        RemoteControlCarController car = other.GetComponent<RemoteControlCarController>();
        if (car != null)
        {
            // Applique le boost
            car.StartCoroutine(ApplyBoost(car));

            // Joue le son si défini
            if (boostAudio != null)
            {
                boostAudio.Play();
            }
        }
    }

    private System.Collections.IEnumerator ApplyBoost(RemoteControlCarController car)
    {
        // On suppose qu'on va temporairement ajouter une force directement
        Rigidbody rb = car.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 boostDir = car.ForwardDirection.normalized;
            float timer = 0f;

            while (timer < boostDuration)
            {
                rb.AddForce(boostDir * boostForce, ForceMode.Acceleration);
                timer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}