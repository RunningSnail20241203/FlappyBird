using System.Threading.Tasks;

public class NetworkManager : MonoSingleton<NetworkManager>
{
    public bool IsConnected { get; private set; }
    public async Task JoinRoom(string matchId)
    {
        
    }
}