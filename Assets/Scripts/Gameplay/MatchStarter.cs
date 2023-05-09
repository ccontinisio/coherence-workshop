using Coherence.Toolkit;
using UnityEngine;

public class MatchStarter : MonoBehaviour
{
    public int playersNeededToStart = 2;

    private int _connectedPlayers;
    private bool _matchHasStarted;
    private CoherenceSync _sync;
    private WaveManager _waveManager;

    private void Awake()
    {
        _sync = GetComponent<CoherenceSync>();
        _waveManager = FindObjectOfType<WaveManager>(true);
    }

    private void OnEnable()
    {
        _sync.MonoBridge.onLiveQuerySynced.AddListener(CountPlayers);
        _sync.MonoBridge.ClientConnections.OnCreated += OnPlayerJoined;
        _sync.MonoBridge.ClientConnections.OnDestroyed += OnPlayerDisconnected;
    }

    private void CountPlayers(CoherenceMonoBridge bridge)
    {
        if (_sync.HasStateAuthority)
        {
            _connectedPlayers = _sync.MonoBridge.ClientConnections.ClientConnectionCount;
            CheckPlayersNumber();
        }
    }

    private void OnPlayerJoined(CoherenceClientConnection obj)
    {
        _connectedPlayers++;
        CheckPlayersNumber();
    }
    
    private void OnPlayerDisconnected(CoherenceClientConnection obj)
    {
        _connectedPlayers--;
        CheckPlayersNumber();
    }

    /// <summary>
    /// Checks if the right number of players has been reached.
    /// If so, kicks off the first wave.
    /// </summary>
    private void CheckPlayersNumber()
    {
        if (_matchHasStarted) return;

        if (_connectedPlayers == playersNeededToStart)
        {
            // Kickoff the match
            _matchHasStarted = true;
            _waveManager.BeginWave();
        }
    }

    private void OnDisable()
    {
        _sync.MonoBridge.onLiveQuerySynced.AddListener(CountPlayers);
        _sync.MonoBridge.ClientConnections.OnCreated -= OnPlayerJoined;
        _sync.MonoBridge.ClientConnections.OnDestroyed -= OnPlayerDisconnected;
    }
}