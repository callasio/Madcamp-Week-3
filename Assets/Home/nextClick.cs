using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Home
{
    public class ImageClickSceneChange : MonoBehaviour
    {
        // Image 컴포넌트를 참조합니다.
        public Image targetImage;
        public string nextSceneName;
        private Button _button;
    
        private void Start()
        {
            _button = GetComponentInChildren<Button>();
            _button.onClick.AddListener(OnImageClick);
            // _button.onClick.Invoke();
            Debug.Log("Image clicked");
        }

        // 클릭 시 실행되는 함수
        private void OnImageClick()
        {
            // 다음 씬 이름을 설정하세요.
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                Debug.Log("Yet Scene loaded");
                SceneManager.LoadScene(nextSceneName);
                Debug.Log("Scene loaded Done");
            }
            else
            {
                Debug.Log("Next scene name is not set.");
            }
        }
    }
}