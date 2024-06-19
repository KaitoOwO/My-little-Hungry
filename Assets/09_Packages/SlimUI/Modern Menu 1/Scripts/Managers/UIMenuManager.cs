﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace SlimUI.ModernMenu {
    public class UIMenuManager : MonoBehaviour {
        private Animator CameraObject;

        [Header("MENUS")]
        public GameObject mainMenu;
        public GameObject firstMenu;
        public GameObject playMenu;
        public GameObject exitMenu;
        public GameObject extrasMenu;

        public enum Theme { custom1, custom2, custom3 };
        [Header("THEME SETTINGS")]
        public Theme theme;
        private int themeIndex;
        public ThemedUIData themeController;

        [Header("PANELS")]
        public GameObject mainCanvas;
        public GameObject PanelControls;
        public GameObject PanelVideo;
        public GameObject PanelGame;
        public GameObject PanelKeyBindings;
        public GameObject PanelMovement;
        public GameObject PanelCombat;
        public GameObject PanelGeneral;

        [Header("SETTINGS SCREEN")]
        public GameObject lineGame;
        public GameObject tittleImage;
        public GameObject lineVideo;
        public GameObject lineControls;
        public GameObject lineKeyBindings;
        public GameObject lineMovement;
        public GameObject lineCombat;
        public GameObject lineGeneral;

        [Header("LOADING SCREEN")]
        public bool waitForInput = true;
        public GameObject loadingMenu;
        public Slider loadingBar;
        public TMP_Text loadPromptText;
        public KeyCode userPromptKey;

        [Header("SFX")]
        public AudioSource hoverSound;
        public AudioSource sliderSound;
        public AudioSource swooshSound;

        [Header("Play Animation")]
        public Animator cameraAnimator;
        public GameObject canvasPlay;
        public Animator puertaAnimator;

        [Header("Fade Settings")]
        public Image fadeImage;
        public float fadeDuration = 1.0f;

        private void Start() {
            CameraObject = transform.GetComponent<Animator>();
            canvasPlay.SetActive(false);
            playMenu.SetActive(false);
            exitMenu.SetActive(false);
            if (extrasMenu) extrasMenu.SetActive(false);
            firstMenu.SetActive(true);
            mainMenu.SetActive(true);
            
            SetThemeColors();
        }

        private void SetThemeColors() {
            switch (theme) {
                case Theme.custom1:
                    themeController.currentColor = themeController.custom1.graphic1;
                    themeController.textColor = themeController.custom1.text1;
                    themeIndex = 0;
                    break;
                case Theme.custom2:
                    themeController.currentColor = themeController.custom2.graphic2;
                    themeController.textColor = themeController.custom2.text2;
                    themeIndex = 1;
                    break;
                case Theme.custom3:
                    themeController.currentColor = themeController.custom3.graphic3;
                    themeController.textColor = themeController.custom3.text3;
                    themeIndex = 2;
                    break;
                default:
                    Debug.Log("Invalid theme selected.");
                    break;
            }
        }

        public void PlayCampaign() {
            exitMenu.SetActive(false);
            if (extrasMenu) extrasMenu.SetActive(false);
            DisablePanels();
            mainMenu.SetActive(false);
            canvasPlay.SetActive(true);
            cameraAnimator.SetTrigger("play");
            puertaAnimator.SetTrigger("play");
            StartCoroutine(StartFadeAfterDelay(5.0f));
        }

        public void PlayCampaignMobile() {
            exitMenu.SetActive(false);
            if (extrasMenu) extrasMenu.SetActive(false);
            playMenu.SetActive(true);
            mainMenu.SetActive(false);
        }

        public void ReturnMenu() {
            playMenu.SetActive(false);
            if (extrasMenu) extrasMenu.SetActive(false);
            exitMenu.SetActive(false);
            mainMenu.SetActive(true);
            tittleImage.SetActive(true);
        }

        public void LoadScene(string scene) {
            if (scene != "") {
                StartCoroutine(LoadAsynchronously(scene));
            }
        }

        public void DisablePlayCampaign() {
            playMenu.SetActive(false);
        }

        public void Position2() {
            DisablePlayCampaign();
            CameraObject.SetFloat("Animate", 1);
        }

        public void Position1() {
            CameraObject.SetFloat("Animate", 0);
        }

        private void DisablePanels() {
            PanelControls.SetActive(false);
            PanelVideo.SetActive(false);
            PanelGame.SetActive(false);
            PanelKeyBindings.SetActive(false);

            lineGame.SetActive(false);
            lineControls.SetActive(false);
            lineVideo.SetActive(false);
            lineKeyBindings.SetActive(false);

            PanelMovement.SetActive(false);
            lineMovement.SetActive(false);
            PanelCombat.SetActive(false);
            lineCombat.SetActive(false);
            PanelGeneral.SetActive(false);
            lineGeneral.SetActive(false);
        }

        public void GamePanel() {
            DisablePanels();
            PanelGame.SetActive(true);
            lineGame.SetActive(true);
        }

        public void VideoPanel() {
            DisablePanels();
            PanelVideo.SetActive(true);
            lineVideo.SetActive(true);
        }

        public void ControlsPanel() {
            DisablePanels();
            PanelControls.SetActive(true);
            lineControls.SetActive(true);
        }

        public void KeyBindingsPanel() {
            DisablePanels();
            MovementPanel();
            PanelKeyBindings.SetActive(true);
            lineKeyBindings.SetActive(true);
        }

        public void MovementPanel() {
            DisablePanels();
            PanelKeyBindings.SetActive(true);
            PanelMovement.SetActive(true);
            lineMovement.SetActive(true);
        }

        public void CombatPanel() {
            DisablePanels();
            PanelKeyBindings.SetActive(true);
            PanelCombat.SetActive(true);
            lineCombat.SetActive(true);
        }

        public void GeneralPanel() {
            DisablePanels();
            PanelKeyBindings.SetActive(true);
            PanelGeneral.SetActive(true);
            lineGeneral.SetActive(true);
        }

        public void PlayHover() {
            hoverSound.Play();
        }

        public void PlaySFXHover() {
            sliderSound.Play();
        }

        public void PlaySwoosh() {
            swooshSound.Play();
        }

        public void AreYouSure() {
            exitMenu.SetActive(true);
            tittleImage.SetActive(false);
            if (extrasMenu) extrasMenu.SetActive(false);
            DisablePlayCampaign();
        }

        public void AreYouSureMobile() {
            exitMenu.SetActive(true);
            tittleImage.SetActive(false);
            if (extrasMenu) extrasMenu.SetActive(false);
            mainMenu.SetActive(false);
            DisablePlayCampaign();
        }

        public void ExtrasMenu() {
            playMenu.SetActive(false);
            tittleImage.SetActive(false);
            if (extrasMenu) extrasMenu.SetActive(true);
            exitMenu.SetActive(false);
        }

        public void QuitGame() {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        // Load Bar synching animation
        IEnumerator LoadAsynchronously(string sceneName) {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;
            mainCanvas.SetActive(false);
            loadingMenu.SetActive(true);

            while (!operation.isDone) {
                float progress = Mathf.Clamp01(operation.progress / .95f);
                loadingBar.value = progress;

                if (operation.progress >= 0.9f && waitForInput) {
                    loadPromptText.text = "Press " + userPromptKey.ToString().ToUpper() + " to continue";
                    loadingBar.value = 1;

                    if (Input.GetKeyDown(userPromptKey)) {
                        operation.allowSceneActivation = true;
                    }
                } else if (operation.progress >= 0.9f && !waitForInput) {
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }
        }

        // Fade out coroutine
        IEnumerator FadeOutAndLoadScene(string sceneName) {
            float elapsedTime = 0f;
            Color color = fadeImage.color;

            while (elapsedTime < fadeDuration) {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
                fadeImage.color = color;
                yield return null;
            }
            
            LoadScene(sceneName);
        }

        // Coroutine to start fade after delay
        IEnumerator StartFadeAfterDelay(float delay) {
            yield return new WaitForSeconds(delay);
            StartCoroutine(FadeOutAndLoadScene("01_Main"));
        }
    }
}
