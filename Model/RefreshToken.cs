namespace worksquare.Model
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public required string Token { get; set; }

        public required string UserId { get; set; }

        public required string TenantId { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
