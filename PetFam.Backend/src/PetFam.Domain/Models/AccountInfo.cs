namespace PetFam.Domain.Models
{
    public class AccountInfo
    {
        public string Number { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public int BankId { get; set; }
        public string RecieverName { get; set; } = string.Empty;
    }
}
