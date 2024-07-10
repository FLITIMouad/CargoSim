using CargoSimBackend.DTO_s;

namespace CargoSimBackend;

public interface IGridService
{
    Task<string> SetGrid();
    Task<(Grid, string)> GetGrid();

    Task<Connection> GetConnection(int firstNodeId, int secondNodeId);

    bool CheckConnection(int firstNodeId, int secondNodeId);
    Task<Edge> GetEdgeById(int edgeId);

}
