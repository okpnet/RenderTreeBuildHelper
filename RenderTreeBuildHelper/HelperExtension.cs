using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace RenderTreeBuildHelper
{
    public static class HelperExtension
    {
        internal const string ATTR_CLASS = "class";
        internal const string ATTR_CHILD_CONTENT = "ChildContent";
        /// <summary>
        /// Add  class.
        /// Specify the class value string as an array.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="seq"></param>
        /// <param name="classes"></param>
        /// <returns></returns>
        public static void AddClassHelper(this RenderTreeBuilder builder,ref int sequence, params string[] classes)
        {
            var seq = sequence;
            if (classes.Length == 0) return;
            builder.AddAttribute(seq++, "class", string.Join(" ", classes));
            sequence = seq;
            return;
        }
        /// <summary>
        /// Close the element after the action of opening the element and inserting content.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="element"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static void OpenElementHelper(this RenderTreeBuilder builder,ref int sequence, string element, Action contents)
        {
            var seq = sequence;
            builder.OpenElement(seq++, element);
            contents.Invoke();
            builder.CloseElement();
            sequence = seq;
            return;
        }
        /// <summary>
        /// Close the component after the action of opening the component and inserting content.
        /// </summary>
        /// <typeparam name="TComponent"></typeparam>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static void OpenComponentHelper<TComponent>(this RenderTreeBuilder builder,ref int sequence, Action contents) where TComponent : notnull, Microsoft.AspNetCore.Components.IComponent
        {
            var seq = sequence;
            builder.OpenComponent<TComponent>(seq++);
            contents.Invoke();
            builder.CloseComponent();
            sequence = seq;
            return;
        }

        /// <summary>
        /// &U+003C span class="icon" &U+003E &U+003Ci class="gg-play-list-add"/&U+003E &U+003C/span &U+003Eを生成
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="seq"></param>
        /// <param name="iconClass"></param>
        /// <returns></returns>
        public static void AddIcons(this RenderTreeBuilder builder,ref int sequence, string iconClass)
        {
            var seq = sequence;
            builder.OpenElementHelper(ref seq, "span", () =>
            {

                builder.AddClassHelper(ref seq, "icon");
                builder.OpenElementHelper(ref seq, "i", () => {
                    builder.AddClassHelper(ref seq, iconClass);
                });
            });
            sequence = seq;
            return;
        }
        /// <summary>
        /// Optional parameter attributes.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="name"></param>
        /// <param name="addAttr"></param>
        public static void AddAttributeHelper(this RenderTreeBuilder builder, ref int sequence,string name,params string[] addAttr)
        {
            if (addAttr.Length>0 && string.Join("", addAttr).Trim() is string attrs && attrs.Length>0 )
            {
                var seq = sequence;

                builder.AddAttribute(seq++, name, attrs);
                sequence = seq;
            }
        }
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void AddAttributeHelper(this RenderTreeBuilder builder, ref int sequence, string name, object? value)
        {
            var seq = sequence;
            builder.AddAttribute(seq++, name, value);
            sequence = seq;
        }
        /// <summary>
        /// Specify the content to include RenderFragment.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="contentName"></param>
        /// <param name="action"></param>
        public static void AddContentsHelper(this RenderTreeBuilder builder, ref int sequence,string contentName,Action<RenderTreeBuilder> action)
        {
            var seq = sequence;
            builder.AddAttribute(seq++, contentName,(RenderFragment)(action.Invoke));
            sequence = seq;
        }
        /// <summary>
        /// Specify ‘ChildContent’ containing child elements.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="action"></param>
        public static void AddChildContentsHelper(this RenderTreeBuilder builder, ref int sequence,  Action<RenderTreeBuilder> action)
        {
            var seq = sequence;
            builder.AddAttribute(seq++, ATTR_CHILD_CONTENT, (RenderFragment)(action.Invoke));
            sequence = seq;
        }
        /// <summary>
        /// Specify ‘ChildContent’ containing child elements.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="action"></param>
        public static void AddChildContentsHelper(this RenderTreeBuilder builder, ref int sequence,string contentName, Action<RenderTreeBuilder> action)
        {
            var seq = sequence;
            builder.AddAttribute(seq++, contentName, (RenderFragment)(action.Invoke));
            sequence = seq;
        }
        /// <summary>
        /// add mouse event to element and specify call back function for fire. 
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="events"></param>
        /// <param name="callback"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void AddMouseEvent(this RenderTreeBuilder builder, ref int sequence, MouseEvent events, EventCallback callback)
        {
            var seq = sequence;
            builder.AddAttribute(seq++, events.GetEventName(), callback);
            sequence = seq;
        }
        /// <summary>
        /// add mouse event to element and specify call back function for fire. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="events"></param>
        /// <param name="callback"></param>
        public static void AddMouseEvent<T>(this RenderTreeBuilder builder, ref int sequence, MouseEvent events, EventCallback<T> callback)
        {
            var seq = sequence;
            builder.AddAttribute(seq++, events.GetEventName(), callback);
            sequence = seq;
        }
        /// <summary>
        /// add touch event to element and specify call back function for fire. 
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="events"></param>
        /// <param name="callback"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void AddTouchEvent(this RenderTreeBuilder builder, ref int sequence, TouchEvent events, EventCallback callback)
        {
            var seq = sequence;
            builder.AddAttribute(seq++, events.GetEventName(), callback);
            sequence = seq;
        }
        /// <summary>
        /// add touch event to element and specify call back function for fire. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="sequence"></param>
        /// <param name="events"></param>
        /// <param name="callback"></param>
        public static void AddTouchEvent<T>(this RenderTreeBuilder builder, ref int sequence, TouchEvent events, EventCallback<T> callback)
        {
            var seq = sequence;
            builder.AddAttribute(seq++, events.GetEventName(), callback);
            sequence = seq;
        }
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
        /// <summary>
        /// Converts a touch event enum to a string for a JavaScript function.
        /// </summary>
        /// <param name="touchEvent"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string GetEventName(this TouchEvent touchEvent)
        {
            return touchEvent switch {
                TouchEvent.Touchstart=> "ontouchstart",
                TouchEvent.Touchend => "ontouchend",
                TouchEvent.Touchmove => "ontouchmove",
                TouchEvent.Touchcancel => "ontouchcancel",
                _ => throw new NotImplementedException($"selected value '{(int)touchEvent}' is not in enum.'")
            };
        }
    }
}
