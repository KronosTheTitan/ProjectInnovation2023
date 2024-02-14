using Mirror.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Examples.MultipleMatches.Scripts
{
    public class PlayerGUI : MonoBehaviour
    {
        public Text playerName;

        [ClientCallback]
        public void SetPlayerInfo(PlayerInfo info)
        {
            playerName.text = $"Player {info.playerIndex}";
            playerName.color = info.ready ? Color.green : Color.red;
        }
    }
}