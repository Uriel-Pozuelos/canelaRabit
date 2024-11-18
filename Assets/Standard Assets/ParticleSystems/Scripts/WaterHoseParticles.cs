using System;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class WaterHoseParticles : MonoBehaviour
    {
        private static float _lastSoundTime;
        [SerializeField] private float force = 1f;

        private ParticleSystem m_ParticleSystem;
        private ParticleCollisionEvent[] m_CollisionEvents = new ParticleCollisionEvent[16];

        private void Awake()
        {
            m_ParticleSystem = GetComponent<ParticleSystem>();
            if (m_ParticleSystem == null)
            {
                Debug.LogError("No ParticleSystem found on this GameObject.");
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            if (m_ParticleSystem == null) return;

            int safeLength = m_ParticleSystem.GetSafeCollisionEventSize();
            if (m_CollisionEvents.Length < safeLength)
            {
                m_CollisionEvents = new ParticleCollisionEvent[safeLength];
            }

            int numCollisionEvents = m_ParticleSystem.GetCollisionEvents(other, m_CollisionEvents);

            for (int i = 0; i < numCollisionEvents; i++)
            {
                if (Time.time > _lastSoundTime + 0.2f)
                {
                    _lastSoundTime = Time.time;
                    // Puedes reproducir un sonido aquí
                }

                // Hacer el cast a Collider
                var col = m_CollisionEvents[i].colliderComponent as Collider;

                if (col != null && col.attachedRigidbody != null)
                {
                    Vector3 vel = m_CollisionEvents[i].velocity;
                    col.attachedRigidbody.AddForce(vel * force, ForceMode.Impulse);
                }

                // Llamar a "Extinguish" opcionalmente
                other.BroadcastMessage("Extinguish", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}
