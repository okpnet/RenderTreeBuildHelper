using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Xml.Schema;

namespace RenderTreeBuildHelper
{
    /// <summary>
    /// mouse event enums
    /// </summary>
    public enum MouseEvent:int
    {
        Click,
        Ctextmenu,
        Dblclick,
        Mousedown,
        Mouseenter,
        Mouseleave,
        Mousemove,
        Mouseout,
        Mouseover,
        Mouseup,
        Drag,
        Dragend,
        Dragenter,
        Dragleave,
        Dragover,
        Dragstart,
        Drop,
    }

    public static class MouseEventExt
    {
        /// <summary>
        /// Converts a mouse event enum to a string for a JavaScript function.
        /// </summary>
        /// <param name="mouseEvent"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string GetEventName(this MouseEvent mouseEvent)
        {
            return mouseEvent switch
            {
                MouseEvent.Click => "onclick",
                MouseEvent.Ctextmenu => "onctextmenu",
                MouseEvent.Dblclick => "ondblclick",
                MouseEvent.Mousedown => "onmousedown",
                MouseEvent.Mouseenter => "onmouseenter",
                MouseEvent.Mouseleave => "onmouseleave",
                MouseEvent.Mousemove => "onmousemove",
                MouseEvent.Mouseout => "onmouseout",
                MouseEvent.Mouseover => "onmouseover",
                MouseEvent.Mouseup => "onmouseup",
                MouseEvent.Drag => "ondrag",
                MouseEvent.Dragend => "ondragend",
                MouseEvent.Dragenter => "ondragenter",
                MouseEvent.Dragleave => "ondragleave",
                MouseEvent.Dragover => "ondragover",
                MouseEvent.Dragstart => "ondragstart",
                MouseEvent.Drop => "ondrop",
                _ => throw new NotImplementedException($"selected value '{(int)mouseEvent}' is not in enum.'")
            };
        }
    }
}
