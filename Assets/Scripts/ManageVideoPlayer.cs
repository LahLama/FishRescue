using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
namespace LahLama
{
    public class ManageVideoPlayer : MonoBehaviour
    {
        public GameObject pickUpPrefab;
        public VideoPlayer videoPlayer;
        public RawImage rawImage;
        AvailibleVideos availibleVideos;
        public ClarifyTank clarifyTank;

        void Awake()
        {
            videoPlayer = GameObject.FindAnyObjectByType<VideoPlayer>();
            clarifyTank = FindAnyObjectByType<ClarifyTank>();
            availibleVideos = FindAnyObjectByType<AvailibleVideos>();
            rawImage = GameObject.FindGameObjectWithTag("videoTexture").GetComponent<RawImage>();
        }
        public void PlayVideo(GameObject fish)
        {
            if (clarifyTank.CurrentTank.name == "beachTank")
            {
                videoPlayer.clip = availibleVideos.videoClips[2];
            }
            if (clarifyTank.CurrentTank.name == "riverTank")
            {
                videoPlayer.clip = availibleVideos.videoClips[1];
            }
            if (clarifyTank.CurrentTank.name == "pondTank")
            {
                videoPlayer.clip = availibleVideos.videoClips[0];
            }

            videoPlayer.Play();
            rawImage.enabled = true;

            Destroy(fish);
            Debug.Log("A");
            // had issues with instant deletetion, temporary fix
            Invoke("StopVideo", 5f);
        }


        void StopVideo()
        {
            videoPlayer.Stop();
            rawImage.enabled = false;
        }
    }
}