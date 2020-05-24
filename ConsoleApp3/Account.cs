
public class Account {

    private double money; //decimal money;
	private string id;
	private string pwd;

	public double Money { get => money; set => money = value; }
	public string Id { get => id; set => id = value; }
	public string Pwd { get => pwd; set => pwd = value; }

	//string name;
	
	
	public bool SaveMoney( double money)
	{
		if( money < 0 ) return false;  //ÎÀÓï¾ä
		
		this.money += money;
		return true;		
	}
	
	public bool WithdrawMoney( double money)
	{
		if( this.money >= money )
		{
			this.money -= money;
			return true;
		}

		return false;

	}
	
	public bool IsMatch( string id, string pwd )
	{
		return id==this.id && pwd==this.pwd;
	}
	
	
		public double getMoney()
	{
		return money;
	}
	
	public void setMoney(double val)
	{
		this.money = val;
	}
	
	public string getId()
	{
		return id;
	}
		
	public void setId(string id)
	{
		this.id = id;
	}
		
	public string getpwd()
	{
		return pwd;
	}
		
	public void setPwd(string pwd)
	{
		this.pwd = pwd;
	}
	
}
