namespace StepByStep.Core.Repositories
{
    public interface IAutomatRepository
    {
        Task<Automat?> GetAsync(string id);

        Task CreateAsync(Automat automat);
    }
}
