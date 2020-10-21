using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LedMatrixController.Services.Interfaces
{
    public enum SpeechDialogAction
    {
        None,
        Listening,
        Speaking
    }

    public interface ILedMatrixLayoutService
    {
        void SetSpeechDialogAnimation(SpeechDialogAction action, TimeSpan frameDuration);
    }
}
