using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderTreeBuildHelper
{
    public enum FocusEvent:int
    {
        Focus,
        FocusIn,
        FocusOut,
    }

    public static class FocusEventExt
    {
        /// <summary>
        /// Converts a touch event enum to a string for a JavaScript function.
        /// </summary>
        /// <param name="focusEvent"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string GetEventName(this FocusEvent focusEvent)
        {
            return focusEvent switch
            {
                FocusEvent.Focus => "onfocus",
                FocusEvent.FocusIn => "onfocusin",
                FocusEvent.FocusOut => "onfocusout",
                _ => throw new NotImplementedException($"selected value '{(int)focusEvent}' is not in enum.'")
            };
        }
    }
}
