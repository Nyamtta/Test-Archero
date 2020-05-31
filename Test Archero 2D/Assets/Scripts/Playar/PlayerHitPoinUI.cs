using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHitPoinUI : MonoBehaviour
{

    [SerializeField] private Image HitPointImag = default;
    [SerializeField] private TextMeshProUGUI HitPointText = default;
    
    private Playar player = default;

    private float FullHipPoint = default; 

    private void Start() {

        player = FindObjectOfType<Playar>();
         
        if(player != null) {

            FullHipPoint = player.GetHitPoint();
            player.UpdateHpEvent += UpdateHitBar;
            player.UpdateHpEvent += UpdateText;
        }
        else {

            Debug.Log("From HitPointBar: Playar is not faund");
        }

        HitPointText.text = FullHipPoint.ToString();

    }

    private void UpdateHitBar(int hp) {

        HitPointImag.fillAmount = ((float)hp / FullHipPoint);
    }

    private void UpdateText(int hp) {

        HitPointText.text = hp.ToString(); 
    }
}
