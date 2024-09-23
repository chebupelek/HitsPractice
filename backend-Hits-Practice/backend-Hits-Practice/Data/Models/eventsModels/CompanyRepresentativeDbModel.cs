namespace Events.EventsDbModels;

public class CompanyRepresentativeDbModel : UserDbModel
{
    public required Guid CompanyId { get; set; }
    public required CompanyDbModel Company { get; set; }
}
