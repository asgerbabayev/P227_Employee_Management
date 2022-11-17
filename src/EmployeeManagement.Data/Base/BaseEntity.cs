namespace EmployeeManagement.Data.Base;
public class BaseEntity : BaseEntity<string>
{

}

public class BaseEntity<TKey>
{
    public TKey Id { get; set; } = default!;
    public bool IsDeleted { get; set; }

}