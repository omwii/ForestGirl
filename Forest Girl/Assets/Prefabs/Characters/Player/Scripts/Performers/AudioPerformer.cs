using UnityEngine;

namespace Player
{
    public class AudioPerformer
    {
        #region Sources and Clips
        private AudioSource _movementAudioSource;
        private AudioSource _jumpAudioSource;
        private AudioSource _landAudioSource;
        private AudioSource _crouchDownAudioSource;
        private AudioSource _crouchUpAudioSource;
        private AudioClip _movementAudioClip;
        private AudioClip[] _jumpAudioClips;
        private AudioClip[] _landAudioClips;
        private AudioClip[] _crouchDownAudioClips;
        private AudioClip[] _crouchUpAudioClips;
        #endregion

        #region Pitches and Volumes
        private float _walkSoundPitch;
        private float _walkSoundVolume;
        private float _crouchedSoundPitch;
        private float _crouchedSoundVolume;
        private float _runSoundPitch;
        private float _runSoundVolume;
        #endregion

        public enum MovementType
        {
            Crouch,
            Walk,
            Run
        }

        //Public Methods
        public void StartMovementSound(MovementType movementType)
        {
            _movementAudioSource.UnPause();
            switch (movementType)
            {
                case MovementType.Walk:
                    _movementAudioSource.pitch = _walkSoundPitch;
                    _movementAudioSource.volume = _walkSoundVolume;
                    break;
                case MovementType.Crouch:
                    _movementAudioSource.pitch = _crouchedSoundPitch;
                    _movementAudioSource.volume = _crouchedSoundVolume;
                    break;
                case MovementType.Run:
                    _movementAudioSource.pitch = _runSoundPitch;
                    _movementAudioSource.volume = _runSoundVolume;
                    break;
            }
        }

        public void PauseMovementSound()
        {
            _movementAudioSource.Pause();
        }

        public void JumpSound()
        {
            _jumpAudioSource.clip = _jumpAudioClips[Random.Range(0, _jumpAudioClips.Length - 1)];
            _jumpAudioSource.Play();
        }

        public void LandSound()
        {
            _landAudioSource.clip = _landAudioClips[Random.Range(0, _landAudioClips.Length - 1)];
            _landAudioSource.Play();
        }

        public void CrouchDownSound()
        {
            _crouchDownAudioSource.clip = _crouchDownAudioClips[Random.Range(0, _crouchDownAudioClips.Length - 1)];
            _crouchDownAudioSource.Play();
        }

        public void CrouchUpSound()
        {
            _crouchUpAudioSource.clip = _crouchUpAudioClips[Random.Range(0, _crouchUpAudioClips.Length - 1)];
            _crouchUpAudioSource.Play();
        }

        //Constructor
        public AudioPerformer(AudioSource movementAudioSource,
        AudioSource jumpAudioSource,
        AudioSource landAudioSource,
        AudioSource crouchDownAudioSource,
        AudioSource crouchUpAudioSource,
        AudioClip movementAudioClip,
        AudioClip[] jumpAudioClips,
        AudioClip[] landAudioClips,
        AudioClip[] crouchDownAudioClips,
        AudioClip[] crouchUpAudioClips,
        float walkSoundPitch,
        float walkSoundVolume,
        float crouchedSoundPitch,
        float crouchedSoundVolume,
        float runSoundPitch,
        float runSoundVolume)
        {
            _movementAudioSource = movementAudioSource;
            _jumpAudioSource = jumpAudioSource;
            _landAudioSource = landAudioSource;
            _crouchDownAudioSource = crouchDownAudioSource;
            _crouchUpAudioSource = crouchUpAudioSource;
            _movementAudioClip = movementAudioClip;
            _jumpAudioClips = jumpAudioClips;
            _landAudioClips = landAudioClips;
            _crouchDownAudioClips = crouchDownAudioClips;
            _crouchUpAudioClips = crouchUpAudioClips;
            _walkSoundPitch = walkSoundPitch;
            _walkSoundVolume = walkSoundVolume;
            _crouchedSoundPitch = crouchedSoundPitch;
            _crouchedSoundVolume = crouchedSoundVolume;
            _runSoundPitch = runSoundPitch;
            _runSoundVolume = runSoundVolume;

            //Movement Audio Source prepare
            _movementAudioSource.clip = _movementAudioClip;
            _movementAudioSource.mute = true;
            _movementAudioSource.Play();
            _movementAudioSource.Pause();
            _movementAudioSource.mute = false;
        }
    }
}