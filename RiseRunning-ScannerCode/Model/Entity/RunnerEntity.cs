namespace RiseRunning_ScannerCode.Model.Entity
{
    public record RunnerDTO(string Nome, string Email, DateTime DataHora);

    public class RunnerEntity
    {
        public Guid Id { get; set; } = new Guid();

        public string Nome { get; set; } = string.Empty;

        public string  Email { get; set; } = string.Empty ;

        public DateTime DataHora { get; set; } = DateTime.UtcNow;
    }
}
