using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScaleParticles : MonoBehaviour {
	private ParticleSystem ps;
	void Update () {
		var main = ps.main;
		main.startSize = transform.lossyScale.magnitude;
	}
}