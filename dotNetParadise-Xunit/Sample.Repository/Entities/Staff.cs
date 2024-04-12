namespace Sample.Repository.Entities;

public class Staff
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int? Age { get; set; }
    public List<string>? Addresses { get; set; }

    public DateTimeOffset? Created { get; set; }

    public void Update(Staff staff)
    {
        this.Name = staff.Name;
        this.Email = staff.Email;
        this.Age = staff.Age;
        this.Addresses = staff.Addresses;
        Created = staff.Created;
    }
}
