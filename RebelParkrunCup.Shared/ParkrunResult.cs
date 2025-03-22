    public class ParkrunResult
    {
        public string RunnerId { get; set; }
        public List<Result> Results { get; set; }
    }
    public class Result
    {
        public string RunnerId { get; set; }
        public string Event { get; set; }
        public string RunDate { get; set; }
        public string Time { get; set; }
    }