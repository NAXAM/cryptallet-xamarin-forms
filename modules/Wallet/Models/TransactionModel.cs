namespace Wallet.Models
{
    public class TransactionModel
    {
        public bool Inward { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public decimal Amount { get; set; }

        public long Timestamp { get; set; }
    }
}
