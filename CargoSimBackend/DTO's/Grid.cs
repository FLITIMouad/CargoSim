using Newtonsoft.Json;

namespace CargoSimBackend.DTO_s
{
    public  class Grid
    {
        [JsonProperty("Nodes")]
        public  List<Node> Nodes { get; set; }
        [JsonProperty("Edges")]
        public  List<Edge> Edges { get; set; }
        [JsonProperty("Connections")]
        public  List<Connection> Connections { get; set; }   
    }

    public record Node(int Id,string Name);
    public record Edge(int Id,int Cost,string Time);
    public record Connection(int Id,int EdgeId,int FirstNodeId,int SecondNodeId);
}
