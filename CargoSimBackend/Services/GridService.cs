using CargoSimBackend.DTO_s;
using CargoSimBackend.Services.Infrastructure;

namespace CargoSimBackend;

public class GridService : IGridService
{
    private Grid _grid;
    private readonly IRestClient _restClient;

    public GridService(IRestClient restClient)
    {
        _restClient = restClient;
    }
    public async Task<string> SetGrid()
    {
        try
        {
          var (grid,error) = await this._restClient.GetGrid();
          this._grid = grid;
            if(!string.IsNullOrWhiteSpace(error))
            {
                throw new Exception(error);
            }
            return await Task.FromResult(string.Empty);
        }
        catch (Exception ex)
        {
            return await Task.FromException<string>(ex);
        }
    }

    public async Task<(Grid, string)> GetGrid()
    {
        try
        {
            return (await Task.FromResult(this._grid), await Task.FromResult(string.Empty));
        }
        catch (Exception ex)
        {
            return (null, await Task.FromException<string>(ex));
        }
    }

    public Task<Connection> GetConnection(int firstNodeId, int secondNodeId)
    {
        try
        {
            Connection connection = this._grid.Connections.Find(cn => cn.FirstNodeId == firstNodeId && cn.SecondNodeId == secondNodeId);
            return Task.FromResult(connection);
        }
        catch (Exception ex)
        {
            return Task.FromResult(default(Connection));
        }
    }
        public bool CheckConnection(int firstNodeId, int secondNodeId)
    {
        try
        {
            if(this._grid == null)
            {
                return false;
            }
            bool existsConnection = _grid.Connections.Any(cn => cn.FirstNodeId == firstNodeId && cn.SecondNodeId == secondNodeId);
            return existsConnection;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Task<Edge> GetEdgeById(int edgeId)
    {
        try
        {
            Edge edge = this._grid.Edges.Find(edge => edge.Id == edgeId);
            return Task.FromResult(edge);
        }
        catch (Exception ex)
        {
            return Task.FromResult(default(Edge));
        }
    }
}
