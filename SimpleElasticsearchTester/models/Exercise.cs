namespace SimpleElasticsearchTester.models
{
    public class Exercise
    {
        // ID: "Primary key" of this object - NOT USED. Elasticsearch is non-relational
        // public int Id { get; set; }

        // -----

        // The DeviceID this session was completed in
        public string DeviceId { get; set; } = "";

        // The UserID this session belongs to
        public string UserId { get; set; } = "";

        // -----

        // Refers to the Family/Type of exercise presented
        public string ChallengeId { get; set; } = "";

        // Time elapsed to complete the exercise
        public short DurationSeconds { get; set; }

        // Exercise completion score
        public short Score { get; set; }

        // refers to when the exercise was completed.
        public DateTime StartAt { get; set; }

        // ***************
        // TODO:
        // Constructor TBD
        // ***************

        public override string ToString()
        {
            return $"[DeviceID = {DeviceId} | UserID = {UserId} | ChallengeId = {ChallengeId} | Duration (s) = {DurationSeconds} | Score = {Score} | StartAt = {StartAt.ToString("dd/MM/yyyy HH:mm:ss")}]";
        }

        // (Fields for testing)
        // public string Description { get; set; } = "";
    }
}
