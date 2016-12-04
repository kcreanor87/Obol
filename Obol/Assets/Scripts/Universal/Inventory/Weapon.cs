[System.Serializable]
public class Weapon {

	public int _id;
	public string _name;
	public int _dam;
	public float _radius;	
	public float _fireRate;
	public int _cost;
	public int _levelReq;
	public bool _bought;


	public Weapon(int id, string name, float radius, int dam, float fireRate, int cost, int levelReq, bool bought){
		_id = id;
		_name = name;
		_radius = radius;
		_dam = dam;
		_fireRate = fireRate;
		_cost = cost;
		_levelReq = levelReq;
		_bought = bought;
	}
}
