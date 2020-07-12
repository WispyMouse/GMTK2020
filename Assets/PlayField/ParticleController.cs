using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public Particle ParticlePF;
    HashSet<Reactor> ReactorsWithParticles { get; set; } = new HashSet<Reactor>();

    public void StartSweatParticle(Reactor onBuilding)
    {
        StartCoroutine(RunParticles(onBuilding, (Particle par) => { par.SetSteamParticle(); }));

        ReactorsWithParticles.Add(onBuilding);
    }

    public void StartSteamParticle(Reactor onBuilding)
    {
        StartCoroutine(RunParticles(onBuilding, (Particle par) => { par.SetSteamParticle(); }));
    }

    public void StopParticle(Reactor onBuilding)
    {
        ReactorsWithParticles.Remove(onBuilding);
    }

    IEnumerator RunParticles(Reactor building, System.Action<Particle> particleSetUp)
    {
        ReactorsWithParticles.Add(building);

        while (ReactorsWithParticles.Contains(building))
        {
            Particle newParticle = Instantiate(ParticlePF, building.transform);
            newParticle.transform.localPosition = Vector3.up * .25f + Vector3.right * .25f;
            particleSetUp(newParticle);
            yield return new WaitForSeconds(.2f);
        }
    }

    public void PlayResourceParticle(Reactor onBuilding, GameResource fromResource)
    {
        Particle newParticle = Instantiate(ParticlePF, onBuilding.transform);
        newParticle.transform.localPosition = Vector3.up * .5f;
        newParticle.SetParticle(fromResource.Graphic);
    }
}
