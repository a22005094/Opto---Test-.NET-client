using System.Text;

namespace SimpleElasticsearchTester.models
{
    public class Session
    {
        // ID: "Primary key" of this object - NOT USED. Elasticsearch is non-relational
        // public int Id { get; set; }

        // -----

        // The DeviceID this session was completed in
        public string DeviceId { get; set; } = "";

        // The UserID this session belongs to
        public string UserId { get; set; } = "";

        // -----

        // Session time will be obtained from the Start and End timestamps.

        // refers to the day when the session was done
        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        // -----

        // List of exercises done during this session
        public List<Exercise> exercises { get; set; } = new List<Exercise>();

        // ***************
        // TODO:
        // Constructor TBD
        // ***************

        private string PrintExercises()
        {
            if (exercises == null || exercises.Count == 0) return "";

            var sb = new StringBuilder();
            foreach (Exercise e in exercises)
            {
                sb.Append(e.ToString());
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return $"[DeviceID = {DeviceId} | UserID = {UserId} | StartAt = {StartAt.ToString("dd/MM/yyyy HH:mm:ss")} | EndAt = {EndAt.ToString("dd/MM/yyyy HH:mm:ss")}"
                + $" | Exercises: {Environment.NewLine + '[' + PrintExercises() + ']'}";
        }

    }
}
