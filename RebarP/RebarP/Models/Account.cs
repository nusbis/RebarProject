using System.Collections.Generic;

namespace RebarP.Models;

public class Account
{
    public Guid ID { get;private set; }= Guid.NewGuid();
    public string Password { get; set; }
    public List<Guid> ListOfOrderIDs { get; }

    public void AddOrderToAccount(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException("id");
        if (IsIdOrderExsist(id))
            throw new Exception("this order exsist in our account");
        ListOfOrderIDs.Add(id);
    }
    public bool IsIdOrderExsist(Guid id)
    {
        return ListOfOrderIDs.FirstOrDefault(thisId => thisId == id) != null;
    }
}
