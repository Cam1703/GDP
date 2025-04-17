using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] public string _filename;

    [SerializeField] private Canvas _entryContainer;
    [SerializeField] private Canvas _entryTemplate;
    [SerializeField] private float _entryHeight;

    public static LeaderboardManager instance;

    private string _jsonString = null;
    public HighScoresList _jsonScores;
    public List<HighScore> _jsonList;
    private HighScore _newEntry;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Leaderboard()
    {
        _entryTemplate.gameObject.SetActive(false);

        GetRanking();

        _newEntry = new HighScore();
        _newEntry.id = GameManager.gameManager.playerName;
        _newEntry.score = GameManager.gameManager.playerScore;

        if (_jsonString == null)
        {   
            _jsonScores = new HighScoresList();
        }
        else {
            _jsonScores = JsonUtility.FromJson<HighScoresList>(_jsonString);
        }

        _jsonList = _jsonScores.highScores;
        _jsonList.Add(_newEntry);

        //Sorting the values
        for (int i = 0; i < _jsonList.Count; i++)
        {
            for (int j = i + 1; j < _jsonList.Count; j++) {
                if (_jsonList[j].score > _jsonList[i].score)
                {
                    HighScore _tmp = _jsonList[i];
                    _jsonList[i] = _jsonList[j];
                    _jsonList[j] = _tmp;
                }
            }
        }
        
        //Capping to 10 elements
        if (_jsonList.Count > 10)
        {
            _jsonList = _jsonList.GetRange(0,10);
        }

        //Showing the values
        for (int i = 0; i < _jsonList.Count; i++)
        {
            Transform _entryTransform = Instantiate(_entryTemplate.transform, _entryContainer.transform);
            RectTransform _entryRectTransform = _entryTransform.GetComponent<RectTransform>();
            _entryRectTransform.anchoredPosition = new Vector2(0, -1*_entryHeight*i);

            int _rank = i + 1;
            string _rankString;
            switch (_rank)
            {
                default: _rankString = _rank + "TH"; break;
                case 1: _rankString = "1ST"; break;
                case 2: _rankString = "2ND"; break;
                case 3: _rankString = "3RD"; break;
            }

            _entryTransform.gameObject.SetActive(true);

            _entryTransform.Find("PositionTemplate").GetComponent<Text>().text = _rankString;
            _entryTransform.Find("ScoreTemplate").GetComponent<Text>().text = (_jsonList[i].score.ToString("F3"));
            _entryTransform.Find("NameTemplate").GetComponent<Text>().text = _jsonList[i].id;
        }
        _jsonScores = new HighScoresList{highScores=_jsonList };
        
        //Saving the new leaderboard
        _jsonString = JsonUtility.ToJson(_jsonScores);
        PlayerPrefs.SetString("leaderboard", _jsonString);
        PlayerPrefs.Save();
    }

    private void GetRanking()
    {
        if (PlayerPrefs.HasKey("leaderboard"))
        {
            _jsonString = PlayerPrefs.GetString("leaderboard");
        }
    }

    [System.Serializable]
    public class HighScore
    { 
        public float score;
        public string id;

    }

    public class HighScoresList
    { 
        public List<HighScore> highScores;
    }
}

