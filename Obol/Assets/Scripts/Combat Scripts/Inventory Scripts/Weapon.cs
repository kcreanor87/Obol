[System.Serializable]
public class Weapon {

	public int _id;
	public string _name;
	public float _radius;
	public int _dam;


	public Weapon(int id, string name, float radius, int dam){
		_id = id;
		_name = name;
		_radius = radius;
		_dam = dam;
	}
}
