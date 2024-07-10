using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderTreeBuildHelper
{
    /// <summary>
    /// touch event enums
    /// </summary>
    public enum TouchEvent
    {
        Touchstart,
        Touchend,
        Touchmove,
        Touchcancel,
    }

    public static class TouchEventExt
    {
        /// <summary>
        /// Converts a touch event enum to a string for a JavaScript function.
        /// </summary>
        /// <param name="touchEvent"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string GetEventName(this TouchEvent touchEvent)
        {
            return touchEvent switch
            {
                TouchEvent.Touchstart => "ontouchstart",
                TouchEvent.Touchend => "ontouchend",
                TouchEvent.Touchmove => "ontouchmove",
                TouchEvent.Touchcancel => "ontouchcancel",
                _ => throw new NotImplementedException($"selected value '{(int)touchEvent}' is not in enum.'")
            };
        }
    }

}
