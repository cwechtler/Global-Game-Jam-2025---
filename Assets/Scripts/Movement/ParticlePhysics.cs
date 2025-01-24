using UnityEngine;

public class ParticlePhysics : MonoBehaviour
{
    public ParticleSystem myParticleSystem;
//    public Rigidbody targetRigidbody; // The Rigidbody to interact with

    public Transform particleSystemTransform;
    public Transform targetObject;
    private ParticleSystem.RotationOverLifetimeModule rotationOverLifetime;


    private ParticleSystem.Particle[] particles;

    void Start()
    {
        particles = new ParticleSystem.Particle[myParticleSystem.main.maxParticles];
        //rotationOverLifetime = particleSystem.rotationOverLifetime;
        //rotationOverLifetime.enabled = true;

        // Set Particle System simulation space to Local
        var mainModule = myParticleSystem.main;
        mainModule.simulationSpace = ParticleSystemSimulationSpace.Local;
    }

//    void LateUpdate()
//    {
  //      if (targetObject != null && particleSystemTransform != null)
  //     {
            // Align the Particle System's Y rotation with the target's Y rotation
  //          Vector3 targetRotation = targetObject.eulerAngles;
  //          Vector3 PSRotation = particleSystemTransform.eulerAngles;

  //          PSRotation.y = targetRotation.z; // Copy the Y rotation
  //          particleSystemTransform.eulerAngles = PSRotation;
           // rotationOverLifetime.y = targetRotation.y;
           //particleSystem.rotation = PSRotation;

  //          
  //      }
  //      else
  //      {
  //          Debug.LogWarning("Target Object or Particle System Transform is not assigned!");
   //     }

        // Update particle interactions with the Rigidbody
//        int numParticles = particleSystem.GetParticles(particles);

//        for (int i = 0; i < numParticles; i++)
//        {
//            Vector3 direction = (particles[i].position - targetRigidbody.position).normalized;
//            float distance = Vector3.Distance(particles[i].position, targetRigidbody.position);

//            if (distance < 1.0f) // Adjust the distance threshold
//            {
//                targetRigidbody.AddForce(direction * 10f); // Adjust the force strength
//            }
//        }

//       particleSystem.SetParticles(particles, numParticles);
//    }
}