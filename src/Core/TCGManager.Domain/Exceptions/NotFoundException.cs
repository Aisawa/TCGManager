namespace TCGManager.Domain.Exceptions;

public class NotFoundException : DomainException
{
    public NotFoundException(string entityName, object key)
        : base($"{entityName} avec l'identifiant '{key}' est introuvable.") { }
}
