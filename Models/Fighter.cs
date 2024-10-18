using Microsoft.VisualBasic;

public class Fighter {
  public int ID  {get; set;}
  public string Name { get; set; }
  public int HP { get; set; }
  public int AD { get; set; }
  public string Quote { get; set; }
  
  public Fighter(int id ,string name, int hp, int ad, string quote) {
    ID = id;
    Name = name;
    HP = hp;
    AD = ad;
    Quote = quote;
    
  }

  public void Attack(Fighter opponent)
    {
        opponent.HP -= this.AD;
    }

  public override string ToString() {
        return $"{ID} {Name} (HP: {HP}, AD: {AD}) says: \"{Quote}\"";
    }

    
}
