using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject failBackground;
    [SerializeField] private GameObject successBackground;
    [SerializeField] private GameObject tapToPlayArea;
    [SerializeField] private GameObject inGameArea;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject successArea;
    [SerializeField] private GameObject failArea;
    [SerializeField] private GameObject playerCoinArea;

    [SerializeField] private TextMeshProUGUI playerCoinText;
    private int tempPlayerCoin;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        tempPlayerCoin = PlayerManager.Instance.playerCoin;
    }

    public void ShowUI()
    {
        failBackground.SetActive(PlayerManager.Instance.GetStateMachine().GetCurrentState()
                == PlayerManager.Instance.GetStateMachine().failState);

        successBackground.SetActive(false);

        tapToPlayArea.SetActive((PlayerManager.Instance.GetStateMachine().GetCurrentState()
                 == PlayerManager.Instance.GetStateMachine().tapToPlayState) && (GameManager.Instance.level < 2));

        inGameArea.SetActive(PlayerManager.Instance.GetStateMachine().GetCurrentState()
                 == PlayerManager.Instance.GetStateMachine().playState);

        successArea.SetActive(false);

        failArea.SetActive(PlayerManager.Instance.GetStateMachine().GetCurrentState()
                 == PlayerManager.Instance.GetStateMachine().failState);

        playerCoinArea.SetActive(PlayerManager.Instance.GetStateMachine().GetCurrentState()
         == PlayerManager.Instance.GetStateMachine().finishState);

    }

    public IEnumerator ShowSuccessPanel()
    {
        yield return new WaitForSeconds(2f);
        successBackground.SetActive(true);

        successArea.SetActive(true);
    }

    public void NextLevelOnUI(bool _isRestart)
    {
        if (!_isRestart)
        {
            GameManager.Instance.level = GameManager.Instance.level + 1;
        }

        GameManager.Instance.StartLevelStartAction();
    }

    public void ShowLevel()
    {
        levelText.text = "Level " + GameManager.Instance.level;
    }

    public void ShowPlayerCoin()
    {
       

        DOTween.To(() => tempPlayerCoin, x => tempPlayerCoin = x, PlayerManager.Instance.playerCoin, 1f)
            .OnUpdate(() => playerCoinText.text = tempPlayerCoin.ToString());
    }

}
