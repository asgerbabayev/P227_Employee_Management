namespace EmployeeManagement.Business.Exceptions.NotFoundExceptions;

public class EntityNotFoundException : NotFoundException
{
	public EntityNotFoundException() : base("Entity not found") { }
	public EntityNotFoundException(string message) : base(message) { }
}
