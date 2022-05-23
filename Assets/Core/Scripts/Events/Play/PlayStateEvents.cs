using Core.Scripts.Events.Enums;

namespace Core.Scripts.Events.Play
{
    /// <summary>
    /// Events relating to the current Play State and its various components(vague, I know)
    /// </summary>
    public class PlayStateEvents
    {
        /// <summary>
        /// Triggered when the current PlayState is changed. Gives the previous and the new PlayState
        /// </summary>
        public delegate void OnPlayStateChanged( PlayState oldPlayState, PlayState newPlayState );
        public static event OnPlayStateChanged PlayStateChangedEvent;
        internal static void _TriggerPlayStateChanged( PlayState oldPlayState, PlayState newPlayState )
            => PlayStateChangedEvent?.Invoke( oldPlayState, newPlayState );

        
    }
}
