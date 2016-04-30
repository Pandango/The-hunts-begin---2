using UnityEngine;
using System.Collections;

public class SoulParticleFix : MonoBehaviour {
    public ParticleSystem SoulParticle;
	// Use this for initialization
	void Start () {
        SoulParticle.GetComponent<Renderer>().sortingLayerName = "player";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
