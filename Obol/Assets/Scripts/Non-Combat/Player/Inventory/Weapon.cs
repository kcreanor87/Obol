[System.Serializable]
public class Weapon {

	public int _id;
	public string _name;
	public float _radius;
	public int _dam;
	public float _fireRate;
	public string _desc;


	public Weapon(int id, string name, float radius, int dam, float fireRate, string desc){
		_id = id;
		_name = name;
		_radius = radius;
		_dam = dam;
		_fireRate = fireRate;
		_desc = desc;
	}
}
