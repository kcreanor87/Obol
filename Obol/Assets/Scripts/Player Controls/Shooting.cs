using UnityEngine;

public class Shooting : MonoBehaviour {

	public Transform _spawn;
	public Vector3 _velocity;
	public GameObject _projectile;
	public ParticleSystem _launchParticle;

	void Start(){
		_spawn = transform.FindChild("LauncherSpawn").GetComponentInChildren<Transform>();
		_launchParticle = _spawn.GetComponentInChildren<ParticleSystem>();
	}

	public void CalcVelocity(Vector3 target){
		var dir = target - transform.position;
		var h = dir.y;		
		dir.y = 0;
		var dist = dir.magnitude;
		var a = 20 * Mathf.Deg2Rad;
		dir.y = dist * Mathf.Tan(a);
		dist += h/Mathf.Tan(a);
		var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2*a));
		_velocity = vel * dir.normalized;
		SpawnProjectile();
		
	}

	void SpawnProjectile(){
		_launchParticle.Play();
		var projectile = (GameObject) Instantiate(_projectile, _spawn.position, transform.rotation);
		var rb = projectile.GetComponent<Rigidbody>();
		rb.velocity = _velocity;
	}
}
