using UnityEngine;

public class Shooting : MonoBehaviour {

	public Transform _spawn;
	public Vector3 _velocity;
	public GameObject _projectileA, _projectileB, _projectileC, _projectileD;
	public GameObject _activeProjectile;
	public ParticleSystem _launchParticle;
	public bool _enemy;

	void Start(){
		_spawn = transform.FindChild("LauncherSpawn").GetComponentInChildren<Transform>();
		_launchParticle = _spawn.GetComponentInChildren<ParticleSystem>();
		if (!_enemy) SwitchProjectile();		
	}

	public void SwitchProjectile(){
		switch (_CombatManager._equipRanged._id){
			case 200:
			_activeProjectile = _projectileB;
			break;
			case 201:
			_activeProjectile = _projectileD;
			break;
			case 202:
			_activeProjectile = _projectileA;
			break;
			case 203:
			_activeProjectile = _projectileB;
			break;
		}
	}

	public void CalcVelocity(Vector3 target){
		SwitchProjectile();
		var dir = target - transform.position;
		var h = dir.y;		
		dir.y = 0;
		var dist = dir.magnitude;
		var a = 20 * Mathf.Deg2Rad;
		dir.y = dist * Mathf.Tan(a);
		dist += h/Mathf.Tan(a);
		if (dist < 0) dist = 0 - dist;
		var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2*a));
		_velocity = vel * dir.normalized;
		SpawnProjectile();		
	}

	public void ShootStraight(Vector3 target){
		CalcVelocity(target);
		/*var dir = target - transform.position;
		_velocity = dir.normalized;
		SpawnProjectile();*/
	}

	void SpawnProjectile(){
		_launchParticle.Play();
		var projectile = (GameObject) Instantiate(_activeProjectile, _spawn.position, transform.rotation);
		var rb = projectile.GetComponent<Rigidbody>();
		rb.velocity = _velocity;
	}
}
